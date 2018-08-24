using System;
using System.Drawing;
using Darc_Euphoria.Euphoric;

namespace Darc_Euphoria.Hacks
{
    public static class Nightmode
    {
        private static int hdc;

        public static void Init()
        {
            hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc().ToInt32();
        }

        public static unsafe bool SetBrightness(int brightness)
        {
            if (brightness > 255) brightness = 255;
            else if (brightness < 0) brightness = 0;

            short* gArray = stackalloc short[3 * 256];
            var idx = gArray;

            for (var j = 0; j < 3; j++)
            for (var i = 0; i < 256; i++)
            {
                var arrayVal = i * (brightness + 128);
                if (arrayVal > 65535) arrayVal = 65535;

                *idx = (short) arrayVal;
                idx++;
            }

            var retVal = WinAPI.SetDeviceGammaRamp(hdc, gArray);
            return retVal;
        }
    }
}