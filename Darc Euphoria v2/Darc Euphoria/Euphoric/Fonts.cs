using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Darc_Euphoria.Euphoric
{
    public static class Fonts
    {
        public static PrivateFontCollection CustomFonts = new PrivateFontCollection();
        public static Font WeaponIcons = null;

        public static void InitFonts()
        {
            AddFont(Properties.Resources.csgo_icons);
            WeaponIcons = new Font(CustomFonts.Families[0], 12);
        }

        private static void AddFont(byte[] font)
        {
            IntPtr data = Marshal.AllocCoTaskMem(font.Length);
            Marshal.Copy(font, 0, data, font.Length);
            CustomFonts.AddMemoryFont(data, font.Length);
        }
    }
}
