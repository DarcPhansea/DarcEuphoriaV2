using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Darc_Euphoria.Euphoric.Structs;
using static Darc_Euphoria.Euphoric.MathFuncs;
using Darc_Euphoria.Euphoric;

namespace Darc_Euphoria.Hacks
{
    class Glow
    {

        public static void Start(Entity player, Settings.UserSettings.Visuals visuals, Settings.UserSettings.VisColors visColors)
        {
            if (!visuals.DisplayTeam && player.isTeam) return;

            int GlowObjectPtr = Memory.Read<int>(Memory.client + Offsets.dwGlowObjectManager);
            GlowSettings glowSettings = new GlowSettings(true, true, visuals.FullBloom);
            GlowColor glowColor = new GlowColor();

            if (visuals.PseudoChams)
            {
                if (player.isTeam)
                {
                    glowColor = visColors.Team_Chams.toGlow();
                    player.renderColor = visColors.Team_Chams.toRender();
                }
                else
                {
                    glowColor = visColors.Enemy_Chams.toGlow();
                    player.renderColor = visColors.Enemy_Chams.toRender();
                }
            }
            else
            {
                if (player.isTeam)
                {
                    if (player.Visible) glowColor = visColors.Team_Glow_Visible.toGlow();
                    else glowColor = visColors.Team_Glow_Hidden.toGlow();
                }
                else
                {
                    if (player.Visible) glowColor = visColors.Enemy_Glow_Visible.toGlow();
                    else glowColor = visColors.Enemy_Glow_Hidden.toGlow();
                }
            }

            Memory.Write<GlowColor>(GlowObjectPtr + (player.GlowIndex * 0x38 + 0x4), glowColor);
            Memory.Write<GlowSettings>(GlowObjectPtr + (player.GlowIndex * 0x38 + 0x24), glowSettings);

            if (visuals.PseudoChams)
                Memory.Write<int>(GlowObjectPtr + (player.GlowIndex * 0x38 + 0x2C), 1);
        }
    }
}
