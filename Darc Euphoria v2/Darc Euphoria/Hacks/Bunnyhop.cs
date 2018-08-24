using System;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;

namespace Darc_Euphoria.Hacks
{
    internal class Bunnyhop
    {
        private static bool failedJump;
        private static readonly Random random = new Random();

        public static void Start()
        {
            if (gvar.isMenu) return;
            if (!Local.InGame) return;

            if (Local.VectorVelocity.x == 0 && Local.VectorVelocity.y == 0 && Local.VectorVelocity.z == 0) return;
            if (!Settings.userSettings.MiscSettings.BunnyHop) return;
            if ((WinAPI.GetAsyncKeyState(32) & 0x8000) <= 0)
            {
                failedJump = false;
                return;
            }

            if (Local.Flags != 257 && Local.Flags != 263) return;
            var r = random.Next(0, 100);
            if (r > Settings.userSettings.MiscSettings.BunnyHopChance)
                failedJump = true;
            if (failedJump) return;

            Local.Jump();
        }
    }
}