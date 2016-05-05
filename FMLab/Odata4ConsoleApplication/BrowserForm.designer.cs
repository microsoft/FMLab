namespace Odata4ConsoleApplication
{
    partial class BrowserForm
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
            this.Browser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // Browser
            // 
            this.Browser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Browser.Location = new System.Drawing.Point(0, 0);
            this.Browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.Browser.Name = "Browser";
            //this.Browser.ScrollBarsEnabled = false;
            this.Browser.Size = new System.Drawing.Size(903, 403);
            this.Browser.TabIndex = 0;
            this.Browser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.WebBrowser_Navigating);
            // 
            // BrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 403);
            this.Controls.Add(this.Browser);
            this.Name = "BrowserForm";
            this.Text = "BrowserForm";
            this.Load += new System.EventHandler(this.BrowserForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(BrowserForm_Closing);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.WebBrowser Browser;
    }
}