using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Darc_Euphoria.Euphoric.Config;

namespace Darc_Euphoria.Euphoric.Controls
{
    internal class EuphoricSlider : Panel
    {
        public Color _BackSliderColor = Color.FromArgb(25, 25, 25);
        public Color _ForeSliderColor = Color.FromArgb(181, 62, 66);
        public double _Maximum = 100;
        public double _Minimum;
        public bool _Round = true;
        public int _RoundPlaces = 1;
        public Color _TextColor = Color.FromArgb(255, 255, 255);
        public double _Value = 50;

        public EuphoricSlider()
        {
            Size = new Size(100, 15);
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            ForeColor = Color.Transparent;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                var curPos = PointToClient(Cursor.Position);
                var value = _Minimum + (_Maximum - _Minimum) * curPos.X / Width;

                if (value < _Minimum)
                    value = _Minimum;

                if (value > _Maximum)
                    value = _Maximum;

                if (_Round) _Value = Math.Round(value);
                else _Value = value;

                Refresh();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                var curPos = PointToClient(Cursor.Position);
                var value = _Minimum + (_Maximum - _Minimum) * curPos.X / Width;

                if (value < _Minimum)
                    value = _Minimum;

                if (value > _Maximum)
                    value = _Maximum;

                if (_Round) _Value = Math.Round(value);
                else _Value = value;

                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var unit = Width / (_Maximum - Minimum);

            using (Brush b = new SolidBrush(_BackSliderColor))
            {
                e.Graphics.FillRectangle(b, new Rectangle(0, 0, Width, Height));
            }

            using (Brush b = new SolidBrush(Settings.userSettings.VisualColors.Menu_Primary_Color))
            {
                e.Graphics.FillRectangle(b, 2, 2, (int) ((_Value - Minimum) * unit) - 4, Height - 4);
            }


            using (Brush b = new SolidBrush(_TextColor))
            {
                var sizeF = e.Graphics.MeasureString(Math.Round(_Value, _RoundPlaces).ToString(), Font);

                if ((int) ((_Value - Minimum) * unit) - sizeF.Width / 2 <= 0)
                    e.Graphics.DrawString(Math.Round(_Value, _RoundPlaces).ToString(), Font, b, 0,
                        Height / 2 - sizeF.Height / 2 + 1);
                else if ((int) ((_Value - Minimum) * unit) + sizeF.Width / 2 >= Width)
                    e.Graphics.DrawString(Math.Round(_Value, _RoundPlaces).ToString(), Font, b, Width - sizeF.Width,
                        Height / 2 - sizeF.Height / 2 + 1);
                else
                    e.Graphics.DrawString(Math.Round(_Value, _RoundPlaces).ToString(), Font, b,
                        (int) ((_Value - Minimum) * unit) - sizeF.Width / 2, Height / 2 - sizeF.Height / 2 + 1);
            }
        }

        #region Properties

        [Category(".Euphoric")]
        public int DecimalPlaces
        {
            get => _RoundPlaces;
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
            get => _Round;
            set => _Round = value;
        }

        [Category(".Euphoric")]
        public double Minimum
        {
            get => _Minimum;
            set
            {
                if (value > _Value)
                    throw new Exception("Value is Too High");

                _Minimum = value;
            }
        }

        [Category(".Euphoric")]
        public double Maximum
        {
            get => _Maximum;
            set => _Maximum = value;
        }

        [Category(".Euphoric")]
        public double Value
        {
            get => _Value;
            set
            {
                if (value > _Maximum)
                    throw new Exception("Value is Too High");

                if (value < _Minimum)
                    throw new Exception("Value is Too Low");

                _Value = value;
                Refresh();
            }
        }

        [Category(".Euphoric")]
        public Color SliderBackColor
        {
            get => _BackSliderColor;
            set => _BackSliderColor = value;
        }

        [Category(".Euphoric")]
        public Color SliderForeColor
        {
            get => _ForeSliderColor;
            set => _ForeSliderColor = value;
        }

        [Category(".Euphoric")]
        public Color TextColor
        {
            get => _TextColor;
            set => _TextColor = value;
        }

        #endregion
    }
}