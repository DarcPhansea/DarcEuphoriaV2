using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Darc_Euphoria.Euphoric.Controls
{
    public class KurumiClock : Panel
    {
        List<Image> Images = new List<Image>();

        private int Hour = 0;
        private int Minute = 0;
        private int Second = 0;
        private int MiliSeconds = 0;

        private Timer Clock = new Timer();

        public KurumiClock()
        {
            this.DoubleBuffered = true;
            this.Resize += KurumiClock_Resize;
            this.Height = this.Width = 150;

            this.Paint += KurumiClock_Paint;

            this.Clock.Tick += Clock_Tick;
            this.Clock.Interval = 1;
            this.Clock.Start();
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            this.UpdateTime();
        }

        public void UpdateTime()
        {
            Hour = DateTime.Now.Hour;
            Minute = DateTime.Now.Minute;
            Second = DateTime.Now.Second;
            MiliSeconds = DateTime.Now.Millisecond;
            this.Refresh();
        }

        private void KurumiClock_Resize(object sender, EventArgs e)
        {
            this.Width = this.Height;
            Images.Clear();
            Images.Add(new Bitmap(Properties.Resources.Clock2, this.Size));
            Images.Add(new Bitmap(Properties.Resources.HourHand, this.Size));
            Images.Add(new Bitmap(Properties.Resources.MinuteHand, this.Size));
            Images.Add(new Bitmap(Properties.Resources.SecondHand, this.Size));
            this.Refresh();
        }

        private void KurumiClock_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                float sec = (Second * 6) + (MiliSeconds * .006f);
                float min = (Minute * 6) + (Second * .1f);
                float hr = (Hour * 30) + ((Minute + Second / 60f) * .5f);

                e.Graphics.DrawImage(TimeCalc(Images[0], 0), 0, 0);
                e.Graphics.DrawImage(TimeCalc(Images[1], hr), 0, hr > 180 ? -1 : 0);
                e.Graphics.DrawImage(TimeCalc(Images[2], min), 0, min > 180 ? -1 : 0);
                e.Graphics.DrawImage(TimeCalc(Images[3], sec), 0, sec > 180 ? -1 : 0);
            }
            catch
            {
                Images.Clear();
                Images.Add(new Bitmap(Properties.Resources.Clock2, this.Size));
                Images.Add(new Bitmap(Properties.Resources.HourHand, this.Size));
                Images.Add(new Bitmap(Properties.Resources.MinuteHand, this.Size));
                Images.Add(new Bitmap(Properties.Resources.SecondHand, this.Size));
                this.Refresh();
            }
        }

        private Bitmap TimeCalc(Image img, float rotation)
        {
            Bitmap bit = new Bitmap(img.Width, img.Height);
            using (Graphics gfx = Graphics.FromImage(bit))
            {
                gfx.TranslateTransform(bit.Width / 2, bit.Height / 2);
                gfx.RotateTransform(rotation);
                gfx.TranslateTransform(-bit.Width / 2, -bit.Height / 2);
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gfx.DrawImage(img, 0, 0);
            }
            //bit.RotateFlip(RotateFlipType.Rotate180FlipY); reverse the orientation
            return bit;
        }
    }
}
