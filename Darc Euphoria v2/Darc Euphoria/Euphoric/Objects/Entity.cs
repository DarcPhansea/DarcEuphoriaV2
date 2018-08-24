using System;
using System.Collections.Generic;
using System.Text;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Euphoric.Objects
{
    public class Entity : IDisposable
    {
        private static Entity[] _GetPlayers;

        private static int rGetPlayers;
        private static int _Ptr;
        private static int _Health;
        private static int _Team;
        private static int _GlowIndex;
        private static float _Speed;
        private static Vector3 _VectorVelocity;
        private static Vector3 _Postition;
        private static readonly BaseWeapon WeaponGet = new BaseWeapon();
        private static BaseWeapon _ActiveWeapon;
        private static bool _SpottedByMask;
        private static bool _Spotted;
        private static bool _Visible;
        private static bool _Dormant;
        private static string _Name;

        private static readonly int rPtr = 0;
        private static readonly int rHealth = 0;
        private static readonly int rTeam = 0;
        private static readonly int rActiveWeapon = 0;
        private static readonly int rGlowIndex = 0;
        private static readonly int rSpeed = 0;
        private static readonly int rVectorVelocity = 0;
        private static readonly int rSpottedByMask = 0;
        private static readonly int rDormant = 0;
        private static readonly int rSpotted = 0;
        private static readonly int rVisible = 0;
        private static readonly int rPostition = 0;
        private static readonly int rRenderColor = 0;
        private static readonly int rName = 0;

        public int Index;

        public Entity(int index, bool Bsp = false)
        {
            Index = index;
            if (Settings.userSettings.MiscSettings.inGameRadar)
                Spotted = true;
        }

        public static Entity[] EntityArray
        {
            get
            {
                if (rGetPlayers < gvar.RefreshID + 5000)
                {
                    rGetPlayers = gvar.RefreshID;

                    var returnArray = new List<Entity>();
                    for (var i = 0; i < 64; i++)
                    {
                        var player = new Entity(i);

                        if (player.Ptr == 0) continue;

                        if (player.Ptr == Local.Ptr)
                        {
                            Local.Index = i;
                            continue;
                        }

                        returnArray.Add(player);
                    }

                    _GetPlayers = returnArray.ToArray();
                }

                return _GetPlayers;
            }
        }

        public int Ptr
        {
            get
            {
                if (rPtr.Upd())
                    _Ptr = Memory.Read<int>(Memory.Client + Offsets.dwEntityList + (Index - 1) * 0x10);

                return _Ptr;
            }
        }

        public int Observe => Memory.Read<int>(Ptr + Netvars.m_hObserverTarget);

        public int Rank
        {
            get
            {
                var gameResource = Memory.Read<int>(Memory.Client + Offsets.dwPlayerResource);
                return Memory.Read<int>(gameResource + Netvars.m_iCompetitiveRanking + Index * 0x4);
            }
        }

        public bool isTeam
        {
            get
            {
                if (Team == Local.Team) return true;
                return false;
            }
        }

        public int Health
        {
            get
            {
                if (rHealth.Upd())
                    _Health = Memory.Read<int>(Ptr + Netvars.m_iHealth);

                return _Health;
            }
        }

        public int Team
        {
            get
            {
                if (rTeam.Upd())
                    _Team = Memory.Read<int>(Ptr + Netvars.m_iTeamNum);

                return _Team;
            }
        }

        public BaseWeapon ActiveWeapon
        {
            get
            {
                if (rActiveWeapon.Upd())
                    _ActiveWeapon = WeaponGet.ActiveWeapon(Ptr);

                return _ActiveWeapon;
            }
        }

        public int GlowIndex
        {
            get
            {
                if (rGlowIndex.Upd())
                    _GlowIndex = Memory.Read<int>(Ptr + Netvars.m_iGlowIndex);

                return _GlowIndex;
            }
        }

        public float Speed
        {
            get
            {
                if (rSpeed.Upd())
                    _Speed = (float) Math.Sqrt(
                        VectorVelocity.x * VectorVelocity.x +
                        VectorVelocity.y * VectorVelocity.y +
                        VectorVelocity.z * VectorVelocity.z
                    );

                return _Speed;
            }
        }

        public Vector3 VectorVelocity
        {
            get
            {
                if (rVectorVelocity.Upd())
                    _VectorVelocity = Memory.Read<Vector3>(Ptr + Netvars.m_vecVelocity);

                return _VectorVelocity;
            }
        }

        public bool SpottedByMask
        {
            get
            {
                if (rSpottedByMask.Upd())
                    _SpottedByMask = Memory.Read<bool>(Ptr + Netvars.m_bSpottedByMask);

                return _SpottedByMask;
            }
        }

        public bool Dormant
        {
            get
            {
                if (rDormant.Upd())
                    _Dormant = Memory.Read<bool>(Ptr + Offsets.m_bDormant);

                return _Dormant;
            }
        }

        public bool Spotted
        {
            get
            {
                if (rSpotted.Upd())
                    _Spotted = Memory.Read<bool>(Ptr + Netvars.m_bSpotted);

                return _Spotted;
            }
            set => Memory.Write(Ptr + Netvars.m_bSpotted, value);
        }

        public bool Visible
        {
            get
            {
                if (rVisible.Upd())
                {
                    if (Dormant)
                        _Visible = false;
                    else
                        _Visible = Local.bspMap.IsVisible(Local.EyeLevel, BonePosition(6));
                }

                return _Visible;
            }
        }

        public Vector3 Position
        {
            get
            {
                if (rPostition.Upd())
                    _Postition = Memory.Read<Vector3>(Ptr + Netvars.m_vecOrigin);

                return _Postition;
            }
        }

        public RenderColor renderColor
        {
            set
            {
                if (rRenderColor.Upd())
                    Memory.Write(Ptr + Netvars.m_clrRender, value);
            }
        }

        public string Name
        {
            get
            {
                if (rName.Upd())
                {
                    var radarBasePtr = gvar.isPanorama ? 0x6C : 0x54;
                    var radarStructSize = gvar.isPanorama ? 0x168 : 0x1E0;
                    var radarStructPos = gvar.isPanorama ? 0x18 : 0x24;

                    var enc = gvar.isPanorama ? Encoding.UTF8 : Encoding.Unicode;

                    var radarBase = Memory.Read<int>(Memory.Client + Offsets.dwRadarBase);

                    var radarPtr = Memory.Read<int>(radarBase + radarBasePtr);

                    var ind = gvar.isPanorama ? Index + 1 : Index;

                    var nameAddr = radarPtr + ind * radarStructSize + radarStructPos;
                    _Name = Memory.ReadString(nameAddr, 64, enc);
                }

                return _Name;
            }
        }

        public void Dispose()
        {
        }

        public Vector3 BonePosition(int Bone)
        {
            var bMatrix = Memory.Read<int>(Ptr + Netvars.m_dwBoneMatrix);

            var bonePos = new Vector3
            {
                x = Memory.Read<float>(bMatrix + 0x30 * Bone + 0x0C),
                y = Memory.Read<float>(bMatrix + 0x30 * Bone + 0x1C),
                z = Memory.Read<float>(bMatrix + 0x30 * Bone + 0x2C)
            };

            return bonePos;
        }
    }
}