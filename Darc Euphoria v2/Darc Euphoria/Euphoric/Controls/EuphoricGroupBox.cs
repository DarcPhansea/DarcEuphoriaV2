using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Darc_Euphoria.Euphoric.Controls
{
    internal class EuphoricGroupBox : GroupBox
    {
        public enum EuphoricHeaderPostion
        {
            TopLeft,
            TopMiddle,
            TopRight
        }

        public EuphoricHeaderPostion _HeaderPostion = EuphoricHeaderPostion.TopLeft;

        public EuphoricGroupBox()
        {
            DoubleBuffered = true;
        }

        [Category(".Euphoric")]
        public EuphoricHeaderPostion headerPostion
        {
            get => _HeaderPostion;
            set => _HeaderPostion = value;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var textSize = e.Graphics.MeasureString(Text, Font);

            using (Brush b = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(b, 0, 0, Width, Height);
            }

            if (Text == "")
            {
                using (var b = new Pen(ForeColor))
                {
                    e.Graphics.DrawRectangle(b, 0, 0, Width - 1, Height - 1);
                }

                return;
            }


            var rect = new Rectangle(0, (int) textSize.Height / 2, Width - 1, Height - (int) textSize.Height / 2 - 1);
            using (var b = new Pen(ForeColor))
            {
                e.Graphics.DrawRectangle(b, rect);
            }


            //e.Graphics.DrawRectangle(b, 0, textSize.Height / 2, this.Width - 1, this.Height - textSize.Height / 2 - 1);

            var fillRec = new Rectangle(
                0,
                (int) (textSize.Height * 1.5),
                Width,
                Height - (int) (textSize.Height * 2.5f));


            using (Brush b = new SolidBrush(BackColor))
            {
                e.Graphics.FillRectangle(b, fillRec);
            }


            using (Brush b = new SolidBrush(ForeColor))
            {
                if (_HeaderPostion == EuphoricHeaderPostion.TopLeft)
                {
                    using (Brush bb = new SolidBrush(BackColor))
                    {
                        e.Graphics.FillRectangle(bb, 5, 0, textSize.Width, textSize.Height);
                    }

                    e.Graphics.DrawString(Text, Font, b, 5, 0);
                }
                else if (_HeaderPostion == EuphoricHeaderPostion.TopMiddle)
                {
                    var pos = (int) (Width / 2 - textSize.Width / 2);

                    using (Brush bb = new SolidBrush(BackColor))
                    {
                        e.Graphics.FillRectangle(bb, pos, 0, textSize.Width, textSize.Height);
                    }

                    e.Graphics.DrawString(Text, Font, b, pos, 0);
                }
                else if (_HeaderPostion == EuphoricHeaderPostion.TopRight)
                {
                    var pos = (int) (Width - textSize.Width - 5);

                    using (Brush bb = new SolidBrush(BackColor))
                    {
                        e.Graphics.FillRectangle(bb, pos, 0, textSize.Width, textSize.Height);
                    }

                    e.Graphics.DrawString(Text, Font, b, pos, 0);
                }
            }
        }
    }
}