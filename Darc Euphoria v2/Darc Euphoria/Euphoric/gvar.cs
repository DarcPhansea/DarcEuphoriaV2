using Darc_Euphoria.Euphoric.Objects;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Euphoric
{
    internal static class gvar
    {
        public static Size OverlaySize;
        public static Point OverlayPoint;
        public static double AspectRatio => (double)OverlaySize.Width / (double)OverlaySize.Height;

        public static float FontSize = 15;

        public static System.Drawing.Font font = new System.Drawing.Font("Calibri", 15, System.Drawing.FontStyle.Bold);

        public static TextFormat textFormat = new TextFormat(new Factory(),
            "Calibri", FontWeight.Bold, SharpDX.DirectWrite.FontStyle.Normal, FontSize);

        public static bool isShuttingDown = false;
        public static int SHUTDOWN = 0;

        public static float Fps = 200;
        private static GlobalVarBase _GlobalVarsBase;
        public static int rGlobalVarsBase = 0;
        public static GlobalVarBase GlobalVarsBase
        {
            get
            {
                return Memory.Read<GlobalVarBase>(Memory.engine + Offsets.dwGlobalVars);
            }
        }

        public static Allocator Allocator = new Allocator();
        public static bool isRunning = true;
        public static bool isPanorama = false;
        public static bool isMenu = false;
        public static Size wndSize = Size.Empty;
        public static string netCFG = Application.LocalUserAppDataPath + @"\netvar.cfg";

        public static int RefreshID = 1;
    }
}
