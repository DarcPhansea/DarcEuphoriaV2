using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;

namespace Darc_Euphoria.Hacks
{
    class Bunnyhop
    {
        public static void Start()
        {
            if (gvar.isMenu) return;
            if (!Local.InGame) return;
            if (Local.VectorVelocity.x == 0 && Local.VectorVelocity.y == 0 && Local.VectorVelocity.z == 0) return;
            if (!Settings.userSettings.MiscSettings.BunnyHop) return;
            if ((WinAPI.GetAsyncKeyState(32) & 0x8000) <= 0) return;
            if (Local.Flags != 257 && Local.Flags != 263) return;
            Local.Jump();
        }
    }
}
