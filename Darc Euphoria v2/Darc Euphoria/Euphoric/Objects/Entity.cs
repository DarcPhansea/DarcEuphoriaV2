using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Hacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Euphoric.Objects
{
    public class Entity : IDisposable
    {
        public void Dispose()
        {

        }

        private static Entity[] _GetPlayers;

        private static int rGetPlayers = 0;
        public static Entity[] EntityArray
        {
            get
            {
                if (rGetPlayers < gvar.RefreshID + 5000)
                {
                    rGetPlayers = gvar.RefreshID;

                    List<Entity> returnArray = new List<Entity>();

                    for (int i = 0; i < 64; i++)
                    {
                        Entity player = new Entity(i);

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

        public int Index;
        private static int _Ptr;
        private static int _Health;
        private static int _Team;
        private static int _GlowIndex;
        private static float _Speed;
        private static Vector3 _VectorVelocity;
        private static Vector3 _Postition;
        private static BaseWeapon WeaponGet = new BaseWeapon();
        private static BaseWeapon _ActiveWeapon;
        private static bool _SpottedByMask;
        private static bool _Spotted;
        private static bool _Visible;
        private static bool _Dormant;
        private static string _Name;
        
        private static int rPtr = 0;
        public int Ptr
        {
            get
            {
                if (rPtr.Upd())
                    _Ptr = Memory.Read<int>(Memory.client + Offsets.dwEntityList + (Index - 1) * 0x10);

                return _Ptr;
            }
        }

        public int Observe => Memory.Read<int>(Ptr + Netvars.m_hObserverTarget);
        public int Rank
        {
            get
            {
                int gameResource = Memory.Read<int>(Memory.client + Offsets.dwPlayerResource);
                return Memory.Read<int>(gameResource + Netvars.m_iCompetitiveRanking + Index * 0x4);
            }
        }

        public bool isTeam
        {
            get
            {
                if (Team == Local.Team) return true;
                else return false;
            }
        }

        private static int rHealth = 0;
        public int Health
        {
            get
            {
                if (rHealth.Upd())
                    _Health = Memory.Read<int>(Ptr + Netvars.m_iHealth);

                return _Health;
            }
        }

        private static int rTeam = 0;
        public int Team
        {
            get
            {
                if (rTeam.Upd())
                    _Team = Memory.Read<int>(Ptr + Netvars.m_iTeamNum);

                return _Team;
            }
        }

        private static int rActiveWeapon = 0;
        public BaseWeapon ActiveWeapon
        {
            get
            {
                if (rActiveWeapon.Upd())
                    _ActiveWeapon = WeaponGet.ActiveWeapon(Ptr);

                return _ActiveWeapon;
            }
        }

        private static int rGlowIndex = 0;
        public int GlowIndex
        {
            get
            {
                if (rGlowIndex.Upd())
                    _GlowIndex = Memory.Read<int>(Ptr + Netvars.m_iGlowIndex);

                return _GlowIndex;
            }
        }

        private static int rSpeed = 0;
        public float Speed
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
        public Vector3 VectorVelocity
        {
            get
            {
                if (rVectorVelocity.Upd())
                    _VectorVelocity = Memory.Read<Vector3>(Ptr + Netvars.m_vecVelocity);

                return _VectorVelocity;
            }
        }

        private static int rSpottedByMask = 0;
        public bool SpottedByMask
        {
            get
            {
                if (rSpottedByMask.Upd())
                    _SpottedByMask = Memory.Read<bool>(Ptr + Netvars.m_bSpottedByMask);

                return _SpottedByMask;
            }
        }

        private static int rDormant = 0;
        public bool Dormant
        {
            get
            {
                if (rDormant.Upd())
                    _Dormant = Memory.Read<bool>(Ptr + Offsets.m_bDormant);

                return _Dormant;
            }
        }

        private static int rSpotted = 0;
        public bool Spotted
        {
            get
            {
                if (rSpotted.Upd())
                    _Spotted = Memory.Read<bool>(Ptr + Netvars.m_bSpotted);

                return _Spotted;
            }
            set
            {
                Memory.Write<bool>(Ptr + Netvars.m_bSpotted, value);
            }
        }

        private static int rVisible = 0;
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

        private static int rPostition = 0;
        public Vector3 Position
        {
            get
            {
                if (rPostition.Upd())
                    _Postition = Memory.Read<Vector3>(Ptr + Netvars.m_vecOrigin);

                return _Postition;
            }
        }

        private static int rRenderColor = 0;
        public RenderColor renderColor
        {
            set
            {
                if(rRenderColor.Upd())
                    Memory.Write<RenderColor>(Ptr + Netvars.m_clrRender, value);
            }
        }

        public Vector3 BonePosition(int Bone)
        {
            int bMatrix = Memory.Read<int>(Ptr + Netvars.m_dwBoneMatrix);

            Vector3 bonePos = new Vector3()
            {
                x = Memory.Read<float>(bMatrix + (0x30 * Bone) + 0x0C),
                y = Memory.Read<float>(bMatrix + (0x30 * Bone) + 0x1C),
                z = Memory.Read<float>(bMatrix + (0x30 * Bone) + 0x2C),

            };

            return bonePos;
        }

        private static int rName = 0;
        public string Name
        {
            get
            {
                if (rName.Upd())
                {
                    int radarBasePtr = gvar.isPanorama ? 0x6C : 0x54;
                    int radarStructSize = gvar.isPanorama ? 0x168 : 0x1E0;
                    int radarStructPos = gvar.isPanorama ? 0x18 : 0x24;

                    Encoding enc = gvar.isPanorama ? Encoding.UTF8 : Encoding.Unicode;

                    int radarBase = Memory.Read<int>(Memory.client + Offsets.dwRadarBase);

                    int radarPtr = Memory.Read<int>(radarBase + radarBasePtr);

                    int ind = gvar.isPanorama ? Index + 1 : Index;

                    var nameAddr = radarPtr + ind * radarStructSize + radarStructPos;
                    _Name = Memory.ReadString(nameAddr, 64, enc);
                }
                return _Name;
            }
        }

        public Entity(int index, bool Bsp = false)
        {
            Index = index;
            if (Settings.userSettings.MiscSettings.inGameRadar)
                Spotted = true;
        }
    }
}
