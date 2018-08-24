using System.Drawing;
using System.Windows.Forms;
using Darc_Euphoria.Euphoric.Structs;
using SharpDX.DirectWrite;
using Font = System.Drawing.Font;
using FontStyle = System.Drawing.FontStyle;

namespace Darc_Euphoria.Euphoric
{
    internal static class gvar
    {
        public static Size OverlaySize = Size.Empty;
        public static Point OverlayPoint;

        public static float FontSize = 15;

        public static Font font = new Font("Calibri", 15, FontStyle.Regular);

        public static TextFormat textFormat = new TextFormat(new Factory(),
            "Calibri", FontWeight.Black, SharpDX.DirectWrite.FontStyle.Normal, FontSize);

        public static bool isShuttingDown = false;

        public static float Fps = 200;

        //private static GlobalVarBase _GlobalVarsBase;
        public static int rGlobalVarsBase = 0;

        public static Allocator Allocator = new Allocator();
        public static bool isPanorama = false;
        public static bool isMenu = false;
        public static Size wndSize = Size.Empty;
        public static string netCFG = Application.LocalUserAppDataPath + @"\netvar.cfg";

        public static int RefreshID = 1;
        public static double AspectRatio => OverlaySize.Width / (double) OverlaySize.Height;

        public static GlobalVarBase GlobalVarsBase => Memory.Read<GlobalVarBase>(Memory.Engine + Offsets.dwGlobalVars);
    }
}