using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Darc_Euphoria.Properties;

namespace Darc_Euphoria.Euphoric.Controls
{
    public class KurumiClock : Panel
    {
        private readonly Timer Clock = new Timer();
        private readonly List<Image> Images = new List<Image>();

        private int Hour;
        private int MiliSeconds;
        private int Minute;
        private int Second;

        public KurumiClock()
        {
            DoubleBuffered = true;
            Resize += KurumiClock_Resize;
            Height = Width = 150;

            Paint += KurumiClock_Paint;

            Clock.Tick += Clock_Tick;
            Clock.Interval = 1;
            Clock.Start();
        }

        private void Clock_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        public void UpdateTime()
        {
            Hour = DateTime.Now.Hour;
            Minute = DateTime.Now.Minute;
            Second = DateTime.Now.Second;
            MiliSeconds = DateTime.Now.Millisecond;
            Refresh();
        }

        private void KurumiClock_Resize(object sender, EventArgs e)
        {
            Width = Height;
            Images.Clear();
            Images.Add(new Bitmap(Resources.Clock2, Size));
            Images.Add(new Bitmap(Resources.HourHand, Size));
            Images.Add(new Bitmap(Resources.MinuteHand, Size));
            Images.Add(new Bitmap(Resources.SecondHand, Size));
            Refresh();
        }

        private void KurumiClock_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                var sec = Second * 6 + MiliSeconds * .006f;
                var min = Minute * 6 + Second * .1f;
                var hr = Hour * 30 + (Minute + Second / 60f) * .5f;

                e.Graphics.DrawImage(TimeCalc(Images[0], 0), 0, 0);
                e.Graphics.DrawImage(TimeCalc(Images[1], hr), 0, hr > 180 ? -1 : 0);
                e.Graphics.DrawImage(TimeCalc(Images[2], min), 0, min > 180 ? -1 : 0);
                e.Graphics.DrawImage(TimeCalc(Images[3], sec), 0, sec > 180 ? -1 : 0);
            }
            catch
            {
                Images.Clear();
                Images.Add(new Bitmap(Resources.Clock2, Size));
                Images.Add(new Bitmap(Resources.HourHand, Size));
                Images.Add(new Bitmap(Resources.MinuteHand, Size));
                Images.Add(new Bitmap(Resources.SecondHand, Size));
                Refresh();
            }
        }

        private Bitmap TimeCalc(Image img, float rotation)
        {
            var bit = new Bitmap(img.Width, img.Height);
            using (var gfx = Graphics.FromImage(bit))
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