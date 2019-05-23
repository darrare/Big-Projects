namespace FileManipulationSuite
{
    partial class RemoveDuplicateCVS
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TextBox_SectionsPerObject = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_SectionToCompare = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBox_FileName = new System.Windows.Forms.TextBox();
            this.Button_Run = new System.Windows.Forms.Button();
            this.TextBox_MessageBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TextBox_SectionsPerObject
            // 
            this.TextBox_SectionsPerObject.Location = new System.Drawing.Point(12, 25);
            this.TextBox_SectionsPerObject.Name = "TextBox_SectionsPerObject";
            this.TextBox_SectionsPerObject.Size = new System.Drawing.Size(100, 20);
            this.TextBox_SectionsPerObject.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sections Per Object";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Section to compare";
            // 
            // TextBox_SectionToCompare
            // 
            this.TextBox_SectionToCompare.Location = new System.Drawing.Point(118, 25);
            this.TextBox_SectionToCompare.Name = "TextBox_SectionToCompare";
            this.TextBox_SectionToCompare.Size = new System.Drawing.Size(100, 20);
            this.TextBox_SectionToCompare.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "File Name";
            // 
            // TextBox_FileName
            // 
            this.TextBox_FileName.Location = new System.Drawing.Point(12, 77);
            this.TextBox_FileName.Name = "TextBox_FileName";
            this.TextBox_FileName.Size = new System.Drawing.Size(206, 20);
            this.TextBox_FileName.TabIndex = 4;
            // 
            // Button_Run
            // 
            this.Button_Run.Location = new System.Drawing.Point(12, 103);
            this.Button_Run.Name = "Button_Run";
            this.Button_Run.Size = new System.Drawing.Size(75, 23);
            this.Button_Run.TabIndex = 6;
            this.Button_Run.Text = "Start";
            this.Button_Run.UseVisualStyleBackColor = true;
            this.Button_Run.Click += new System.EventHandler(this.Button_Run_Click);
            // 
            // TextBox_MessageBox
            // 
            this.TextBox_MessageBox.Enabled = false;
            this.TextBox_MessageBox.Location = new System.Drawing.Point(93, 103);
            this.TextBox_MessageBox.Multiline = true;
            this.TextBox_MessageBox.Name = "TextBox_MessageBox";
            this.TextBox_MessageBox.Size = new System.Drawing.Size(125, 49);
            this.TextBox_MessageBox.TabIndex = 7;
            // 
            // RemoveDuplicateCVS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 158);
            this.Controls.Add(this.TextBox_MessageBox);
            this.Controls.Add(this.Button_Run);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextBox_FileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextBox_SectionToCompare);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextBox_SectionsPerObject);
            this.Name = "RemoveDuplicateCVS";
            this.Text = "RemoveDuplicateCVS";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TextBox_SectionsPerObject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextBox_SectionToCompare;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBox_FileName;
        private System.Windows.Forms.Button Button_Run;
        private System.Windows.Forms.TextBox TextBox_MessageBox;
    }
}