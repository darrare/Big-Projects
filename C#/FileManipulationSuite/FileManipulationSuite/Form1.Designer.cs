namespace FileManipulationSuite
{
    partial class Form1
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
            this.GitPull = new System.Windows.Forms.Button();
            this.GlobalTextBox = new System.Windows.Forms.TextBox();
            this.GlobalTextBoxLabel = new System.Windows.Forms.Label();
            this.LogWindow = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SwitchFirstLast = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // GitPull
            // 
            this.GitPull.Location = new System.Drawing.Point(13, 13);
            this.GitPull.Name = "GitPull";
            this.GitPull.Size = new System.Drawing.Size(131, 23);
            this.GitPull.TabIndex = 0;
            this.GitPull.Text = "Git Pull";
            this.GitPull.UseVisualStyleBackColor = true;
            this.GitPull.Click += new System.EventHandler(this.GitPull_Click);
            // 
            // GlobalTextBox
            // 
            this.GlobalTextBox.Location = new System.Drawing.Point(466, 15);
            this.GlobalTextBox.Name = "GlobalTextBox";
            this.GlobalTextBox.Size = new System.Drawing.Size(322, 20);
            this.GlobalTextBox.TabIndex = 1;
            // 
            // GlobalTextBoxLabel
            // 
            this.GlobalTextBoxLabel.AutoSize = true;
            this.GlobalTextBoxLabel.Location = new System.Drawing.Point(398, 18);
            this.GlobalTextBoxLabel.Name = "GlobalTextBoxLabel";
            this.GlobalTextBoxLabel.Size = new System.Drawing.Size(62, 13);
            this.GlobalTextBoxLabel.TabIndex = 2;
            this.GlobalTextBoxLabel.Text = "Folder Root";
            // 
            // LogWindow
            // 
            this.LogWindow.Enabled = false;
            this.LogWindow.Location = new System.Drawing.Point(428, 253);
            this.LogWindow.Multiline = true;
            this.LogWindow.Name = "LogWindow";
            this.LogWindow.Size = new System.Drawing.Size(360, 185);
            this.LogWindow.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(428, 234);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Log Window";
            // 
            // SwitchFirstLast
            // 
            this.SwitchFirstLast.Location = new System.Drawing.Point(13, 42);
            this.SwitchFirstLast.Name = "SwitchFirstLast";
            this.SwitchFirstLast.Size = new System.Drawing.Size(131, 23);
            this.SwitchFirstLast.TabIndex = 5;
            this.SwitchFirstLast.Text = "Switch First-Last";
            this.SwitchFirstLast.UseVisualStyleBackColor = true;
            this.SwitchFirstLast.Click += new System.EventHandler(this.SwitchFirstLast_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(630, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Build 1.0.0     2/8/2019 9:50PM";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SwitchFirstLast);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LogWindow);
            this.Controls.Add(this.GlobalTextBoxLabel);
            this.Controls.Add(this.GlobalTextBox);
            this.Controls.Add(this.GitPull);
            this.Name = "Form1";
            this.Text = "File Manipulation Suite";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button GitPull;
        private System.Windows.Forms.TextBox GlobalTextBox;
        private System.Windows.Forms.Label GlobalTextBoxLabel;
        private System.Windows.Forms.TextBox LogWindow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SwitchFirstLast;
        private System.Windows.Forms.Label label2;
    }
}

