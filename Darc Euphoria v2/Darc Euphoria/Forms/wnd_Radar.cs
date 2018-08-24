using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;
using Darc_Euphoria.Hacks;

namespace Darc_Euphoria
{
    public partial class wnd_Radar : Form
    {
        private Point center;

        public wnd_Radar()
        {
            InitializeComponent();

            if (!Sig.Init())
            {
                MessageBox.Show("Failed to Load!\nMake Sure You Have CS:GO Started Before Launching.");
                Environment.Exit(Environment.ExitCode);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void wnd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void wnd_Radar_Load(object sender, EventArgs e)
        {
            Location = gvar.OverlayPoint;
            Left += 21;
            Top += 50;
        }

        private void radar_Paint(object sender, PaintEventArgs e)
        {
            center = new Point(radar.Width / 2, radar.Height / 2);
            using (var p = new Pen(Color.FromArgb(50, 50, 50)))
            {
                e.Graphics.DrawLine(p, radar.Width / 2, 0, radar.Width / 2, radar.Height);
                e.Graphics.DrawLine(p, 0, radar.Height / 2, radar.Width, radar.Height / 2);
            }

            if (!Local.InGame) return;
            try
            {
                foreach (var player in EntityList.List)
                {
                    if (player.Dormant) continue;
                    if (player.Health <= 0) continue;

                    var dist = MathFuncs.LocationToPlayer(Local.Position, player.Position) *
                               (float) Settings.userSettings.MiscSettings.RadarZoom;
                    dist.x += center.X;
                    dist.y += center.Y;

                    Brush b = player.Team == Local.Team ? new SolidBrush(Color.Green) : new SolidBrush(Color.Red);

                    var coord = RotatePoint(
                        new Point((int) dist.y, (int) dist.x),
                        Local.ViewAngle.x);

                    e.Graphics.FillRectangle(b, coord.X - 2, coord.Y - 2, 5, 5);

                    b.Dispose();
                }
            }
            catch
            {
            }
        }

        private Point RotatePoint(Point pointToRotate, float angle, bool angleInRadians = false)
        {
            if (!angleInRadians)
                angle = (float) (angle * (Math.PI / 180f));
            var cosTheta = (float) Math.Cos(angle);
            var sinTheta = (float) Math.Sin(angle);
            var returnVec = new Point(
                (int) (cosTheta * (pointToRotate.X - center.X) - sinTheta * (pointToRotate.Y - center.Y)),
                (int) (sinTheta * (pointToRotate.X - center.X) + cosTheta * (pointToRotate.Y - center.Y))
            );
            returnVec.X += center.X;
            returnVec.Y += center.Y;
            return returnVec;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Settings.userSettings.MiscSettings.Radar) Show();
            else Hide();
            BackColor = Settings.userSettings.VisualColors.Menu_Primary_Color;
            radar.Width = Settings.userSettings.MiscSettings.RadarSize * 4;
            radar.Height = Settings.userSettings.MiscSettings.RadarSize * 4;
            Width = radar.Width + 6;
            Height = radar.Height + 28;
            radar.Invalidate();
        }
    }
}