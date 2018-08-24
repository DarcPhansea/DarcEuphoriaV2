﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Darc_Euphoria.Euphoric.Controls
{
    public partial class EuphoricColorPicker : UserControl
    {
        private readonly Graphics _BufferGraphic;
        private readonly zBitmap _RedzBitmap;
        private readonly zBitmap _TranszBitmap;

        private readonly zBitmap _zBitmap;
        private int _A = 255;
        private int _B = 200;
        private int _G;
        private int _R = 120;
        private Color _SelectedColor;

        public EuphoricColorPicker()
        {
            InitializeComponent();

            _zBitmap = new zBitmap(panel_ColorPicker.Width, panel_ColorPicker.Height);
            _BufferGraphic = Graphics.FromImage(_zBitmap.Bitmap);
            _RedzBitmap = new zBitmap(panel_RedSlider.Width, panel_RedSlider.Height);
            _TranszBitmap = new zBitmap(panel_opacity.Width, panel_opacity.Height);


            textBox1.KeyPress += TextBox1_KeyPress;
            textBox2.KeyPress += TextBox1_KeyPress;
            textBox3.KeyPress += TextBox1_KeyPress;
            textBox4.KeyPress += TextBox1_KeyPress;
            textBox1.TextChanged += TextBox1_TextChanged;
            textBox2.TextChanged += TextBox1_TextChanged;
            textBox3.TextChanged += TextBox1_TextChanged;
            textBox4.TextChanged += TextBox1_TextChanged;
            DoubleBuffered = true;
            panel1.Paint += EuphoricColorPicker_Paint;
            BackColor = Color.Transparent;
            panel_RedSlider.Paint += Panel_RedSlider_Paint;
            panel_ColorPicker.Paint += Panel_ColorPicker_Paint;
            panel_opacity.Paint += Panel_opacity_Paint;
            panel_ColorPicker.MouseDown += Panel_ColorPicker_MouseDown;
            panel_ColorPicker.MouseMove += Panel_ColorPicker_MouseMove;
            panel_RedSlider.MouseDown += Panel_RedSlider_MouseDown;
            panel_RedSlider.MouseMove += Panel_RedSlider_MouseMove;
            panel_opacity.MouseDown += Panel_opacity_MouseDown;
            panel_opacity.MouseMove += Panel_opacity_MouseMove;
            UpdateColors();
        }

        public Color SelectedColor
        {
            get => _SelectedColor;
            set
            {
                _A = value.A;
                _R = value.R;
                _G = value.G;
                _B = value.B;
                UpdateColors();
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            var tex = (TextBox) sender;
            if (tex.Text == string.Empty)
            {
                switch (tex.Name)
                {
                    case "textBox1":
                        _A = 0;
                        break;
                    case "textBox2":
                        _R = 0;
                        break;
                    case "textBox3":
                        _G = 0;
                        break;
                    case "textBox4":
                        _B = 0;
                        break;
                    default:
                        break;
                }
            }
            else if (int.Parse(tex.Text) > 255)
            {
                switch (tex.Name)
                {
                    case "textBox1":
                        _A = 255;
                        break;
                    case "textBox2":
                        _R = 255;
                        break;
                    case "textBox3":
                        _G = 255;
                        break;
                    case "textBox4":
                        _B = 255;
                        break;
                    default:
                        break;
                }

                UpdateColors();
            }
            else if (int.Parse(tex.Text) < 0)
            {
                switch (tex.Name)
                {
                    case "textBox1":
                        _A = 0;
                        break;
                    case "textBox2":
                        _R = 0;
                        break;
                    case "textBox3":
                        _G = 0;
                        break;
                    case "textBox4":
                        _B = 0;
                        break;
                    default:
                        break;
                }

                UpdateColors();
            }
            else
            {
                try
                {
                    switch (tex.Name)
                    {
                        case "textBox1":
                            _A = int.Parse(tex.Text);
                            break;
                        case "textBox2":
                            _R = int.Parse(tex.Text);
                            break;
                        case "textBox3":
                            _G = int.Parse(tex.Text);
                            break;
                        case "textBox4":
                            _B = int.Parse(tex.Text);
                            break;
                        default:
                            break;
                    }

                    UpdateColors();
                }
                catch
                {
                    switch (tex.Name)
                    {
                        case "textBox1":
                            _A = 0;
                            break;
                        case "textBox2":
                            _R = 0;
                            break;
                        case "textBox3":
                            _G = 0;
                            break;
                        case "textBox4":
                            _B = 0;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.') e.Handled = true;
        }

        private void Panel_opacity_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (255 - e.Y > 255 || 255 - e.Y < 0)
                return;

            _A = 255 - e.Y;

            UpdateColors();
        }

        private void Panel_opacity_MouseDown(object sender, MouseEventArgs e)
        {
            _A = 255 - e.Y;

            UpdateColors();
        }

        private void Panel_RedSlider_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (255 - e.Y > 255 || 255 - e.Y < 0)
                return;

            _R = 255 - e.Y;

            UpdateColors();
        }

        private void Panel_RedSlider_MouseDown(object sender, MouseEventArgs e)
        {
            _R = 255 - e.Y;

            UpdateColors();
        }

        private void Panel_ColorPicker_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (e.X > 255 || e.X < 0)
                return;

            if (255 - e.Y > 255 || 255 - e.Y < 0)
                return;

            _B = e.X;
            _G = 255 - e.Y;

            UpdateColors();
        }

        private void Panel_ColorPicker_MouseDown(object sender, MouseEventArgs e)
        {
            _B = e.X;
            _G = 255 - e.Y;
            UpdateColors();
        }

        private void UpdateColors()
        {
            #region Hex Value

            var hex = "#";

            if (_R.ToString("X").Length == 1)
                hex += "0" + _R.ToString("X");
            else
                hex += _R.ToString("X");

            if (_G.ToString("X").Length == 1)
                hex += "0" + _G.ToString("X");
            else
                hex += _G.ToString("X");

            if (_B.ToString("X").Length == 1)
                hex += "0" + _B.ToString("X");
            else
                hex += _B.ToString("X");

            HexValue.Text = hex;

            #endregion

            Pen pen = Color.FromArgb(_R, _G, _B).GetBrightness() > 0.3f
                ? pen = new Pen(Color.Black)
                : new Pen(Color.White);

            for (var y = 0; y < _zBitmap.Height; y++)
            for (var x = 0; x < _zBitmap.Width; x++)
                _zBitmap.SetPixel(x, y, Color.FromArgb(_R, 255 - y, x));

            for (var y = 0; y < _RedzBitmap.Height; y++)
            for (var x = 0; x < _RedzBitmap.Width; x++)
                _RedzBitmap.SetPixel(x, y, Color.FromArgb(255 - y, _G, _B));

            for (var y = 0; y < _TranszBitmap.Height; y++)
            for (var x = 0; x < _TranszBitmap.Width; x++)
                _TranszBitmap.SetPixel(x, y, Color.FromArgb(255 - y, y, y, y));

            _BufferGraphic.DrawEllipse(pen, _B - 4, 255 - _G - 4, 7, 7);

            panel_ColorPreview.BackColor = Color.FromArgb(_R, _G, _B);
            _SelectedColor = Color.FromArgb(_A, _R, _G, _B);

            panel1.Refresh();
            panel_ColorPicker.Refresh();
            panel_opacity.Refresh();
            panel_RedSlider.Refresh();
            textBox1.Text = _A.ToString();
            textBox2.Text = _R.ToString();
            textBox3.Text = _G.ToString();
            textBox4.Text = _B.ToString();
        }

        private void Panel_ColorPicker_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(_zBitmap.Bitmap, 0, 0);
        }

        private void Panel_opacity_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImageUnscaled(_TranszBitmap.Bitmap, 0, 0);
        }

        private void Panel_RedSlider_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(_RedzBitmap.Bitmap, 0, 0);
        }

        private void EuphoricColorPicker_Paint(object sender, PaintEventArgs e)
        {
            using (Brush b = new SolidBrush(Color.White))
            {
                if (255 - _R < 253)
                    e.Graphics.FillRectangle(b, 7, 255 - _R, 26, 2);
                else
                    e.Graphics.FillRectangle(b, 7, 253, 26, 2);


                if (255 - _A < 253)
                    e.Graphics.FillRectangle(b, 37, 255 - _A, 26, 2);
                else
                    e.Graphics.FillRectangle(b, 37, 253, 26, 2);
            }
        }
    }
}