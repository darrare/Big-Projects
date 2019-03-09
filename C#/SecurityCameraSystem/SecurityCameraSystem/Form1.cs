using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;

namespace SecurityCameraSystem
{
    public partial class Form1 : Form
    {
        bool debugMode = false;
        bool isEmailing = false;
        private VideoCapture camera;
        private Mat frame;
        private Stopwatch stopwatch;
        private Stopwatch emailStopwatch;
        VideoWriter videoWriter;

        List<string> fileNames = new List<string>();

        private Mat storedFrame;
        private Mat differenceFrame;
        double maxIntensityPerIteration = 0;
        double intensityToTriggerRecording = 5;

        double timePerStoredUpdateInMilliseconds = 2000;
        double timeToCollectBeforeEmailInMilliseconds = 120000;

        IntensityContainer intensityContainer = new IntensityContainer();
        bool isRecording;

        public Form1()
        {
            InitializeComponent();

            camera = new VideoCapture(0);
            stopwatch = new Stopwatch();
            stopwatch.Start();
            emailStopwatch = new Stopwatch();
            camera.ImageGrabbed += ProcessFrame;
            frame = new Mat();
            storedFrame = new Mat();
            differenceFrame = new Mat();
            if (camera != null)
            {
                try
                {
                    camera.Start();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void InitializeStoredFrame()
        {
            stopwatch.Reset(); stopwatch.Start();
            camera.Retrieve(storedFrame, 0);
            CvInvoke.CvtColor(storedFrame, storedFrame, ColorConversion.Bgr2Gray);
            OneSecondIntervalPictureBox.Invoke(new MethodInvoker(delegate () { OneSecondIntervalPictureBox.Image = storedFrame.Bitmap; }));
            maxIntensityPerIteration = 0;
        }
        
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (camera != null && camera.Ptr != IntPtr.Zero)
            {
                camera.Retrieve(frame, 0);
                if (storedFrame.Bitmap == null)
                {
                    InitializeStoredFrame();
                }
                CvInvoke.CvtColor(frame, frame, ColorConversion.Bgr2Gray);

                DefaultCameraPictureBox.Invoke(new MethodInvoker(delegate () { DefaultCameraPictureBox.Image = frame.Bitmap; }));

                //It has been timeperstoredupdateinmilliseconds milliseconds
                if (stopwatch.ElapsedMilliseconds > timePerStoredUpdateInMilliseconds)
                {
                    stopwatch.Reset(); stopwatch.Start();
                    camera.Retrieve(storedFrame, 0);
                    CvInvoke.CvtColor(storedFrame, storedFrame, ColorConversion.Bgr2Gray);
                    OneSecondIntervalPictureBox.Invoke(new MethodInvoker(delegate () { OneSecondIntervalPictureBox.Image = storedFrame.Bitmap; }));
                    maxIntensityPerIteration = 0;
                }

                Image<Gray, byte> diff = new Image<Gray, byte>(frame.Bitmap);
                diff = diff.AbsDiff(new Image<Gray, byte>(storedFrame.Bitmap));
                diff = diff.Erode(2);

                intensityContainer.Add(diff.GetAverage().Intensity);

                if (!isRecording && intensityContainer.GetAverage() > intensityToTriggerRecording)
                {
                    InitializeRecording();
                    isRecording = true;
                    if (!emailStopwatch.IsRunning)
                        SendWarningEmail();
                    emailStopwatch.Reset();
                }
                else if (isRecording && intensityContainer.GetAverage() < intensityToTriggerRecording / 2)
                {
                    DisableRecordingAndSaveVideo();
                    isRecording = false;
                    emailStopwatch.Start();
                }
                textBox1.Invoke(new MethodInvoker(delegate ()
                {
                    if (diff.GetAverage().Intensity > maxIntensityPerIteration)
                    {
                        maxIntensityPerIteration = diff.GetAverage().Intensity;
                        textBox1.Text = diff.GetAverage().Intensity.ToString();
                    }
                    IntensityContainerAverageTextBox.Text = intensityContainer.GetAverage().ToString();
                }));

                DifferencePictureBox.Invoke(new MethodInvoker(delegate () { DifferencePictureBox.Image = diff.Bitmap; }));

                //Handle emails
                if (emailStopwatch.ElapsedMilliseconds > timeToCollectBeforeEmailInMilliseconds)
                {
                    SendWarningEmail(true);
                    fileNames.Clear();
                    emailStopwatch.Reset();
                }
            }
        }

        private void AddFrameToVideo(object sender, EventArgs e)
        {
            if (videoWriter.IsOpened && !debugMode)
                videoWriter.Write(frame);
        }

        private void InitializeRecording()
        {
            if (debugMode)
                return;
            DateTime dateTime = DateTime.Now;
            string fileName = dateTime.Month + "-" + dateTime.Day + "-" + dateTime.Year + "---" + dateTime.Hour + "h" + dateTime.Minute + "m" + dateTime.Second + "s";
            videoWriter = new VideoWriter(Application.StartupPath + "\\Recordings\\Videos\\" + fileName + ".mp4", (int)camera.GetCaptureProperty(CapProp.FourCC), 30, frame.Size, false);
            camera.ImageGrabbed += AddFrameToVideo;
            IsRecordingRadioButton.Invoke(new MethodInvoker(delegate () { IsRecordingRadioButton.Checked = true; }));
            SaveImage(Application.StartupPath + "\\Recordings\\Images\\" + fileName);
        }

        private void SaveImage(string path)
        {
            frame.Bitmap.Save(path + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            fileNames.Add(path + ".jpg");
        }

        private void DisableRecordingAndSaveVideo()
        {
            if (debugMode)
                return;
            camera.ImageGrabbed -= AddFrameToVideo;
            videoWriter.Dispose();
            IsRecordingRadioButton.Invoke(new MethodInvoker(delegate () { IsRecordingRadioButton.Checked = false; }));
        }

        private void SendWarningEmail(bool sendScreenshots = false)
        {
            if (debugMode || !isEmailing)
                return;
            MailAddress from = new MailAddress("RD.GeneratedEmail@gmail.com", "Ryan Darras");
            MailAddress to = new MailAddress("ryandarras@gmail.com", "Ryan Darras");
            string phrase = "IntruderAlert";
            string subject = "Intruder Alert";
            string body = "An intruder has entered your room.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, phrase),
                Timeout = 120000,
            };

            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;

            if (sendScreenshots)
            {
                foreach (string s in fileNames)
                {
                    message.Attachments.Add(new Attachment(s));
                }
            }
            
            smtp.Send(message);
        }
    }
}
