using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;

namespace Darc_Euphoria.Hacks
{
    internal class SkinChanger
    {
        public static bool UpdateNeeded;

        public static void Start()
        {
            if ((WinAPI.GetAsyncKeyState(Settings.userSettings.ForceUpdateKey) & 0x1) > 0) Local.ForceUpdate();

            if (!Settings.userSkinSettings.Enable) return;
            if (!Local.InGame) return;

            var weapons = Local.WeaponList;

            var skinSettings = new Settings._SkinSettings();

            skinSettings.Name = string.Empty;
            skinSettings.Seed = 0;
            skinSettings.Wear = 0;
            skinSettings.StatTrak = -1;
            skinSettings.SkinID = 1337;

            foreach (var weapon in weapons)
            {
                switch (weapon.WeaponID)
                {
                    case 1:
                        skinSettings = Settings.userSkinSettings.Deagle;
                        break;
                    case 2:
                        skinSettings = Settings.userSkinSettings.DuelBerettas;
                        break;
                    case 3:
                        skinSettings = Settings.userSkinSettings.FiveSeven;
                        break;
                    case 4:
                        skinSettings = Settings.userSkinSettings.Glock18;
                        break;
                    case 7:
                        skinSettings = Settings.userSkinSettings.AK47;
                        break;
                    case 8:
                        skinSettings = Settings.userSkinSettings.Aug;
                        break;
                    case 9:
                        skinSettings = Settings.userSkinSettings.Awp;
                        break;
                    case 10:
                        skinSettings = Settings.userSkinSettings.Famas;
                        break;
                    case 11:
                        skinSettings = Settings.userSkinSettings.G3sg1;
                        break;
                    case 13:
                        skinSettings = Settings.userSkinSettings.Galilar;
                        break;
                    case 14:
                        skinSettings = Settings.userSkinSettings.M249;
                        break;
                    case 16:
                        skinSettings = Settings.userSkinSettings.M4a4;
                        break;
                    case 17:
                        skinSettings = Settings.userSkinSettings.Mac10;
                        break;
                    case 19:
                        skinSettings = Settings.userSkinSettings.P90;
                        break;
                    case 24:
                        skinSettings = Settings.userSkinSettings.Ump45;
                        break;
                    case 25:
                        skinSettings = Settings.userSkinSettings.Xm1014;
                        break;
                    case 26:
                        skinSettings = Settings.userSkinSettings.PPBizon;
                        break;
                    case 27:
                        skinSettings = Settings.userSkinSettings.Mag7;
                        break;
                    case 28:
                        skinSettings = Settings.userSkinSettings.Negev;
                        break;
                    case 29:
                        skinSettings = Settings.userSkinSettings.SawedOff;
                        break;
                    case 30:
                        skinSettings = Settings.userSkinSettings.Tec9;
                        break;
                    case 32:
                        skinSettings = Settings.userSkinSettings.P2000;
                        break;
                    case 33:
                        skinSettings = Settings.userSkinSettings.Mp7;
                        break;
                    case 34:
                        skinSettings = Settings.userSkinSettings.Mp9;
                        break;
                    case 35:
                        skinSettings = Settings.userSkinSettings.Nova;
                        break;
                    case 36:
                        skinSettings = Settings.userSkinSettings.P250;
                        break;
                    case 38:
                        skinSettings = Settings.userSkinSettings.Scar20;
                        break;
                    case 39:
                        skinSettings = Settings.userSkinSettings.Sg553;
                        break;
                    case 40:
                        skinSettings = Settings.userSkinSettings.Ssg08;
                        break;
                    case 60:
                        skinSettings = Settings.userSkinSettings.M4a1s;
                        break;
                    case 61:
                        skinSettings = Settings.userSkinSettings.USPS;
                        break;
                    case 63:
                        skinSettings = Settings.userSkinSettings.Cz75a;
                        break;
                    case 64:
                        skinSettings = Settings.userSkinSettings.R8Revolver;
                        break;
                    default:
                        skinSettings.Name = string.Empty;
                        skinSettings.Seed = 0;
                        skinSettings.Wear = 0;
                        skinSettings.StatTrak = -1;
                        skinSettings.SkinID = 1337;
                        break;
                }

                if (weapon.PaintKit != skinSettings.SkinID && skinSettings.SkinID != 1337 &&
                    Settings.userSkinSettings.Enable)
                {
                    do
                    {
                        weapon.ItemIDLow = -1;
                        weapon.PaintKit = skinSettings.SkinID;
                        weapon.StatTrak = skinSettings.StatTrak;
                        weapon.Seed = skinSettings.Seed;
                        weapon.Wear = skinSettings.Wear == 0 ? 0.00001f : skinSettings.Wear;
                        weapon.Name = skinSettings.Name.ToCharArray();
                        weapon.AccountID = weapon.XuIDLow;
                    } while (weapon.PaintKit != skinSettings.SkinID);

                    UpdateNeeded = true;
                }
            }


            if (UpdateNeeded)
            {
                UpdateNeeded = false;
                Local.ForceUpdate();
            }
        }
    }
}