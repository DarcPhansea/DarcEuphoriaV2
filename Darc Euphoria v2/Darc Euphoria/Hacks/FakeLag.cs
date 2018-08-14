using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Darc_Euphoria.Hacks
{
    class FakeLag
    {
        private static int _lastLag = 0;
        private static bool once = false;
        public static void Start()
        {
            gvar.SHUTDOWN++;
            while (true)
            {
                if (gvar.isShuttingDown)
                {
                    gvar.SHUTDOWN--;
                    Local.SendPackets = true;
                    break;
                }

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
                else
                {
                    once = true;
                }

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
                if (endLag + 20 > Environment.TickCount)
                {
                    Local.SendPackets = true;
                    continue;
                }
                _lastLag = Environment.TickCount;
            }
        }
    }
}
