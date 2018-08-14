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
    class EuphoricSlider : Panel
    {
        public double _Minimum = 0;
        public double _Maximum = 100;
        public double _Value = 50;
        public int _RoundPlaces = 1;
        public bool _Round = true;
        public Color _BackSliderColor = Color.FromArgb(25, 25, 25);
        public Color _ForeSliderColor = Color.FromArgb(181, 62, 66);
        public Color _TextColor = Color.FromArgb(255, 255, 255);

        #region Properties
        [Category(".Euphoric")]
        public int DecimalPlaces
        {
            get { return _RoundPlaces; }
            set
            {
                if (value < 0)
                    throw new Exception("Value is Too Low");

                _RoundPlaces = value;
            }
        }

        [Category(".Euphoric")]
        public bool Round
        {
            get { return _Round; }
            set { _Round = value; }
        }

        [Category(".Euphoric")]
        public double Minimum
        {
            get { return _Minimum; }
            set
            {
                if (value > this._Value)
                    throw new Exception("Value is Too High");

                _Minimum = value;
            }
        }

        [Category(".Euphoric")]
        public double Maximum
        {
            get { return _Maximum; }
            set
            {
                _Maximum = value;
            }
        }

        [Category(".Euphoric")]
        public double Value
        {
            get { return _Value; }
            set
            {
                if (value > this._Maximum)
                    throw new Exception("Value is Too High");

                if (value < this._Minimum)
                    throw new Exception("Value is Too Low");

                _Value = value;
                this.Refresh();
            }
        }

        [Category(".Euphoric")]
        public Color SliderBackColor
        {
            get { return _BackSliderColor; }
            set { _BackSliderColor = value; }
        }

        [Category(".Euphoric")]
        public Color SliderForeColor
        {
            get { return _ForeSliderColor; }
            set { _ForeSliderColor = value; }
        }

        [Category(".Euphoric")]
        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; }
        }
        #endregion

        public EuphoricSlider()
        {
            Size = new Size(100, 15);
            this.DoubleBuffered = true;
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.Transparent;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                Point curPos = this.PointToClient(Cursor.Position);
                double value = this._Minimum + (this._Maximum - this._Minimum) * curPos.X / this.Width;

                if (value < this._Minimum)
                    value = this._Minimum;

                if (value > this._Maximum)
                    value = this._Maximum;

                if (_Round) this._Value = Math.Round(value);
                else this._Value = value;

                this.Refresh();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                Point curPos = this.PointToClient(Cursor.Position);
                double value = this._Minimum + (this._Maximum - this._Minimum) * curPos.X / this.Width;

                if (value < this._Minimum)
                    value = this._Minimum;

                if (value > this._Maximum)
                    value = this._Maximum;

                if (_Round) this._Value = Math.Round(value);
                else this._Value = value;

                this.Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            double unit = this.Width / (this._Maximum - this.Minimum);

            using (Brush b = new SolidBrush(this._BackSliderColor))
                e.Graphics.FillRectangle(b, new Rectangle(0, 0, this.Width, this.Height));

            using (Brush b = new SolidBrush(this.SliderForeColor))
                e.Graphics.FillRectangle(b, 2, 2, (int)((_Value-Minimum) * unit) - 4, this.Height - 4);


            using (Brush b = new SolidBrush(this._TextColor))
            {
                SizeF sizeF = e.Graphics.MeasureString(Math.Round(this._Value, _RoundPlaces).ToString(), this.Font);

                if ((int)((_Value - Minimum) * unit) - (sizeF.Width / 2) <= 0)
                    e.Graphics.DrawString(Math.Round(this._Value, _RoundPlaces).ToString(), this.Font, b, 0, (this.Height / 2) - (sizeF.Height / 2) + 1);
                else if ((int)((_Value - Minimum) * unit) + (sizeF.Width / 2) >= this.Width)
                    e.Graphics.DrawString(Math.Round(this._Value, _RoundPlaces).ToString(), this.Font, b, this.Width - sizeF.Width, (this.Height / 2) - (sizeF.Height / 2) + 1);
                else
                    e.Graphics.DrawString(Math.Round(this._Value, _RoundPlaces).ToString(), this.Font, b, (int)((_Value - Minimum) * unit) - (sizeF.Width / 2), (this.Height / 2) - (sizeF.Height / 2) + 1);
            }
        }
    }
}
