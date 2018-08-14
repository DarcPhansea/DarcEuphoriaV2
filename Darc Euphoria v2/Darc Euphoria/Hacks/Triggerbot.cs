using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;
using static Darc_Euphoria.Euphoric.Structs;
using System.Windows.Forms;

namespace Darc_Euphoria.Hacks
{
    class Triggerbot
    {
        private static Settings.UserSettings.Triggerbot.TSettings TriggerbotSettings;

        private static void LoadSetting()
        {
            if (Local.ActiveWeapon.isPistol())
                TriggerbotSettings = Settings.userSettings.TriggerbotSettings.Pistol;
            else if (Local.ActiveWeapon.isRifile())
                TriggerbotSettings = Settings.userSettings.TriggerbotSettings.Rifle;
            else if (Local.ActiveWeapon.isSMG())
                TriggerbotSettings = Settings.userSettings.TriggerbotSettings.Smg;
            else if (Local.ActiveWeapon.isSniper())
                TriggerbotSettings = Settings.userSettings.TriggerbotSettings.Sniper;
            else if (Local.ActiveWeapon.isShotgun())
                TriggerbotSettings = Settings.userSettings.TriggerbotSettings.Shotgun;
            else if (Local.ActiveWeapon.isLMG())
                TriggerbotSettings = Settings.userSettings.TriggerbotSettings.Lmg;
        }

        public static void Start()
        {
            if (gvar.isMenu) return;
            if (!Local.InGame) return;

            if (Settings.userSettings.TriggerbotSettings.Knifebot && Local.ActiveWeapon.isKnife())
            {
                if (CanKnife()) Local.Attack2();

                return;
            }
            else
            {
                if (!Local.ActiveWeapon.CanFire) return;

                if (Local.CrosshairID < 0 || Local.CrosshairID > 65) return;

                LoadSetting();

                if (!TriggerbotSettings.Enabled) return;

                Entity target = new Entity(Local.CrosshairID);

                if (target.Dormant) return;

                if (target.isTeam && !TriggerbotSettings.TargetTeam) return;

                if (target.Health <= 0) return;

                target.Dispose();

                Thread.Sleep(TriggerbotSettings.Delay);

                if (TriggerbotSettings.TriggerbotMode == Settings.TriggerMode.Auto)
                {
                    Local.Attack(true);
                }
                else if (TriggerbotSettings.TriggerbotMode == Settings.TriggerMode.Burst)
                {
                    Local.Attack(TriggerbotSettings.BurstAmount);
                }
                else
                {
                    Local.Attack();
                    Thread.Sleep(500);
                }
            }
        }

        public static bool CanKnife()
        {
            foreach(Entity player in EntityList.List)
            {
                if (player.Dormant) continue;
                if (player.Health <= 0) continue;
                if (player.isTeam && !TriggerbotSettings.TargetTeam) continue;

                if (80 < MathFuncs.VectorDistance(Local.Position, player.Position)) continue;

                Vector2 newAimAngle = MathFuncs.CalcAngle(Local.EyeLevel, player.BonePosition(4));

                double Fov = MathFuncs.CalcFov(Local.ViewAngle, newAimAngle);

                if (Fov > 70)
                    Fov = MathFuncs.CalcFov(Local.ViewAngle.NormalizeAngle(), newAimAngle.NormalizeAngle());

                if (Fov > 70) continue;

                return true;
            }
            return false;
        }
    }
}
