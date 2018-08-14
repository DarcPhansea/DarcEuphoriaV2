using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;
using Darc_Euphoria.Hacks.Injection;

namespace Darc_Euphoria.Hacks
{
    class Rank
    {
        public static void Start()
        {
            if (!Settings.userSettings.MiscSettings.RankRevealer) return;
            if ((WinAPI.GetAsyncKeyState(0x9) & 0x8000) <= 0) return;

            RankRevealer.Show();
            Thread.Sleep(200);
        }
    }
}
