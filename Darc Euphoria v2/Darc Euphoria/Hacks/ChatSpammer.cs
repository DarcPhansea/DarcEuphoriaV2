using System;
using System.Threading;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Hacks.Injection;

namespace Darc_Euphoria.Hacks
{
    public static class ChatSpammer
    {
        private static readonly int prevNum = 0;

        public static void Start()
        {
            if (Settings.userSettings.MiscSettings.ChatSpammer)
            {
                var r = new Random();
                var t = 0;
                do
                {
                    t = r.Next(0, 9);
                } while (t == prevNum);

                switch (t)
                {
                    case 0:
                        ClientCMD.Exec("say Get Mediocre, Go Euphoric.");
                        break;
                    case 1:
                        ClientCMD.Exec("say Darc Euphoira, an alright C# External Cheat.");
                        break;
                    case 2:
                        ClientCMD.Exec("say Get Mediocre, Get Darc Euphoria.");
                        break;
                    case 3:
                        ClientCMD.Exec("say Don't cry if I kill you. It's just a good gaming chair.");
                        break;
                    case 4:
                        ClientCMD.Exec("say If you say Darc Euphoria is shit, I won't argue, cause it is.");
                        break;
                    case 5:
                        ClientCMD.Exec("say Phansea owns Darc Euphoria and me.");
                        break;
                    case 6:
                        ClientCMD.Exec("say Why you mad? Is it because I rejected your confession?");
                        break;
                    case 7:
                        ClientCMD.Exec("say If you kill me it's luck.");
                        break;
                    case 8:
                        ClientCMD.Exec("say If I kill you it's because you suck.");
                        break;
                    default: break;
                }

                Thread.Sleep(20);
            }
        }
    }
}