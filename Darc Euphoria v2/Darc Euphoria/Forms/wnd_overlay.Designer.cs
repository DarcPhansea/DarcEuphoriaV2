namespace Darc_Euphoria
{
    partial class wnd_overlay
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
            this.updateWnd = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // updateWnd
            // 
            this.updateWnd.DoWork += new System.ComponentModel.DoWorkEventHandler(this.updateWnd_DoWork);
            // 
            // wnd_overlay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(100, 100);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "wnd_overlay";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "wnd_overlay";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Black;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.wnd_overlay_FormClosing);
            this.Resize += new System.EventHandler(this.wnd_overlay_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker updateWnd;
    }
}