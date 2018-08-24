namespace Darc_Euphoria
{
    partial class wnd_Radar
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.radar = new Darc_Euphoria.Euphoric.Controls.EuphoricPanel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(2, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Radar";
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.wnd_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // radar
            // 
            this.radar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.radar.Location = new System.Drawing.Point(3, 25);
            this.radar.Name = "radar";
            this.radar.Size = new System.Drawing.Size(200, 200);
            this.radar.TabIndex = 0;
            this.radar.Paint += new System.Windows.Forms.PaintEventHandler(this.radar_Paint);
            this.radar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.wnd_MouseDown);
            // 
            // wnd_Radar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(181)))), ((int)(((byte)(62)))), ((int)(((byte)(66)))));
            this.ClientSize = new System.Drawing.Size(206, 228);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radar);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "wnd_Radar";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "wnd_Radar";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.wnd_Radar_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.wnd_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Euphoric.Controls.EuphoricPanel radar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}