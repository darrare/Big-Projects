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
        private VideoCapture camera;
        private Mat frame;
        private Stopwatch stopwatch;
        private Stopwatch emailStopwatch;

        private Mat storedFrame;
        private Mat differenceFrame;
        double maxIntensityPerIteration = 0;
        double intensityToTriggerEmail = 5;

        double timePerStoredUpdateInMilliseconds = 10000;
        double timeForSendEmailInMilliseconds = 100000;

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
        
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (camera != null && camera.Ptr != IntPtr.Zero)
            {
                camera.Retrieve(frame, 0);
                if (storedFrame.Bitmap == null)
                {
                    camera.Retrieve(storedFrame, 0);
                    camera.Retrieve(differenceFrame, 0);
                }
                CvInvoke.CvtColor(frame, frame, ColorConversion.Bgr2Gray);

                DefaultCameraPictureBox.Invoke(new MethodInvoker(delegate () { DefaultCameraPictureBox.Image = frame.Bitmap; }));

                //It has been 1 second
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

                if (diff.GetAverage().Intensity > intensityToTriggerEmail && !emailStopwatch.IsRunning || emailStopwatch.ElapsedMilliseconds > timeForSendEmailInMilliseconds)
                {
                    emailStopwatch.Reset(); emailStopwatch.Start();
                    camera.Retrieve(frame, 0);
                    SendWarningEmail(frame.Bitmap);
                }
                textBox1.Invoke(new MethodInvoker(delegate ()
                {
                    if (diff.GetAverage().Intensity > maxIntensityPerIteration)
                    {
                        maxIntensityPerIteration = diff.GetAverage().Intensity;
                        textBox1.Text = diff.GetAverage().Intensity.ToString();
                    }
                }));
                DifferencePictureBox.Invoke(new MethodInvoker(delegate () { DifferencePictureBox.Image = diff.Bitmap; }));
            }
        }

        private void SendWarningEmail(Image bitmap)
        {
            return;
            MailAddress from = new MailAddress("RD.GeneratedEmail@gmail.com", "Ryan Darras");
            MailAddress to = new MailAddress("ryandarras@gmail.com", "Ryan Darras");
            string fromPassword = "Google0274";
            string subject = "Intruder Alert";
            string body = "An intruder has entered your room.";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(from.Address, fromPassword),
                Timeout = 20000,
            };

            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;

            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                stream.Position = 0;
                message.Attachments.Add(new Attachment(stream, "image.jpg"));
                smtp.Send(message);
            }
        }
    }
}
