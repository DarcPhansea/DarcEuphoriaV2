using System;
using System.Threading;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;

namespace Darc_Euphoria.Hacks
{
    internal class FakeLag
    {
        private static int _lastLag;
        private static bool once;

        public static void Start()
        {
            while (!gvar.isShuttingDown)
            {
                Thread.Sleep(1);

                Bunnyhop.Start();

                if (!Local.InGame)
                {
                    if (Local.SendPackets == false)
                        Local.SendPackets = true;
                    continue;
                }

                if (!Settings.userSettings.MiscSettings.FakeLag)
                {
                    if (once)
                    {
                        if (Local.SendPackets == false)
                            Local.SendPackets = true;
                        once = false;
                    }

                    continue;
                }

                once = true;

                if ((WinAPI.GetAsyncKeyState(0x1) & 0x8000) > 0 || WinAPI.GetAsyncKeyState(0x1) > 0)
                {
                    if (Local.SendPackets == false)
                        Local.SendPackets = true;
                    continue;
                }

                var endLag = _lastLag + Settings.userSettings.MiscSettings.FakeLagAmount;
                if (endLag > Environment.TickCount)
                {
                    Local.SendPackets = false;
                    continue;
                }

                if (endLag + 15 > Environment.TickCount)
                {
                    Local.SendPackets = true;
                    continue;
                }

                _lastLag = Environment.TickCount;
            }

            Local.SendPackets = true;
        }
    }
}