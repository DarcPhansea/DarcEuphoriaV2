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
    class Aimbot
    {
        public static Settings.UserSettings.Aimbot.ASettings AimbotSettings;
        private static void LoadSetting()
        {
            if (Local.ActiveWeapon.isPistol())
                AimbotSettings = Settings.userSettings.AimbotSettings.Pistol;
            else if (Local.ActiveWeapon.isRifile())
                AimbotSettings = Settings.userSettings.AimbotSettings.Rifle;
            else if (Local.ActiveWeapon.isSMG())
                AimbotSettings = Settings.userSettings.AimbotSettings.Smg;
            else if (Local.ActiveWeapon.isSniper())
                AimbotSettings = Settings.userSettings.AimbotSettings.Sniper;
            else if (Local.ActiveWeapon.isShotgun())
                AimbotSettings = Settings.userSettings.AimbotSettings.Shotgun;
            else if (Local.ActiveWeapon.isLMG())
                AimbotSettings = Settings.userSettings.AimbotSettings.Lmg;
        }

        public static void Start()
        {
            gvar.SHUTDOWN++;
            while (true)
            {
                if (gvar.isShuttingDown)
                {
                    gvar.SHUTDOWN--;
                    break;
                }

                Thread.Sleep(10);

                if (gvar.isMenu) continue;
                if (!Local.InGame)
                {
                    AimbotSettings = new Settings.UserSettings.Aimbot.ASettings();
                    continue;
                }
                if (Local.ActiveWeapon.isKnife() || Local.ActiveWeapon.isBomb() || Local.ActiveWeapon.isGrenade())
                {
                    AimbotSettings = new Settings.UserSettings.Aimbot.ASettings();
                    continue;
                }
                LoadSetting();

                if (!Local.ActiveWeapon.CanFire) continue;

                if (!AimbotSettings.Enabled)
                {
                    Local.ViewAngle = RCS.ClampAngle();

                    Attack();
                    continue;
                }

                if (Settings.userSettings.AimbotSettings.Key == 1)
                {
                    if (AimbotSettings.SmoothPitch == 0 && AimbotSettings.SmoothYaw == 0)
                    {
                        if (!AimbotSettings.AutoShoot)
                        {
                            if (WinAPI.GetAsyncKeyState(1) <= 0 &&
                                (WinAPI.GetAsyncKeyState(1) & 0x8000) <= 0) continue;
                        }
                    }     
                }
                else
                {
                    if (WinAPI.GetAsyncKeyState(Settings.userSettings.AimbotSettings.Key) <= 0 &&
                                (WinAPI.GetAsyncKeyState(Settings.userSettings.AimbotSettings.Key) & 0x8000) <= 0)
                    {
                        Local.ViewAngle = RCS.ClampAngle();
                        continue;
                    }
                }
                using (Entity closestPlayer = ClosestPlayer)
                {
                    if (closestPlayer.Index == -1)
                    {
                        Local.ViewAngle = RCS.ClampAngle();

                        Attack();
                    }
                    else
                    {
                        Thread.Sleep(AimbotSettings.Delay);
                    
                        if (AimbotSettings.SmoothPitch == 0 || AimbotSettings.SmoothYaw == 0)
                            Local.SendPackets = false;

                        oldAngle.x = (float)(Local.PunchAngle.x * (AimbotSettings.RcsYaw * 2) / 100);
                        oldAngle.y = (float)(Local.PunchAngle.y * (AimbotSettings.RcsPitch * 2) / 100);

                        Vector2 ang = CalculateAimAngle(Local.EyeLevel, closestPlayer).ClampAngle();

                        Local.ViewAngle = ang;

                        //SetSilentAngle(ang);
                        //Enable If you dare.

                        Thread.Sleep(6);

                        Local.SendPackets = true;

                        if (AimbotSettings.AutoShoot)
                        {
                            Vector2 v2 = CalculateAimAngle(Local.EyeLevel, closestPlayer, false).ClampAngle();
                            if (Math.Round(ang.x, 1) == Math.Round(v2.x, 1) && Math.Round(ang.y, 1) == Math.Round(v2.y, 1))
                                Local.Attack();
                        }
                        else
                        {
                            Vector2 v2 = CalculateAimAngle(Local.EyeLevel, closestPlayer, false).ClampAngle();
                            if (Math.Round(ang.x, 1) == Math.Round(v2.x, 1) && Math.Round(ang.y, 1) == Math.Round(v2.y, 1))
                                Attack();
                        }
                        Thread.Sleep(50);
                    }
                }   
            }
        }

        public static Random rnd = new Random();
        public static float Randomize(double amm)
        {
            double val = rnd.NextDouble();
            val -= 0.5;
            val *= amm;
            return (float)val;
        }

        public static void Attack()
        {
            if (Local.ActiveWeapon.isPistol() && AimbotSettings.AutoPistol)
            {
                if (WinAPI.GetAsyncKeyState(1) > 0 || (WinAPI.GetAsyncKeyState(1) & 0x8000) > 0)
                {
                    if (Local.ActiveWeapon.WeaponID != 64)
                    {
                        if (Local.ActiveWeapon.CanFire)
                            Local.Attack();
                    }
                }
            }
        }

        public static Entity ClosestPlayer
        {
            get
            {
                int PlayerIndex = -1;

                double lowestDistance = double.MaxValue;
                double lowestFov = double.MaxValue;
                double lowestHealth = double.MaxValue;

                foreach (Entity player in EntityList.List)
                {
                    if (player.Dormant) continue;
                    if (player.Health <= 0) continue;
                    if (player.isTeam && !AimbotSettings.TargetTeam) continue;
                    if (!player.Visible && AimbotSettings.VisiblitiyCheck) continue;

                    if (!player.isTeam && AimbotSettings.SpottedCheck)
                    {
                        if (!player.SpottedByMask) continue;
                    }

                    int bone = 0;
                    switch (AimbotSettings.AimbotTarget)
                    {
                        case Settings.AimTarget.Head:
                            bone = 8; break;
                        case Settings.AimTarget.Neck:
                            bone = 7; break;
                        case Settings.AimTarget.UpperChest:
                            bone = 6; break;
                        case Settings.AimTarget.MiddleChest:
                            bone = 5; break;
                        case Settings.AimTarget.LowerChest:
                            bone = 4; break;
                    }

                    Vector2 newAimAngle = MathFuncs.CalcAngle(Local.EyeLevel, player.BonePosition(bone));

                    double fov = MathFuncs.CalcFov(Local.ViewAndPunch, newAimAngle);

                    if (fov > AimbotSettings.Fov)
                        fov = MathFuncs.CalcFov(Local.ViewAndPunch.NormalizeAngle(), newAimAngle.NormalizeAngle());

                    if (fov > AimbotSettings.Fov) continue;

                    if (AimbotSettings.AimbotPriority == Settings.AimPriority.Fov)
                    {
                        if (lowestFov <= fov) continue;
                    }
                    else if (AimbotSettings.AimbotPriority == Settings.AimPriority.Distance)
                    {
                        if (lowestDistance <= MathFuncs.VectorDistance(Local.EyeLevel, player.BonePosition(bone)))
                            continue;
                    }
                    else
                    {
                        if (lowestHealth <= player.Health)
                            continue;
                    }

                    lowestDistance = MathFuncs.VectorDistance(Local.EyeLevel, player.BonePosition(bone));
                    lowestFov = fov;
                    lowestHealth = player.Health;
                    PlayerIndex = player.Index;
                }
                return new Entity(PlayerIndex);
            }
        }

        public static Vector2 CalculateAimAngle(Vector3 src, Entity target, bool withSmooth = true)
        {
            int bone = 0;
            switch (AimbotSettings.AimbotTarget)
            {
                case Settings.AimTarget.Head:
                    bone = 8; break;
                case Settings.AimTarget.Neck:
                    bone = 7; break;
                case Settings.AimTarget.UpperChest:
                    bone = 6; break;
                case Settings.AimTarget.MiddleChest:
                    bone = 5; break;
                case Settings.AimTarget.LowerChest:
                    bone = 4; break;
            }

            Vector3 targetBone = target.BonePosition(bone);
            targetBone += target.VectorVelocity * gvar.GlobalVarsBase.interval_per_tick;

            Vector3 rando = new Vector3()
            {
                x = Randomize(AimbotSettings.Randomized),
                y = Randomize(AimbotSettings.Randomized),
                z = Randomize(AimbotSettings.Randomized),
            };
            targetBone += rando;

            Vector3 srcEye = src;
            srcEye += Local.VectorVelocity * gvar.GlobalVarsBase.interval_per_tick;

            Vector3 delta = (targetBone - srcEye);
            float magn = delta.Length;

            Vector2 newAimAngle = new Vector2()
            {
                x = (float)Math.Atan2(delta.y, delta.x),
                y = (float)-Math.Atan2(delta.z, magn),
            };
            
            newAimAngle *= (180f / 3.14f); //Radians to Degrees

            newAimAngle.x -= (float)(Local.PunchAngle.x * (AimbotSettings.RcsYaw * 2) / 100);
            newAimAngle.y -= (float)(Local.PunchAngle.y * (AimbotSettings.RcsPitch * 2) / 100);

            newAimAngle = newAimAngle.ClampAngle();

            double fov = MathFuncs.CalcFov(Local.ViewAndPunch, newAimAngle);
            double fov2 = MathFuncs.CalcFov(Local.ViewAndPunch.NormalizeAngle(), newAimAngle.NormalizeAngle());
              
            
            if (withSmooth)
            {
                if (fov < fov2)
                {
                    if (AimbotSettings.SmoothYaw > 0)
                        newAimAngle.x = (newAimAngle - Local.ViewAngle).x / (float)AimbotSettings.SmoothYaw + Local.ViewAngle.x;

                    if (AimbotSettings.SmoothPitch > 0)
                        newAimAngle.y = (newAimAngle - Local.ViewAngle).y / (float)AimbotSettings.SmoothPitch + Local.ViewAngle.y;
                }
                else
                {
                    if (AimbotSettings.SmoothYaw > 0)
                        newAimAngle.x = (newAimAngle.NormalizeAngle() - Local.ViewAngle.NormalizeAngle()).x / (float)AimbotSettings.SmoothYaw + Local.ViewAngle.NormalizeAngle().x;

                    if (AimbotSettings.SmoothPitch > 0)
                        newAimAngle.y = (newAimAngle.NormalizeAngle() - Local.ViewAngle.NormalizeAngle()).y / (float)AimbotSettings.SmoothPitch + Local.ViewAngle.NormalizeAngle().y;

                }
            }
            
            return newAimAngle;
        }

        public static Vector2 oldAngle = new Vector2();
        public static Vector2 RCS
        {
            get
            {
                Local.SendPackets = true;

                if (Local.ShotsFired < 1 && !Local.ActiveWeapon.isPistol())
                {
                    oldAngle.x = 0;
                    oldAngle.y = 0;
                    return Local.ViewAngle;
                }
                else if (Local.ActiveWeapon.isPistol())
                {
                    if (WinAPI.GetAsyncKeyState(1) <= 0 && (WinAPI.GetAsyncKeyState(1) & 0x8000) <= 0)
                    {
                        oldAngle.x = 0;
                        oldAngle.y = 0;
                        return Local.ViewAngle;
                    }
                }
                Vector2 viewAngle = Local.ViewAngle + oldAngle;

                viewAngle.x -= (float)(Local.PunchAngle.x * (AimbotSettings.RcsYaw * 2) / 100);
                viewAngle.y -= (float)(Local.PunchAngle.y * (AimbotSettings.RcsPitch * 2) / 100);

                oldAngle.x = (float)(Local.PunchAngle.x * (AimbotSettings.RcsYaw * 2) / 100);
                oldAngle.y = (float)(Local.PunchAngle.y * (AimbotSettings.RcsPitch * 2) / 100);

                return viewAngle.ClampAngle();
            }
        }

        //It was a failed attempt :/
        public static void SetSilentAngle(Vector2 viewangle)
        {
            Vector2 oldViewAngle = Local.ViewAngle;
            int iUserCMDSequenceNumber = 0;
            int iCurrentSequenceNumber = 
                Memory.Read<int>(Local.ClientState + Offsets.LastOutGoingCommand) + 1;

            int userCmd = Memory.Read<int>(Memory.client + Offsets.dwInput + 0xEC);
            userCmd += (iCurrentSequenceNumber % 150) * 0x64;

            Local.SendPackets = false;

            while (iUserCMDSequenceNumber != iCurrentSequenceNumber)
            {
                oldViewAngle = Local.ViewAngle;
                iUserCMDSequenceNumber = Memory.Read<int>(userCmd + 0x4);
            }

            for (int i = 0; i < 20; i++)
                Memory.Write<Vector2>(userCmd + 0xC, viewangle.ClampAngle());

            Local.ViewAngle = oldViewAngle;
            oldViewAngle.x -= (float)(Local.PunchAngle.x * (AimbotSettings.RcsYaw * 2) / 100);
            oldViewAngle.y -= (float)(Local.PunchAngle.y * (AimbotSettings.RcsPitch * 2) / 100);

            Thread.Sleep(6);
            Local.SendPackets = true;
        }
    }
}
