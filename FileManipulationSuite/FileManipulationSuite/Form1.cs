using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileManipulationSuite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GlobalTextBox.Text = Application.StartupPath;
        }

        /// <summary>
        /// Simplifies opening a confirmation window in other methods
        /// </summary>
        /// <param name="message">The message to tell the user for confirmation</param>
        /// <returns>true = yes button, false = no button</returns>
        private bool OpenConfirmWindow(string message)
        {
            DialogResult confirmResult = MessageBox.Show(message, "Confirm Action", MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Attempts to git pull for all folders in this location
        /// </summary>
        private void GitPull_Click(object sender, EventArgs e)
        {
            if (!OpenConfirmWindow("Are you sure you want to attempt to pull a repository in every directory?"))
                return;
            try
            {
                string[] directories = Directory.GetDirectories(GlobalTextBox.Text);

                Process p = new Process()
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                    },
                };
                p.Start();
                using (var writer = p.StandardInput)
                {
                    for (int i = 0; i < directories.Length; i++)
                    {
                        writer.WriteLine("cd " + directories[i] + " 2>> ..\\CDError.txt");
                        //sr.WriteLine("git clean -f");
                        writer.WriteLine("git fetch origin 2>> ..\\FetchError.txt");
                        writer.WriteLine("git reset --hard origin/master 2>> ..\\ResetError.txt");
                    }
                }
                LogWindow.Text = "Repositories pulled";
            }
            catch(Exception ex)
            {
                LogWindow.Text = ex.Message;
            }
        }

        /// <summary>
        /// If folder names are "Ryan Darras", will switch to "Darras Ryan"
        /// If Folder names are "Darras Ryan", will switch to "Ryan Darras"
        /// 
        /// Warning, this will apply to ALL folders in location
        /// </summary>
        private void SwitchFirstLast_Click(object sender, EventArgs e)
        {
            if (!OpenConfirmWindow("This will change all directories names from RYAN DARRAS to DARRAS RYAN, and visa versa. " +
                "\nThis only works on file names with a single space between two words." +
                "\nDo not use otherwise."))
                return;

            try
            {
                string[] directories = Directory.GetDirectories(GlobalTextBox.Text);
                string[] directoriesChange = directories.Select(t => t.Substring(0, t.LastIndexOf("\\") + 1) + t.Substring(t.LastIndexOf("\\") + 1).Split()[1] + " " + t.Substring(t.LastIndexOf("\\") + 1).Split()[0]).ToArray();
                for(int i = 0; i < directories.Length; i++)
                {
                    Directory.Move(directories[i], directoriesChange[i]);
                }
                LogWindow.Text = "Successfully changed names";
            }
            catch (Exception ex)
            {
                LogWindow.Text = ex.Message;
            }
        }
    }
}
