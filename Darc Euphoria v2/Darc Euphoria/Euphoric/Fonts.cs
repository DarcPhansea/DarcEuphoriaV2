using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using Darc_Euphoria.Properties;

namespace Darc_Euphoria.Euphoric
{
    public static class Fonts
    {
        public static PrivateFontCollection CustomFonts = new PrivateFontCollection();
        public static Font WeaponIcons;

        public static void InitFonts()
        {
            AddFont(Resources.csgo_icons);
            WeaponIcons = new Font(CustomFonts.Families[0], 12);
        }

        private static void AddFont(byte[] font)
        {
            var data = Marshal.AllocCoTaskMem(font.Length);
            Marshal.Copy(font, 0, data, font.Length);
            CustomFonts.AddMemoryFont(data, font.Length);
        }
    }
}