namespace PatchIT.Updater
{
    partial class Patcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Patcher));
            this.TitleBar = new System.Windows.Forms.Panel();
            this.ClosePicture = new System.Windows.Forms.PictureBox();
            this.LabelTitle = new System.Windows.Forms.Label();
            this.GbProgress = new System.Windows.Forms.GroupBox();
            this.LabelSpeed = new System.Windows.Forms.Label();
            this.LabelOverall = new System.Windows.Forms.Label();
            this.LabelStep = new System.Windows.Forms.Label();
            this.LabelOverallProgress = new System.Windows.Forms.Label();
            this.LabelCurrentOperation = new System.Windows.Forms.Label();
            this.ProgressBarOverall = new System.Windows.Forms.ProgressBar();
            this.ProgressBarStep = new System.Windows.Forms.ProgressBar();
            this.GbDetails = new System.Windows.Forms.GroupBox();
            this.TbLog = new System.Windows.Forms.RichTextBox();
            this.TitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClosePicture)).BeginInit();
            this.GbProgress.SuspendLayout();
            this.GbDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // TitleBar
            // 
            this.TitleBar.BackColor = System.Drawing.Color.DimGray;
            this.TitleBar.Controls.Add(this.ClosePicture);
            this.TitleBar.Controls.Add(this.LabelTitle);
            this.TitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(828, 36);
            this.TitleBar.TabIndex = 0;
            this.TitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleBar_MouseDown);
            // 
            // ClosePicture
            // 
            this.ClosePicture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClosePicture.Image = global::PatchIT.Updater.Properties.Resources.Close2;
            this.ClosePicture.Location = new System.Drawing.Point(793, 1);
            this.ClosePicture.Name = "ClosePicture";
            this.ClosePicture.Size = new System.Drawing.Size(32, 32);
            this.ClosePicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ClosePicture.TabIndex = 19;
            this.ClosePicture.TabStop = false;
            this.ClosePicture.Click += new System.EventHandler(this.ClosePicture_Click);
            // 
            // LabelTitle
            // 
            this.LabelTitle.AutoSize = true;
            this.LabelTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTitle.ForeColor = System.Drawing.Color.Snow;
            this.LabelTitle.Location = new System.Drawing.Point(14, 6);
            this.LabelTitle.Name = "LabelTitle";
            this.LabelTitle.Size = new System.Drawing.Size(239, 22);
            this.LabelTitle.TabIndex = 0;
            this.LabelTitle.Text = "PatchIT - Updater Software";
            // 
            // GbProgress
            // 
            this.GbProgress.Controls.Add(this.LabelSpeed);
            this.GbProgress.Controls.Add(this.LabelOverall);
            this.GbProgress.Controls.Add(this.LabelStep);
            this.GbProgress.Controls.Add(this.LabelOverallProgress);
            this.GbProgress.Controls.Add(this.LabelCurrentOperation);
            this.GbProgress.Controls.Add(this.ProgressBarOverall);
            this.GbProgress.Controls.Add(this.ProgressBarStep);
            this.GbProgress.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.GbProgress.Location = new System.Drawing.Point(12, 42);
            this.GbProgress.Name = "GbProgress";
            this.GbProgress.Size = new System.Drawing.Size(804, 120);
            this.GbProgress.TabIndex = 1;
            this.GbProgress.TabStop = false;
            this.GbProgress.Text = "Patching Progress";
            // 
            // LabelSpeed
            // 
            this.LabelSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSpeed.Location = new System.Drawing.Point(564, 25);
            this.LabelSpeed.Name = "LabelSpeed";
            this.LabelSpeed.Size = new System.Drawing.Size(234, 13);
            this.LabelSpeed.TabIndex = 19;
            this.LabelSpeed.Text = "00000 / 00000 Kb (Speed: 0.00 Kb/s)";
            this.LabelSpeed.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelOverall
            // 
            this.LabelOverall.AutoSize = true;
            this.LabelOverall.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelOverall.Location = new System.Drawing.Point(117, 74);
            this.LabelOverall.Name = "LabelOverall";
            this.LabelOverall.Size = new System.Drawing.Size(52, 13);
            this.LabelOverall.TabIndex = 18;
            this.LabelOverall.Text = "Initializing";
            // 
            // LabelStep
            // 
            this.LabelStep.AutoSize = true;
            this.LabelStep.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelStep.Location = new System.Drawing.Point(117, 24);
            this.LabelStep.Name = "LabelStep";
            this.LabelStep.Size = new System.Drawing.Size(69, 13);
            this.LabelStep.TabIndex = 17;
            this.LabelStep.Text = "Starting Up...";
            // 
            // LabelOverallProgress
            // 
            this.LabelOverallProgress.AutoSize = true;
            this.LabelOverallProgress.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelOverallProgress.Location = new System.Drawing.Point(6, 74);
            this.LabelOverallProgress.Name = "LabelOverallProgress";
            this.LabelOverallProgress.Size = new System.Drawing.Size(91, 14);
            this.LabelOverallProgress.TabIndex = 16;
            this.LabelOverallProgress.Text = "Overall Progress:";
            // 
            // LabelCurrentOperation
            // 
            this.LabelCurrentOperation.AutoSize = true;
            this.LabelCurrentOperation.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelCurrentOperation.Location = new System.Drawing.Point(6, 24);
            this.LabelCurrentOperation.Name = "LabelCurrentOperation";
            this.LabelCurrentOperation.Size = new System.Drawing.Size(96, 14);
            this.LabelCurrentOperation.TabIndex = 15;
            this.LabelCurrentOperation.Text = "Current Operation:";
            // 
            // ProgressBarOverall
            // 
            this.ProgressBarOverall.Location = new System.Drawing.Point(6, 91);
            this.ProgressBarOverall.Name = "ProgressBarOverall";
            this.ProgressBarOverall.Size = new System.Drawing.Size(792, 23);
            this.ProgressBarOverall.TabIndex = 1;
            // 
            // ProgressBarStep
            // 
            this.ProgressBarStep.Location = new System.Drawing.Point(6, 41);
            this.ProgressBarStep.Name = "ProgressBarStep";
            this.ProgressBarStep.Size = new System.Drawing.Size(792, 23);
            this.ProgressBarStep.TabIndex = 0;
            // 
            // GbDetails
            // 
            this.GbDetails.Controls.Add(this.TbLog);
            this.GbDetails.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.GbDetails.Location = new System.Drawing.Point(12, 168);
            this.GbDetails.Name = "GbDetails";
            this.GbDetails.Size = new System.Drawing.Size(804, 257);
            this.GbDetails.TabIndex = 2;
            this.GbDetails.TabStop = false;
            this.GbDetails.Text = "Update Details";
            // 
            // TbLog
            // 
            this.TbLog.Location = new System.Drawing.Point(6, 19);
            this.TbLog.Name = "TbLog";
            this.TbLog.ReadOnly = true;
            this.TbLog.Size = new System.Drawing.Size(792, 232);
            this.TbLog.TabIndex = 0;
            this.TbLog.Text = "";
            // 
            // Patcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(828, 437);
            this.Controls.Add(this.GbDetails);
            this.Controls.Add(this.GbProgress);
            this.Controls.Add(this.TitleBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Patcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Patcher_FormClosing);
            this.Shown += new System.EventHandler(this.Patcher_Shown);
            this.TitleBar.ResumeLayout(false);
            this.TitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClosePicture)).EndInit();
            this.GbProgress.ResumeLayout(false);
            this.GbProgress.PerformLayout();
            this.GbDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TitleBar;
        private System.Windows.Forms.GroupBox GbProgress;
        private System.Windows.Forms.GroupBox GbDetails;
        private System.Windows.Forms.ProgressBar ProgressBarOverall;
        private System.Windows.Forms.ProgressBar ProgressBarStep;
        private System.Windows.Forms.RichTextBox TbLog;
        private System.Windows.Forms.Label LabelTitle;
        private System.Windows.Forms.Label LabelCurrentOperation;
        private System.Windows.Forms.Label LabelStep;
        private System.Windows.Forms.Label LabelOverallProgress;
        private System.Windows.Forms.Label LabelOverall;
        private System.Windows.Forms.PictureBox ClosePicture;
        private System.Windows.Forms.Label LabelSpeed;
    }
}

