using System.Threading;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Hacks.Injection;

namespace Darc_Euphoria.Hacks
{
    internal class Rank
    {
        private static bool once;

        public static void Start()
        {
            if (!Settings.userSettings.MiscSettings.RankRevealer) return;
            if ((WinAPI.GetAsyncKeyState(0x9) & 0x8000) <= 0)
            {
                if (!gvar.isPanorama)
                    once = false;

                return;
            }

            if (!once)
            {
                RankRevealer.Show();
                once = true;
            }

            Thread.Sleep(200);
        }
    }
}