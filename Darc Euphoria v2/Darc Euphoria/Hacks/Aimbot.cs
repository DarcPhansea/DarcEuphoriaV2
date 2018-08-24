using System;
using System.Threading;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;
using Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Hacks
{
    internal class Aimbot
    {
        public static Settings.UserSettings.Aimbot.ASettings AimbotSettings;

        private static readonly Random rnd = new Random();

        private static Vector2 oldAngle;

        public static Entity ClosestPlayer
        {
            get
            {
                var PlayerIndex = -1;

                var lowestDistance = double.MaxValue;
                var lowestFov = double.MaxValue;
                var lowestHealth = double.MaxValue;

                foreach (var player in EntityList.List)
                {
                    if (player.Dormant) continue;
                    if (player.Health <= 0) continue;
                    if (player.isTeam && !AimbotSettings.TargetTeam) continue;
                    if (!player.Visible && AimbotSettings.VisiblitiyCheck) continue;

                    if (!player.isTeam && AimbotSettings.SpottedCheck)
                        if (!player.SpottedByMask)
                            continue;

                    var bone = 0;
                    switch (AimbotSettings.AimbotTarget)
                    {
                        case Settings.AimTarget.Head:
                            bone = 8;
                            break;
                        case Settings.AimTarget.Neck:
                            bone = 7;
                            break;
                        case Settings.AimTarget.UpperChest:
                            bone = 6;
                            break;
                        case Settings.AimTarget.MiddleChest:
                            bone = 5;
                            break;
                        case Settings.AimTarget.LowerChest:
                            bone = 4;
                            break;
                    }

                    var newAimAngle = MathFuncs.CalcAngle(Local.EyeLevel, player.BonePosition(bone));

                    var fov = MathFuncs.CalcFov(Local.ViewAndPunch, newAimAngle);

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

        public static Vector2 RCS
        {
            get
            {
                if (Local.ShotsFired < 1 && !Local.ActiveWeapon.isPistol())
                {
                    oldAngle.x = 0;
                    oldAngle.y = 0;
                    return Local.ViewAngle;
                }

                if (Local.ActiveWeapon.isPistol())
                    if (WinAPI.GetAsyncKeyState(1) <= 0 && (WinAPI.GetAsyncKeyState(1) & 0x8000) <= 0)
                        return Local.ViewAngle;

                var viewAngle = Local.ViewAngle + oldAngle;

                viewAngle.x -= (float) (Local.PunchAngle.x * (AimbotSettings.RcsYaw * 2) / 100);
                viewAngle.y -= (float) (Local.PunchAngle.y * (AimbotSettings.RcsPitch * 2) / 100);

                oldAngle.x = (float) (Local.PunchAngle.x * (AimbotSettings.RcsYaw * 2) / 100);
                oldAngle.y = (float) (Local.PunchAngle.y * (AimbotSettings.RcsPitch * 2) / 100);

                return viewAngle.ClampAngle();
            }
        }

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
            while (!gvar.isShuttingDown)
            {
                Thread.Sleep(1);

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

                if (Settings.userSettings.AimbotSettings.Key != 0)
                    if (WinAPI.GetAsyncKeyState(Settings.userSettings.AimbotSettings.Key) <= 0 &&
                        (WinAPI.GetAsyncKeyState(Settings.userSettings.AimbotSettings.Key) & 0x8000) <= 0)
                    {
                        Local.ViewAngle = RCS.ClampAngle();
                        continue;
                    }

                using (var closestPlayer = ClosestPlayer)
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

                        oldAngle.x = (float) (Local.PunchAngle.x * (AimbotSettings.RcsYaw * 2) / 100);
                        oldAngle.y = (float) (Local.PunchAngle.y * (AimbotSettings.RcsPitch * 2) / 100);

                        var ang = CalculateAimAngle(Local.EyeLevel, closestPlayer).ClampAngle();

                        Local.ViewAngle = ang;

                        //SetSilentAngle(ang);
                        //Enable If you dare.

                        Thread.Sleep(6);

                        Local.SendPackets = true;

                        if (AimbotSettings.AutoShoot)
                            Local.Attack();
                        else
                            Attack();
                    }
                }
            }
        }

        public static float Randomize(double amm)
        {
            var val = rnd.NextDouble();
            val -= 0.5;
            val *= amm;
            return (float) val;
        }

        public static void Attack()
        {
            if (Local.ActiveWeapon.isPistol() && AimbotSettings.AutoPistol)
                if (WinAPI.GetAsyncKeyState(1) > 0 || (WinAPI.GetAsyncKeyState(1) & 0x8000) > 0)
                    if (Local.ActiveWeapon.WeaponID != 64)
                        if (Local.ActiveWeapon.CanFire)
                            Local.Attack();
        }

        public static Vector2 CalculateAimAngle(Vector3 src, Entity target, bool withSmooth = true)
        {
            var bone = 0;
            switch (AimbotSettings.AimbotTarget)
            {
                case Settings.AimTarget.Head:
                    bone = 8;
                    break;
                case Settings.AimTarget.Neck:
                    bone = 7;
                    break;
                case Settings.AimTarget.UpperChest:
                    bone = 6;
                    break;
                case Settings.AimTarget.MiddleChest:
                    bone = 5;
                    break;
                case Settings.AimTarget.LowerChest:
                    bone = 4;
                    break;
            }

            var targetBone = target.BonePosition(bone);
            targetBone += target.VectorVelocity * gvar.GlobalVarsBase.interval_per_tick;

            var rando = new Vector3
            {
                x = Randomize(AimbotSettings.Randomized),
                y = Randomize(AimbotSettings.Randomized),
                z = Randomize(AimbotSettings.Randomized)
            };
            targetBone += rando;

            var srcEye = src;
            srcEye += Local.VectorVelocity * gvar.GlobalVarsBase.interval_per_tick;

            var delta = targetBone - srcEye;
            var magn = delta.Length;

            var newAimAngle = new Vector2
            {
                x = (float) Math.Atan2(delta.y, delta.x),
                y = (float) -Math.Atan2(delta.z, magn)
            };

            newAimAngle *= 180f / 3.14f; //Radians to Degrees

            newAimAngle.x -= (float) (Local.PunchAngle.x * (AimbotSettings.RcsYaw * 2) / 100);
            newAimAngle.y -= (float) (Local.PunchAngle.y * (AimbotSettings.RcsPitch * 2) / 100);

            newAimAngle = newAimAngle.ClampAngle();

            var fov = MathFuncs.CalcFov(Local.ViewAndPunch, newAimAngle);
            var fov2 = MathFuncs.CalcFov(Local.ViewAndPunch.NormalizeAngle(), newAimAngle.NormalizeAngle());


            if (withSmooth)
            {
                if (fov < fov2)
                {
                    if (AimbotSettings.SmoothYaw > 0)
                        newAimAngle.x = (newAimAngle - Local.ViewAngle).x / (float) AimbotSettings.SmoothYaw +
                                        Local.ViewAngle.x;

                    if (AimbotSettings.SmoothPitch > 0)
                        newAimAngle.y = (newAimAngle - Local.ViewAngle).y / (float) AimbotSettings.SmoothPitch +
                                        Local.ViewAngle.y;
                }
                else
                {
                    if (AimbotSettings.SmoothYaw > 0)
                        newAimAngle.x =
                            (newAimAngle.NormalizeAngle() - Local.ViewAngle.NormalizeAngle()).x /
                            (float) AimbotSettings.SmoothYaw + Local.ViewAngle.NormalizeAngle().x;

                    if (AimbotSettings.SmoothPitch > 0)
                        newAimAngle.y =
                            (newAimAngle.NormalizeAngle() - Local.ViewAngle.NormalizeAngle()).y /
                            (float) AimbotSettings.SmoothPitch + Local.ViewAngle.NormalizeAngle().y;
                }
            }

            return newAimAngle;
        }

        //It was a failed attempt :/
        public static void SetSilentAngle(Vector2 viewangle)
        {
            var oldViewAngle = Local.ViewAngle;
            var iUserCMDSequenceNumber = 0;
            var iCurrentSequenceNumber =
                Memory.Read<int>(Local.ClientState + Offsets.LastOutGoingCommand) + 1;

            var userCmd = Memory.Read<int>(Memory.Client + Offsets.dwInput + 0xEC);
            userCmd += iCurrentSequenceNumber % 150 * 0x64;

            Local.SendPackets = false;

            while (iUserCMDSequenceNumber != iCurrentSequenceNumber)
            {
                oldViewAngle = Local.ViewAngle;
                iUserCMDSequenceNumber = Memory.Read<int>(userCmd + 0x4);
            }

            for (var i = 0; i < 20; i++)
                Memory.Write(userCmd + 0xC, viewangle.ClampAngle());

            Local.ViewAngle = oldViewAngle;
            oldViewAngle.x -= (float) (Local.PunchAngle.x * (AimbotSettings.RcsYaw * 2) / 100);
            oldViewAngle.y -= (float) (Local.PunchAngle.y * (AimbotSettings.RcsPitch * 2) / 100);

            Thread.Sleep(6);
            Local.SendPackets = true;
        }
    }
}