using System;
using System.Text;

namespace Darc_Euphoria.Euphoric.Objects
{
    public class BaseWeapon : IDisposable
    {
        private readonly int Ptr;

        public BaseWeapon()
        {
        }

        private BaseWeapon(int ptr)
        {
            Ptr = ptr;
        }

        public int Base => Memory.Read<int>(Memory.Client + Offsets.dwEntityList + (Ptr - 1) * 0x10);

        public short WeaponID
        {
            get => Memory.Read<short>(Base + Netvars.m_iItemDefinitionIndex);
            set => Memory.Write(Base + Netvars.m_iItemDefinitionIndex, value);
        }

        public int AccountID
        {
            get => Memory.Read<int>(Base + Netvars.m_iAccountID);
            set => Memory.Write(Base + Netvars.m_iAccountID, value);
        }

        public int XuIDHigh
        {
            get => Memory.Read<int>(Base + Netvars.m_OriginalOwnerXuidHigh);
            set => Memory.Write(Base + Netvars.m_OriginalOwnerXuidHigh, value);
        }

        public int XuIDLow
        {
            get => Memory.Read<int>(Base + Netvars.m_OriginalOwnerXuidLow);
            set => Memory.Write(Base + Netvars.m_OriginalOwnerXuidLow, value);
        }

        public int ItemIDHigh
        {
            get => Memory.Read<int>(Base + Netvars.m_iItemIDHigh);
            set => Memory.Write(Base + Netvars.m_iItemIDHigh, value);
        }

        public int ItemIDLow
        {
            get => Memory.Read<int>(Base + Netvars.m_iItemIDLow);
            set => Memory.Write(Base + Netvars.m_iItemIDLow, value);
        }

        public int PaintKit
        {
            get => Memory.Read<int>(Base + Netvars.m_nFallbackPaintKit);
            set => Memory.Write(Base + Netvars.m_nFallbackPaintKit, value);
        }

        public float Wear
        {
            get => Memory.Read<float>(Base + Netvars.m_flFallbackWear);
            set => Memory.Write(Base + Netvars.m_flFallbackWear, value);
        }

        public int StatTrak
        {
            get => Memory.Read<int>(Base + Netvars.m_nFallbackStatTrak);
            set => Memory.Write(Base + Netvars.m_nFallbackStatTrak, value);
        }

        public int Seed
        {
            get => Memory.Read<int>(Base + Netvars.m_nFallbackSeed);
            set => Memory.Write(Base + Netvars.m_nFallbackSeed, value);
        }

        public char[] Name
        {
            set
            {
                var writeChar = new char[32];

                for (var i = 0; i < writeChar.Length; i++)
                    if (i < value.Length)
                        writeChar[i] = value[i];

                var writebytes = Encoding.Default.GetBytes(writeChar);

                Memory.WriteBytes(Base + Netvars.m_szCustomName, writebytes);
            }
        }

        public int Ammo => Memory.Read<int>(Ptr + Netvars.m_iClip1);

        public int ScopeLevel => CanFire ? Memory.Read<int>(Base + Netvars.m_zoomLevel) : 0;

        public float nextPrimaryAttack => Memory.Read<float>(Ptr + Netvars.m_flNextPrimaryAttack);

        public bool CanFire => nextPrimaryAttack < Memory.Read<int>(Memory.Client + Netvars.m_nTickBase);

        public float AccuracyPenalty => Memory.Read<int>(Base + Netvars.m_fAccuracyPenalty);

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
                    case 23: return "MP5-SD";
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
                    case 45: return "Smoke Grenade";
                    case 46: return "Molotov";
                    case 47: return "Decoy";
                    case 48: return "Incendiary";
                    case 49: return "C4";
                    case 69: return "M4A1-S";
                    case 61: return "USP-S";
                    case 63: return "CZ75-Auto";
                    case 64: return "R8 Revolver";
                    default: return WeaponID.ToString();
                }
            }
        }

        public float WeaponSpread
        {
            get
            {
                var iCurrentSequenceNumber =
                    Memory.Read<int>(Local.ClientState + Offsets.LastOutGoingCommand) + 1;

                var userCmd = Memory.Read<int>(Memory.Client + Offsets.dwInput + 0xEC);
                userCmd += iCurrentSequenceNumber % 150 * 0x64;

                return Memory.Read<float>(userCmd + 0x3A);
            }
        }

        public void Dispose()
        {
        }

        public BaseWeapon MyWeapons(int player, int index)
        {
            var _Ptr = Memory.Read<int>(player + Netvars.m_hMyWeapons + (index - 1) * 0x4) & 0xFFF;
            return new BaseWeapon(_Ptr);
        }

        public BaseWeapon ActiveWeapon(int player)
        {
            var _Ptr = Memory.Read<int>(player + Netvars.m_hActiveWeapon) & 0xFFF;
            return new BaseWeapon(_Ptr);
        }

        public bool isBomb()
        {
            if (WeaponID == 49) return true;
            return false;
        }

        public bool isGrenade()
        {
            switch (WeaponID)
            {
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                    return true;
                default:
                    return false;
            }
        }

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
                case 519:
                case 520:
                case 522:
                case 523:
                    return true;
                default:
                    return false;
            }
        }

        public bool isPistol()
        {
            switch (WeaponID)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 30:
                case 32:
                case 36:
                case 61:
                case 63:
                case 64:
                    return true;
                default:
                    return false;
            }
        }

        public bool isSniper()
        {
            switch (WeaponID)
            {
                case 9:
                case 11:
                case 38:
                case 40:
                    return true;
                default:
                    return false;
            }
        }

        public bool isRifile()
        {
            switch (WeaponID)
            {
                case 7:
                case 8:
                case 10:
                case 13:
                case 16:
                case 39:
                case 60:
                    return true;

                default:
                    return false;
            }
        }

        public bool isSMG()
        {
            switch (WeaponID)
            {
                case 17:
                case 19:
                case 24:
                case 26:
                case 33:
                case 34:
                case 23:
                    return true;
                default:
                    return false;
            }
        }

        public bool isShotgun()
        {
            switch (WeaponID)
            {
                case 25:
                case 27:
                case 29:
                case 35:
                    return true;
                default:
                    return false;
            }
        }

        public bool isLMG()
        {
            switch (WeaponID)
            {
                case 14:
                case 28:
                    return true;
                default:
                    return false;
            }
        }
    }
}