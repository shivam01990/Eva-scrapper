namespace FencingScrapper
{
    partial class MainWindow
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
            this.btnScrapData = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.webBrowserControl = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // btnScrapData
            // 
            this.btnScrapData.Location = new System.Drawing.Point(469, 12);
            this.btnScrapData.Name = "btnScrapData";
            this.btnScrapData.Size = new System.Drawing.Size(75, 23);
            this.btnScrapData.TabIndex = 1;
            this.btnScrapData.Text = "Scrap Data";
            this.btnScrapData.UseVisualStyleBackColor = true;
            this.btnScrapData.Click += new System.EventHandler(this.btnScrapData_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(586, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 2;
            // 
            // webBrowserControl
            // 
            this.webBrowserControl.Location = new System.Drawing.Point(12, 64);
            this.webBrowserControl.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserControl.Name = "webBrowserControl";
            this.webBrowserControl.Size = new System.Drawing.Size(960, 657);
            this.webBrowserControl.TabIndex = 3;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 733);
            this.Controls.Add(this.webBrowserControl);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnScrapData);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scrapper App";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnScrapData;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.WebBrowser webBrowserControl;
    }
}