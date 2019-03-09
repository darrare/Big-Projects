namespace SecurityCameraSystem
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
            this.DefaultCameraPictureBox = new System.Windows.Forms.PictureBox();
            this.OneSecondIntervalPictureBox = new System.Windows.Forms.PictureBox();
            this.DifferencePictureBox = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.IsRecordingRadioButton = new System.Windows.Forms.RadioButton();
            this.IntensityContainerAverageTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DefaultCameraPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OneSecondIntervalPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DifferencePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // DefaultCameraPictureBox
            // 
            this.DefaultCameraPictureBox.Location = new System.Drawing.Point(12, 12);
            this.DefaultCameraPictureBox.Name = "DefaultCameraPictureBox";
            this.DefaultCameraPictureBox.Size = new System.Drawing.Size(320, 180);
            this.DefaultCameraPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DefaultCameraPictureBox.TabIndex = 3;
            this.DefaultCameraPictureBox.TabStop = false;
            // 
            // OneSecondIntervalPictureBox
            // 
            this.OneSecondIntervalPictureBox.Location = new System.Drawing.Point(338, 12);
            this.OneSecondIntervalPictureBox.Name = "OneSecondIntervalPictureBox";
            this.OneSecondIntervalPictureBox.Size = new System.Drawing.Size(320, 180);
            this.OneSecondIntervalPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.OneSecondIntervalPictureBox.TabIndex = 4;
            this.OneSecondIntervalPictureBox.TabStop = false;
            // 
            // DifferencePictureBox
            // 
            this.DifferencePictureBox.Location = new System.Drawing.Point(12, 198);
            this.DifferencePictureBox.Name = "DifferencePictureBox";
            this.DifferencePictureBox.Size = new System.Drawing.Size(320, 180);
            this.DifferencePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.DifferencePictureBox.TabIndex = 5;
            this.DifferencePictureBox.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(491, 418);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(297, 20);
            this.textBox1.TabIndex = 6;
            // 
            // IsRecordingRadioButton
            // 
            this.IsRecordingRadioButton.AutoSize = true;
            this.IsRecordingRadioButton.Location = new System.Drawing.Point(774, 12);
            this.IsRecordingRadioButton.Name = "IsRecordingRadioButton";
            this.IsRecordingRadioButton.Size = new System.Drawing.Size(14, 13);
            this.IsRecordingRadioButton.TabIndex = 7;
            this.IsRecordingRadioButton.TabStop = true;
            this.IsRecordingRadioButton.UseVisualStyleBackColor = true;
            // 
            // IntensityContainerAverageTextBox
            // 
            this.IntensityContainerAverageTextBox.Location = new System.Drawing.Point(491, 392);
            this.IntensityContainerAverageTextBox.Name = "IntensityContainerAverageTextBox";
            this.IntensityContainerAverageTextBox.Size = new System.Drawing.Size(297, 20);
            this.IntensityContainerAverageTextBox.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.IntensityContainerAverageTextBox);
            this.Controls.Add(this.IsRecordingRadioButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.DifferencePictureBox);
            this.Controls.Add(this.OneSecondIntervalPictureBox);
            this.Controls.Add(this.DefaultCameraPictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.DefaultCameraPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OneSecondIntervalPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DifferencePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox DefaultCameraPictureBox;
        private System.Windows.Forms.PictureBox OneSecondIntervalPictureBox;
        private System.Windows.Forms.PictureBox DifferencePictureBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton IsRecordingRadioButton;
        private System.Windows.Forms.TextBox IntensityContainerAverageTextBox;
    }
}

