using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Euphoric.Objects
{
    
    public class ItemObjects : IDisposable
    {
        public void Dispose()
        {

        }

        private static ItemObjects[] _GetItem;

        private static int rGetItem = 0;
        public static ItemObjects[] ItemList
        {
            get
            {
                if (rGetItem < gvar.RefreshID + 5000)
                {
                    rGetItem = gvar.RefreshID;
                    List<ItemObjects> returnArray = new List<ItemObjects>();

                    for (int i = 65; i < Local.EntityListLength; i++)
                    {
                        ItemObjects item = new ItemObjects(i);

                        if (item.Ptr == 0) continue;
                        if (item.ClassName == "-1") continue;
                        if (item.Dormant) continue;
                        if (item.HasOwner) continue;
                        returnArray.Add(item);
                    }

                    _GetItem = returnArray.ToArray();
                }
                
                return _GetItem;
            }
        }

        public int Index;

        private static int _Ptr;
        private static int rPtr = 0;
        public int Ptr
        {
            get
            {
                if (rPtr.Upd())
                    _Ptr = Memory.Read<int>(Memory.client + Offsets.dwEntityList + Index * 0x10);

                return _Ptr;
            }
        }

        private static int _GlowIndex;
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

        private static int _Classid;
        private static int rClassid = 0;
        public int ClassID
        {
            get
            {
                if (rClassid.Upd())
                {
                    var vt = Memory.Read<int>(Ptr + 0x8);
                    var fn = Memory.Read<int>(vt + 0x8);
                    var cls = Memory.Read<int>(fn + 0x1);
                    _Classid = Memory.Read<int>(cls + 0x14);
                }
                return _Classid;
            }
        }

        private static Vector3 _Position;
        private static int rPosition = 0;
        public Vector3 Position
        {
            get
            {
                if (rPosition.Upd())
                    _Position = Memory.Read<Vector3>(Ptr + Netvars.m_vecOrigin);

                return _Position;
            }
        }

        private static bool _Dormant;
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

        public bool HasOwner => Position.Equals(new Vector3());

        public short WeaponID => Memory.Read<short>(Ptr + Netvars.m_iItemDefinitionIndex);

        public bool isKnife()
        {
            switch (WeaponID)
            {
                case 41:
                case 42:
                case 59:
                case 500:
                case 505:
                case 506:
                case 507:
                case 508:
                case 509:
                case 512:
                case 514:
                case 515:
                case 516:
                    return true;
                default:
                    return false;
            }
        }

        public string WeaponName
        {
            get
            {
                if (isKnife()) return "Knife";

                switch (WeaponID)
                {
                    case 1: return "Desert Eagle";
                    case 2: return "Duel Berettas";
                    case 3: return "Five-SeveN";
                    case 4: return "Glock-18";
                    case 7: return "AK-47";
                    case 8: return "AUG";
                    case 9: return "AWP";
                    case 10: return "FAMAS";
                    case 11: return "G3SG1";
                    case 13: return "Galil AR";
                    case 14: return "M249";
                    case 16: return "M4A4";
                    case 17: return "MAC-10";
                    case 19: return "P90";
                    case 24: return "UMP-45";
                    case 25: return "XM1014";
                    case 26: return "PP-Bizon";
                    case 27: return "MAG-7";
                    case 28: return "Negev";
                    case 29: return "Sawed-Off";
                    case 30: return "Tec-9";
                    case 31: return "Zeus x27";
                    case 32: return "P2000";
                    case 33: return "MP7";
                    case 34: return "MP9";
                    case 35: return "Nova";
                    case 36: return "P250";
                    case 38: return "SCAR-20";
                    case 39: return "SG 553";
                    case 40: return "SSG 08";
                    case 43: return "Flashbang";
                    case 44: return "HE Grenade";
                    case 45: return "Smoke";
                    case 46: return "Molotov";
                    case 47: return "Decoy";
                    case 48: return "Incendiary";
                    case 49: return "C4";
                    case 69: return "M4A1-S";
                    case 61: return "USP-S";
                    case 63: return "CZ75-Auto";
                    case 64: return "R8 Revolver";
                    default: return "-1";
                }

            }
        }

        public string Icon
        {
            get
            {
                if (isKnife()) return "\uE02A";

                switch (WeaponID)
                {
                    case 1: return "\uE001";
                    case 2: return "\uE002";
                    case 3: return "\uE003";
                    case 4: return "\uE004";
                    case 7: return "\uE007";
                    case 8: return "\uE008";
                    case 9: return "\uE009";
                    case 10: return "\uE00A";
                    case 11: return "\uE00B";
                    case 13: return "\uE00D";
                    case 14: return "\uE00E";
                    case 16: return "\uE010";
                    case 17: return "\uE011";
                    case 19: return "\uE013";
                    case 24: return "\uE018";
                    case 25: return "\uE019";
                    case 26: return "\uE01A";
                    case 27: return "\uE01B";
                    case 28: return "\uE01C";
                    case 29: return "\uE01D";
                    case 30: return "\uE01E";
                    case 31: return "\uE01F";
                    case 32: return "\uE020";
                    case 33: return "\uE021";
                    case 34: return "\uE022";
                    case 35: return "\uE023";
                    case 36: return "\uE024";
                    case 38: return "\uE026";
                    case 39: return "\uE027";
                    case 40: return "\uE028";
                    case 43: return "\uE02B";
                    case 44: return "\uE02C";
                    case 45: return "\uE02D";
                    case 46: return "\uE02E";
                    case 47: return "\uE02F";
                    case 48: return "\uE030";
                    case 49: return "\uE031";
                    case 69: return "\uE045";
                    case 61: return "\uE03D";
                    case 63: return "\uE03F";
                    case 64: return "\uE040";
                    default: return WeaponID.ToString();
                }

            }
        }

        public bool isWeapon
        {
            get
            {
                if (isKnife()) return true;

                switch (WeaponID)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 7:
                    case 8: 
                    case 9:
                    case 10: 
                    case 11:
                    case 13: 
                    case 14: 
                    case 16: 
                    case 17: 
                    case 19: 
                    case 24: 
                    case 25:
                    case 26: 
                    case 27:
                    case 28: 
                    case 29:
                    case 30:
                    case 31: 
                    case 32: 
                    case 33: 
                    case 34: 
                    case 35: 
                    case 36:
                    case 38:
                    case 39:
                    case 40: 
                    case 49:
                    case 69:
                    case 61:
                    case 63:
                    case 64:
                        return true;
                    default: return false;
                }
            }
        }

        public string ClassName
        {
            get
            {
                switch (ClassID)
                {
                    case 44:
                        return "Defuser";
                    case 202:
                        return "AUG";
                    case 222:
                        return "MAG-10";
                    case 226:
                        return "MP9";
                    case 227:
                        return "Negev";
                    case 39:
                        return "Deagle";
                    case 108:
                        return "Planted C4";
                    case 29:
                        return "C4";
                    case 237:
                        return "SG 553";
                    case 211:
                        return "Dual Berettas";
                    case 217:
                        return "Glock-18";
                    case 219:
                        return "M249";
                    case 31:
                        return "Chicken";
                    case 207:
                        return "PP-Bizon";
                    case 134:
                        return "Smoke";
                    case 231:
                        return "P90";
                    case 230:
                        return "P250";
                    case 223:
                        return "MAG-7";
                    case 214:
                        return "G3SG1";
                    case 205:
                        return "AWP";
                    case 225:
                        return "MP7";
                    case 212:
                        return "FAMAS";
                    case 1:
                        return "AK-47";
                    case 240:
                        return "Tec-9";
                    case 232:
                        return "Sawed-Off";
                    case 233:
                        return "SCAR-20";
                    case 228:
                        return "Nova";
                    case 218:
                        return "USP-S";
                    case 244:
                        return "XM1014";
                    case 239:
                        return "Zeus x27";
                    case 221:
                        return "M4A1";
                    case 242:
                        return "UMP-45";
                    case 213:
                        return "Five-SeveN";
                    case 238:
                        return "SSG 08";
                    case 216:
                        return "Galil-AR";


                    case 84:
                        return "HE Grenade";
                    case 9:
                        return "Grenade";
                    case 8:
                        return "Grenade";
                    case 88:
                        return "Incendiary";
                    case 97:
                        return "Incendiary";
                    case 66:
                        return "Flashbang";
                    case 87:
                        return "Incendiary";
                    case 98:
                        return "Incendiary";
                    case 133:
                        return "Smoke";
                    case 40:
                        return "Decoy";
                    case 41:
                        return "Decoy";
                }

                return "-1";
            }
        }

        public ItemObjects(int index)
        {
            Index = index;
        }
    }
}
