﻿namespace Gwent_App
{
    partial class StartForm
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
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Sienna;
            button1.BackgroundImage = Properties.Resources.grajbutton;
            button1.BackgroundImageLayout = ImageLayout.Stretch;
            button1.Location = new Point(216, 620);
            button1.Name = "button1";
            button1.Size = new Size(340, 68);
            button1.TabIndex = 0;
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.Sienna;
            button2.BackgroundImage = Properties.Resources.rejestracjaprzyciisk;
            button2.BackgroundImageLayout = ImageLayout.Stretch;
            button2.Location = new Point(216, 675);
            button2.Name = "button2";
            button2.Size = new Size(340, 62);
            button2.TabIndex = 1;
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(768, 768);
            Controls.Add(button2);
            Controls.Add(button1);
            Name = "StartForm";
            Text = "Gwent";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button button2;
    }
}