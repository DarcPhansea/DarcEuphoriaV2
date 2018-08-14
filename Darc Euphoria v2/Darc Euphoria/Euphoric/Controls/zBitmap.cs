using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Darc_Euphoria.Euphoric.Controls
{
    public class zBitmap
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Disposed { get; private set; }
        public string Path { get; private set; }

        protected GCHandle BitsHandle { get; private set; }

        public zBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public zBitmap(string path)
        {
            Bitmap image = (Bitmap)Bitmap.FromFile(path);
            Width = image.Width;
            Height = image.Height;
            Bits = new Int32[image.Width * image.Height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(image.Width, image.Height, image.Width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
            Path = path;
            SetImage(path);
        }

        public zBitmap(Bitmap bitmap)
        {
            Bitmap image = bitmap;
            Width = image.Width;
            Height = image.Height;
            Bits = new Int32[image.Width * image.Height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(image.Width, image.Height, image.Width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }


        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + (y * Width);
            int col = colour.ToArgb();
            Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];
            return Color.FromArgb(col);
        }

        public void SetImage(string path)
        {
            Bitmap image = (Bitmap)Bitmap.FromFile(path);
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    SetPixel(x, y, color);
                }
            }
        }

        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }
    }
}
