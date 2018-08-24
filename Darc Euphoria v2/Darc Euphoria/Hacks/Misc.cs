using System.Threading;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;

namespace Darc_Euphoria.Hacks
{
    public static class Misc
    {
        public static void Start()
        {
            while (!gvar.isShuttingDown)
            {
                Thread.Sleep(50);

                if (!Local.InGame)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                Triggerbot.Start();
                SkinChanger.Start();

                Local.NoArms = Settings.userSettings.VisualSettings.NoHands;
                Local.Flash = Settings.userSettings.MiscSettings.FlashAlpha;
                Local.PostProcessingDisable = Settings.userSettings.MiscSettings.NoPostProcessing;

                ClanTagChanger.Start();
                ChatSpammer.Start();
                Rank.Start();

                if (Settings.userSettings.VisualSettings.NoScope)
                    if (Local.Scoped)
                        Local.Scoped = false;

                if (Settings.userSettings.VisualSettings.NoScope)
                {
                    if (Local.ActiveWeapon.ScopeLevel == 0)
                    {
                        Local.DrawViewModel = true;
                        Local.Fov = Settings.userSettings.MiscSettings.Fov;
                    }
                    else if (Local.ActiveWeapon.ScopeLevel == 1)
                    {
                        if (Local.ActiveWeapon.WeaponID != 8 && Local.ActiveWeapon.WeaponID != 39)
                        {
                            Local.DrawViewModel = false;
                            Local.Fov = 40;
                        }
                        else
                        {
                            Local.DrawViewModel = true;
                            Local.Fov = 90;
                        }
                    }
                    else if (Local.ActiveWeapon.ScopeLevel == 2)
                    {
                        Local.DrawViewModel = false;
                        Local.Fov = 10;
                    }
                    else
                    {
                        Local.DrawViewModel = true;
                        Local.Fov = Settings.userSettings.MiscSettings.Fov;
                    }
                }
                else
                {
                    if (!Local.Scoped)
                    {
                        Local.DrawViewModel = true;
                        Local.Fov = Settings.userSettings.MiscSettings.Fov;
                    }
                    else if (Local.ActiveWeapon.ScopeLevel == 1)
                    {
                        if (Local.ActiveWeapon.WeaponID != 8 && Local.ActiveWeapon.WeaponID != 39)
                        {
                            Local.DrawViewModel = false;
                            Local.Fov = 40;
                        }
                        else
                        {
                            Local.DrawViewModel = true;
                            Local.Fov = 90;
                        }
                    }
                    else if (Local.ActiveWeapon.ScopeLevel == 2)
                    {
                        Local.DrawViewModel = false;
                        Local.Fov = 10;
                    }
                    else
                    {
                        Local.DrawViewModel = true;
                        Local.Fov = Settings.userSettings.MiscSettings.Fov;
                    }
                }
            }
        }
    }
}