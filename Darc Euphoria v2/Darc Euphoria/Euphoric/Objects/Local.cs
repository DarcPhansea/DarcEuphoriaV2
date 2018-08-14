using Darc_Euphoria.Hacks;
using Darc_Euphoria.Hacks.Injection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Euphoric.Objects
{
    public static class Local
    {
        public static GlobalVarBase GlobalVar => 
            Memory.Read<GlobalVarBase>(Memory.engine + Offsets.dwGlobalVars);

        private static int _Index;
        private static int _Ptr;
        private static int _ClientState;
        private static int _Health;
        private static int _Team;
        private static int _GlowIndex;
        private static float _Speed;
        private static Vector3 _VectorVelocity;
        private static Vector3 _Postition;
        private static Vector2 _ViewAngle;
        private static Vector2 _PunchAngle;
        private static bool _InGame;
        private static int _Flags;
        private static BaseWeapon WeaponGet = new BaseWeapon();
        private static BaseWeapon _ActiveWeapon;
        private static List<BaseWeapon> _WeaponList;
        private static int _CrosshairID;
        private static int _Fov;
        private static int _ViewmodelFov;
        private static float _Flash;
        private static bool _GotKill;
        private static int Kills;
        public static BspParsing.BSPFile bspMap = null;

        public static int Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
            }
        }

        public static bool GotKill
        {
            get
            {
                int _kills = Memory.Read<int>(Offsets.dwPlayerResource + 0xBE8 + (Index + 1) * 0x4);
                if (Kills < _kills)
                {
                    Kills = _kills;
                    return true;
                }
                return false;
            }
        }

        public static string MapName
        {
            get
            {
                return Memory.ReadString(Local.ClientState + Offsets.dwClientState_Map, 32, Encoding.ASCII);
            }
        }

        public static Process[] csgo;
        public static string MapPath
        {
            get
            {
                try
                {
                    csgo = Process.GetProcessesByName("csgo");

                    if (csgo.Length > 0)
                    {

                        string file = csgo[0].MainModule.FileName;
                        file = file.Substring(0, file.Length - 9) + @"\csgo\";
                        return file;
                    }
                    else
                    {
                        return null;
                    }

                    
                }
                catch
                {
                    return null;
                }
            }
        }

        public static string MapFile
        {
            get
            {
                try
                {
                    string f = Memory.ReadString(Local.ClientState + Offsets.dwClientState_MapDirectory, 32, Encoding.ASCII);
                    
                    if (!f.ToLower().Contains("map"))
                        return null;

                    string file = Memory.process.MainModule.FileName;
                    file = file.Substring(0, file.Length - 9) + @"\csgo\";

                    if (!f.EndsWith(".bsp")) file = string.Format("{0}{1}.bsp", file, f);
                    else file = string.Format("{0}{1}", file, f);

                    return file;
                }
                catch
                {
                    return null;
                }
            }
        }

        private static int rFov = 0;
        public static int Fov
        {
            get
            {
                if (rFov.Upd())
                {
                    if (Local.ActiveWeapon.WeaponID == 8 || Local.ActiveWeapon.WeaponID == 39)
                        _Fov = Memory.Read<int>(Ptr + 0x330C);
                    else
                        _Fov = Memory.Read<int>(Ptr + Netvars.m_iFOVStart - 4);
                }

                return _Fov;
            }
            set
            {
                if (value == _Fov) return;

                if (Local.ActiveWeapon.WeaponID == 8 || Local.ActiveWeapon.WeaponID == 39)
                {
                    for (int i = 0; i < 1000; i++)
                        Memory.Write<int>(Ptr + 0x330C, value);
                }
                else
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        Memory.Write<int>(Ptr + Netvars.m_iFOVStart - 4, value);
                        Memory.Write<int>(Ptr + 0x330C, 90);
                    }
                }
                
            }

        }

        private static int rViewmodelFov = 0;
        public static int ViewmodelFov
        {
            get
            {
                if (rViewmodelFov.Upd())
                    _ViewmodelFov = Memory.Read<int>(Ptr + 0x330C);

                return _ViewmodelFov;
            }
            set
            {
                if (value == _ViewmodelFov) return;

                for (int i = 0; i < 1000; i++)
                    Memory.Write<int>(Ptr + 0x330C, value);
            }

        }

        private static int rFlash = 0;
        public static float Flash
        {
            get
            {
                if (rFlash.Upd())
                    _Flash = Memory.Read<float>(Ptr + Netvars.m_flFlashMaxAlpha);

                return _Flash;
            }
            set
            {
                if (value == _Flash) return;
                Memory.Write<float>(Ptr + Netvars.m_flFlashMaxAlpha, value);
                _Flash = value;
            }

        }

        private static int rCrosshairID = 0;
        public static int CrosshairID
        {
            get
            {
                if (rCrosshairID.Upd())
                    _CrosshairID = Memory.Read<int>(Ptr + Netvars.m_iCrosshairId);

                return _CrosshairID;
            }
        }

        private static int rActiveWeapon = 0;
        public static BaseWeapon ActiveWeapon
        {
            get
            {
                if (rActiveWeapon.Upd())
                    _ActiveWeapon = WeaponGet.ActiveWeapon(Ptr);

                return _ActiveWeapon;
            }
        }

        private static int rWeaponList = 0;
        public static List<BaseWeapon> WeaponList
        {
            get
            {
                if (rWeaponList.Upd())
                {
                    _WeaponList = new List<BaseWeapon>();
                    for (int i = 1; i < 16; i++)
                    {
                        BaseWeapon weapo = WeaponGet.MyWeapons(Ptr, i);
                        if (weapo.WeaponID == 0) continue;

                        _WeaponList.Add(weapo);
                    }
                }
                return _WeaponList;
            }
        }

        private static int rInGame = 0;
        public static bool InGame
        {
            get
            {
                if (rInGame.Upd())
                    _InGame = Memory.Read<int>(ClientState + Offsets.dwClientState_State) == 6;

                return _InGame;
            }
        }

        private static int rPtr = 0;
        public static int Ptr
        {
            get
            {
                if (rPtr.Upd())
                    _Ptr = Memory.Read<int>(Memory.client + Offsets.dwLocalPlayer);

                return _Ptr;
            }
        }

        private static int rClientState = 0;
        public static int ClientState
        {
            get
            {
                if (rClientState.Upd())
                    _ClientState = Memory.Read<int>(Memory.engine + Offsets.dwClientState);

                return _ClientState;
            }
        }

        private static int rHealth = 0;
        public static int Health
        {
            get
            {
                if (rHealth.Upd())
                    _Health = Memory.Read<int>(Ptr + Netvars.m_iHealth);

                return _Health;
            }
        }

        private static int rTeam = 0;
        public static int Team
        {
            get
            {
                if (rTeam.Upd())
                    _Team = Memory.Read<int>(Ptr + Netvars.m_iTeamNum);

                return _Team;
            }
        }

        public static int ShotsFired
        {
            get
            {
                return Memory.Read<int>(Ptr + Netvars.m_iShotsFired);
            }
        }

        private static int rGlowIndex = 0;
        public static int GlowIndex
        {
            get
            {
                if (rGlowIndex.Upd())
                    _GlowIndex = Memory.Read<int>(Ptr + Netvars.m_iGlowIndex);

                return _GlowIndex;
            }
        }

        private static int rSpeed = 0;
        public static float Speed
        {
            get
            {
                if (rSpeed.Upd())
                    _Speed = (float)Math.Sqrt(
                            VectorVelocity.x * VectorVelocity.x +
                            VectorVelocity.y * VectorVelocity.y +
                            VectorVelocity.z * VectorVelocity.z
                            );

                return _Speed;
            }

        }

        private static int rVectorVelocity = 0;
        public static Vector3 VectorVelocity
        {
            get
            {
                if (rVectorVelocity.Upd())
                    _VectorVelocity = Memory.Read<Vector3>(Ptr + Netvars.m_vecVelocity);

                return _VectorVelocity;
            }
        }

        private static int rPosition = 0;
        public static Vector3 Position
        {
            get
            {
                if (rPosition.Upd())
                    _Postition = Memory.Read<Vector3>(Ptr + Netvars.m_vecOrigin);

                return _Postition;
            }
        }

        public static Vector3 EyeLevel
        {
            get
            {
                Vector3 vector = Position;
                vector.z += Memory.Read<float>(Ptr + 0x10C);
                return vector;
            }
        }

        private static int rViewAngle = 0;
        public static Vector2 ViewAngle
        {
            get
            {
                if (rViewAngle.Upd())
                    _ViewAngle = Memory.Read<Vector2>(ClientState + Offsets.dwClientState_ViewAngles);

                return _ViewAngle;
            }
            set
            {
                if (value.Equals(_ViewAngle)) return;

                Memory.Write<Vector2>(ClientState + Offsets.dwClientState_ViewAngles, value);

                _ViewAngle = value;
            }
        }

        private static int rPunchAngle = 0;
        public static Vector2 PunchAngle
        {
            get
            {
                if (rPunchAngle.Upd())
                    _PunchAngle = Memory.Read<Vector2>(Ptr + Netvars.m_aimPunchAngle);

                return _PunchAngle;
            }
        }

        public static Vector2 ViewAndPunch
        {
            get
            {
                return ViewAngle + PunchAngle;
            }
        }

        public static float ScopeScale
        {
            get
            {
                if (!Scoped) return 1f;


                return ActiveWeapon.ScopeLevel == 1 ? 2f : 12f;
            }
        }

        public static bool DrawViewModel
        {
            get
            {
                return Memory.Read<bool>(Local.Ptr + Netvars.m_bDrawViewmodel);
            }
            set
            {
                for(int i = 0; i < 1000 ; i++)
                    Memory.Write<bool>(Local.Ptr + Netvars.m_bDrawViewmodel, value);
            }
        }

        public static RenderColor renderColor
        {
            set
            {
                Memory.Write<RenderColor>(Ptr + Netvars.m_clrRender, value);
            }
        }

        public static bool Scoped
        {
            get
            {
                return Memory.Read<bool>(Local.Ptr + Netvars.m_bIsScoped);
            }
            set
            {
                for(int i = 0; i < 1000; i++)
                    Memory.Write<bool>(Local.Ptr + Netvars.m_bIsScoped, value);
            }
        }

        public static int rFlags = 0;
        public static int Flags
        {
            get
            {
                if (rFlags.Upd())
                    _Flags = Memory.Read<int>(Ptr + Netvars.m_fFlags);

                return _Flags;
            }

        }

        public static void Jump()
        {
            Memory.Write<int>(Memory.client + Offsets.dwForceJump, 5);
            Thread.Sleep(15);
            Memory.Write<int>(Memory.client + Offsets.dwForceJump, 4);
        }

        public static void Attack()
        {
            if (!ActiveWeapon.CanFire) return;

            Memory.Write<int>(Memory.client + Offsets.dwForceAttack, 5);
            Thread.Sleep(10);
            Memory.Write<int>(Memory.client + Offsets.dwForceAttack, 4);
        }

        public static void Attack(int Length)
        {
            if (!ActiveWeapon.isPistol())
            {
                Memory.Write<int>(Memory.client + Offsets.dwForceAttack, 5);

                while (ShotsFired < Length)
                {
                    Entity p = new Entity(CrosshairID);
                    if (p.Dormant || p.Health <= 0) break;
                }

                Memory.Write<int>(Memory.client + Offsets.dwForceAttack, 4);

                Thread.Sleep(500);
            }
            else
            {
                while (ShotsFired < Length)
                {
                    Entity p = new Entity(CrosshairID);
                    if (p.Dormant || p.Health <= 0) break;

                    if (ActiveWeapon.CanFire) Attack();
                }
                Thread.Sleep(500);
            }
        }

        public static void Attack(bool auto)
        {
            if (!auto) return;

            if (!ActiveWeapon.isPistol())
            {
                Memory.Write<int>(Memory.client + Offsets.dwForceAttack, 5);

                while (CrosshairID >= 0 && CrosshairID <= 65)
                {
                    Entity p = new Entity(CrosshairID);
                    if (p.Dormant || p.Health <= 0) break;

                    Thread.Sleep(1);
                }

                Memory.Write<int>(Memory.client + Offsets.dwForceAttack, 4);
            }
            else
            {
                while (CrosshairID >= 0 && CrosshairID <= 65)
                {
                    Entity p = new Entity(CrosshairID);
                    if (p.Dormant || p.Health <= 0) break;


                    if (ActiveWeapon.CanFire) Attack();
                }
            }
        }

        public static void Attack2()
        {
            Memory.Write<int>(Memory.client + Offsets.dwForceAttack2, 5);
            Thread.Sleep(15);
            Memory.Write<int>(Memory.client + Offsets.dwForceAttack2, 4);
        }

        public static bool _post;
        public static bool PostProcessingDisable
        {
            get
            {
                return Memory.Read<bool>(Memory.client + Offsets.s_bOverridePostProcessingDisable);
            }
            set
            {
                if (value == _post) return;
                Memory.Write<bool>(Memory.client + Offsets.s_bOverridePostProcessingDisable, value);
                _post = value;
            }
        }

        public static bool SendPackets
        {
            get
            {
                return Memory.ReadBytes(Memory.engine + Offsets.dwSendPackets, 1)[0] == 1;
            }
            set
            {
                var vByte = value ? (byte)1 : (byte)0;
                Memory.Write<byte>(Memory.engine + Offsets.dwSendPackets, vByte);
            }
        }

        public static void ForceUpdate()
        {
            Local.SendPackets = true;
            Thread.Sleep(10);
            Memory.Write<int>(ClientState + 0x174, -1);
        }


        public static bool NoArms
        {
            set
            {
                if (value == true)
                {
                    for (int i = 0; i < 1000; i++)
                        Memory.Write<int>(Ptr + Offsets.m_nModelIndex, 0);
                }
                else
                {
                    if (Memory.Read<int>(Ptr + Offsets.m_nModelIndex) == 0)
                        ForceUpdate();
                }
            }
        }

        private static bool _ThirdPerson;
        private static int rThirdPerson = 0;
        public static bool ThirdPerson
        {
            get
            {
                if (rThirdPerson.Upd())
                    _ThirdPerson = Memory.Read<int>(Ptr + Netvars.m_iObserverMode) == 5;

                return _ThirdPerson;
            }
            set
            {
                Memory.Write<int>(Ptr + Netvars.m_iObserverMode, value ? 1 : 0);
            }
        }

        public static bool ThirdPersonA
        {
            set
            {
                int iUserCMDSequenceNumber = 0;
                int iCurrentSequenceNumber =
                    Memory.Read<int>(Local.ClientState + Offsets.LastOutGoingCommand) + 1;

                int userCmd = Memory.Read<int>(Memory.client + Offsets.dwInput + 0xEC)
                    + (iCurrentSequenceNumber % 150) * 0x64;

                while (iUserCMDSequenceNumber != iCurrentSequenceNumber)
                    iUserCMDSequenceNumber = Memory.Read<int>(userCmd + 0x4);

                while (iUserCMDSequenceNumber == iCurrentSequenceNumber)
                {
                    iUserCMDSequenceNumber = Memory.Read<int>(userCmd + 0x4);

                    Memory.Write<bool>(Memory.client + Offsets.dwInput + 0xA5, EntityList.thirdperson);
                    Memory.Write<Vector3>(Memory.client + Offsets.dwInput + 0xA8, new Vector3(ViewAngle.y, ViewAngle.x, EntityList.thirdperson ? 100 : 0));
                }
            }

        }

        private static int _GameObjectsCount;
        private static int rGameObjectsCount = 0;
        public static int EntityListLength
        {
            get
            {
                if (rGameObjectsCount.Upd())
                    _GameObjectsCount = Memory.Read<int>(Memory.engine + Offsets.dwEntityListLength);

                return _GameObjectsCount;
            }
        }
    }
}
