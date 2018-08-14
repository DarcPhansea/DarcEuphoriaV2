using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;
using Darc_Euphoria.Hacks.Injection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darc_Euphoria.Hacks
{
    class ClanTagChanger
    {
        public static int clanPrevNum = -1;
        public static void Start()
        {
            if (Settings.userSettings.MiscSettings.ClanChangerTheme == Settings.ClanChangerTheme.SkeetTheme)
            {
                string tag = String.Empty;
                int t = ((int)(Local.GlobalVar.curtime * 2.4) % 29);
                switch (t)
                {
                    case 0: tag = "                 "; break;
                    case 1: tag = "                D"; break;
                    case 2: tag = "               Da"; break;
                    case 3: tag = "              Dar"; break;
                    case 4: tag = "             Darc"; break;
                    case 5: tag = "            Darc "; break;
                    case 6: tag = "           Darc E"; break;
                    case 7: tag = "          Darc Eu"; break;
                    case 8: tag = "         Darc Eup"; break;
                    case 9: tag = "        Darc Euph"; break;
                    case 10: tag = "       Darc Eupho"; break;
                    case 11: tag = "      Darc Euphor"; break;
                    case 12: tag = "     Darc Euphori"; break;
                    case 13: tag = "    Darc Euphoria"; break;
                    case 14: tag = "   Darc Euphoria "; break;
                    case 15: tag = "  Darc Euphoria  "; break;
                    case 16: tag = " Darc Euphoria   "; break;
                    case 17: tag = "Darc Euphoria    "; break;
                    case 18: tag = "arc Euphoria     "; break;
                    case 19: tag = "rc Euphoria      "; break;
                    case 20: tag = "c Euphoria       "; break;
                    case 21: tag = " Euphoria        "; break;
                    case 22: tag = "Euphoria         "; break;
                    case 23: tag = "uphoria          "; break;
                    case 24: tag = "phoria           "; break;
                    case 25: tag = "horia            "; break;
                    case 26: tag = "oria             "; break;
                    case 27: tag = "ria              "; break;
                    case 28: tag = "ia               "; break;
                    case 29: tag = "a                "; break;
                    default: break;
                }
                if (clanPrevNum != t)
                {
                    SetClanTag.Set(tag);
                    clanPrevNum = t;
                }
            }

            if (Settings.userSettings.MiscSettings.ClanChangerTheme == Settings.ClanChangerTheme.SignatureTheme)
            {
                string tag = String.Empty;
                int t = ((int)(Local.GlobalVar.curtime * 1.5) % 11);
                switch (t)
                {
                    case 0: tag = "____ ________"; break;
                    case 1: tag = "_a__ _______a"; break;
                    case 2: tag = "_a_c _______a"; break;
                    case 3: tag = "Da_c _______a"; break;
                    case 4: tag = "Da_c E______a"; break;
                    case 5: tag = "Da_c E__h___a"; break;
                    case 6: tag = "Da_c E__h__ia"; break;
                    case 7: tag = "Da_c E__ho_ia"; break;
                    case 8: tag = "Da_c E_pho_ia"; break;
                    case 9: tag = "Darc E_phoria"; break;
                    case 10: tag = "Darc Euphoria"; break;

                    default: break;
                }
                if (clanPrevNum != t)
                {
                    SetClanTag.Set(tag);
                    clanPrevNum = t;
                }
            }

            if (Settings.userSettings.MiscSettings.ClanChangerTheme == Settings.ClanChangerTheme.Static)
                SetClanTag.Set("[Darc Euphoria]");

            if (Settings.userSettings.MiscSettings.ClanChangerTheme == Settings.ClanChangerTheme.Custom)
                SetClanTag.Set(Settings.userSettings.MiscSettings.ClanChanger);

            if (Settings.userSettings.MiscSettings.ClanChangerTheme == Settings.ClanChangerTheme.Hidden)
            {
                SetClanTag.Set("\n\n\n\n");
            }
        }
    }
}
