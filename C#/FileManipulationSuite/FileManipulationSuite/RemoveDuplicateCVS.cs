using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileManipulationSuite
{
    public partial class RemoveDuplicateCVS : Form
    {
        public RemoveDuplicateCVS()
        {
            InitializeComponent();
        }

        private void Button_Run_Click(object sender, EventArgs e)
        {
            if (!File.Exists(TextBox_FileName.Text))
            {
                TextBox_MessageBox.Text = "File does not exist.";
                return;
            }

            int sectionsPerObject;
            int sectionToCompare;
            if (!int.TryParse(TextBox_SectionsPerObject.Text, out sectionsPerObject))
            {
                TextBox_MessageBox.Text = "Sections Per Object input not an integer";
                return;
            }
            if (!int.TryParse(TextBox_SectionToCompare.Text, out sectionToCompare))
            {
                TextBox_MessageBox.Text = "Section to compare input not an integer";
                return;
            }

            string s;
            using (StreamReader sr = new StreamReader(TextBox_FileName.Text))
            {
                s = sr.ReadToEnd();
            }
            s = s.Replace("\n", "").Replace("\r", "");

            List<string> arr = s.Split(',').Where(t => t != "").ToList();
            List<List<string>> strings = new List<List<string>>();
            for (int i = 0; i < arr.Count; i += sectionsPerObject)
            {
                strings.Add(new List<string>());
                for (int j = 0; j < sectionsPerObject; j++)
                {
                    strings.Last().Add(arr[i + j]);
                }
            }

            var grouped = strings.GroupBy(t => t[sectionToCompare - 1]);
            using (StreamWriter sr = new StreamWriter(TextBox_FileName.Text.Remove(TextBox_FileName.Text.Length - 4) + "_UPDATED.csv"))
            {
                foreach(var g in grouped)
                {
                    string newLine = "";
                    for (int i = 0; i < g.First().Count; i++)
                    {
                        if (i + 1 == sectionToCompare)
                            newLine += g.Key;
                        else
                            newLine += g.First()[i];

                        if(i != g.First().Count - 1)
                            newLine += ",";
                    }
                    sr.WriteLine(newLine);
                }
            }
            this.Close();
        }
    }
}
