using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.BspParsing;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;
using Darc_Euphoria.Hacks.Injection;

namespace Darc_Euphoria.Hacks
{
    public static class EntityList
    {
        public static Entity player;
        public static Entity[] List;
        public static ItemObjects[] ItemList;
        public static bool thirdperson;

        private static Process[] proc;

        public static void Start()
        {
            while (!gvar.isShuttingDown)
            {
                Thread.Sleep(10);

                proc = Process.GetProcessesByName("csgo");
                if (proc.Length == 0)
                {
                    gvar.isShuttingDown = true;
                    Environment.Exit(Environment.ExitCode);
                }

                if (!Local.InGame) continue;


                if (Settings.userSettings.MiscSettings._3rdPerson)
                {
                    if ((WinAPI.GetAsyncKeyState(Settings.userSettings.MiscSettings._3rdPersonKey) & 0x1) > 0)
                        thirdperson = !thirdperson;
                    Local.ThirdPerson = thirdperson;
                }
                else
                {
                    thirdperson = false;
                    Local.ThirdPerson = thirdperson;
                    Thread.Sleep(1000);
                }


                #region LoadMap

                var MapPath = string.Format(@"{0}\csgo\maps\{1}.bsp",
                    Memory.process.Modules[0].FileName.Substring(0, Memory.process.Modules[0].FileName.Length - 9),
                    Local.MapName);

                if (Local.bspMap == null)
                    LoadMap(MapPath);
                else if (Local.bspMap.FileName != MapPath) LoadMap(MapPath);

                #endregion
            }
        }

        public static void LoadMap(string MapPath)
        {
            if (File.Exists(MapPath) && Local.ActiveWeapon.WeaponID != -1)
            {
                Local.bspMap = new BSPFile(MapPath);
                ClientCMD.Exec(string.Format("clear; echo Map File {0} Loaded!", Local.MapName));
            }
        }
    }
}