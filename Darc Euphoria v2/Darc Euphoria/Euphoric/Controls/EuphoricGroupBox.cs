using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Darc_Euphoria.Euphoric.Controls
{
    class EuphoricGroupBox : GroupBox
    {
        public enum EuphoricHeaderPostion
        {
            TopLeft,
            TopMiddle,
            TopRight,
        }

        public EuphoricHeaderPostion _HeaderPostion = EuphoricHeaderPostion.TopLeft;

        [Category(".Euphoric")]
        public EuphoricHeaderPostion headerPostion
        {
            get { return _HeaderPostion; }
            set { _HeaderPostion = value; }
        }

        public EuphoricGroupBox()
        {
            this.DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SizeF textSize = e.Graphics.MeasureString(this.Text, this.Font);

            using (Brush b = new SolidBrush(this.BackColor))
                e.Graphics.FillRectangle(b, 0,0, this.Width, this.Height);

            Rectangle rect = new Rectangle(0, (int)textSize.Height / 2, this.Width - 1, this.Height - (int)textSize.Height / 2 - 1);
            using (Pen b = new Pen(this.ForeColor))
                e.Graphics.DrawRectangle(b, rect);


            //e.Graphics.DrawRectangle(b, 0, textSize.Height / 2, this.Width - 1, this.Height - textSize.Height / 2 - 1);

            Rectangle fillRec = new Rectangle(
                0,
                (int)(textSize.Height * 1.5),
                this.Width,
                this.Height - (int)(textSize.Height * 2.5f));


            using (Brush b = new SolidBrush(this.BackColor))
                e.Graphics.FillRectangle(b, fillRec);


            using (Brush b = new SolidBrush(this.ForeColor))
            {
                if (_HeaderPostion == EuphoricHeaderPostion.TopLeft)
                {
                    using (Brush bb = new SolidBrush(this.BackColor))
                        e.Graphics.FillRectangle(bb, 5, 0, textSize.Width, textSize.Height);

                    e.Graphics.DrawString(this.Text, this.Font, b, 5, 0);
                }
                else if (_HeaderPostion == EuphoricHeaderPostion.TopMiddle)
                {
                    int pos = (int)((this.Width / 2) - (textSize.Width / 2));

                    using (Brush bb = new SolidBrush(this.BackColor))
                        e.Graphics.FillRectangle(bb, pos, 0, textSize.Width, textSize.Height);

                    e.Graphics.DrawString(this.Text, this.Font, b, pos, 0);
                }
                else if (_HeaderPostion == EuphoricHeaderPostion.TopRight)
                {
                    int pos = (int)(this.Width - textSize.Width - 5);

                    using (Brush bb = new SolidBrush(this.BackColor))
                        e.Graphics.FillRectangle(bb, pos, 0, textSize.Width, textSize.Height);

                    e.Graphics.DrawString(this.Text, this.Font, b, pos, 0);
                }
            }
        }
    }
}
