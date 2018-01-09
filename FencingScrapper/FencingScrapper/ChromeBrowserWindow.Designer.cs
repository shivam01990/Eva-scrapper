namespace FencingScrapper
{
    partial class ChromeBrowserWindow
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
            this.SuspendLayout();
            // 
            // btnScrapData
            // 
            this.btnScrapData.Location = new System.Drawing.Point(435, 12);
            this.btnScrapData.Name = "btnScrapData";
            this.btnScrapData.Size = new System.Drawing.Size(75, 23);
            this.btnScrapData.TabIndex = 2;
            this.btnScrapData.Text = "Scrap Data";
            this.btnScrapData.UseVisualStyleBackColor = true;
            this.btnScrapData.Click += new System.EventHandler(this.btnScrapData_Click);
            // 
            // ChromeBrowserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.btnScrapData);
            this.Name = "ChromeBrowserWindow";
            this.Text = "ChromeBrowserWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnScrapData;
    }
}