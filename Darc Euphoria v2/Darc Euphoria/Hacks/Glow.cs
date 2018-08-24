using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Config;
using Darc_Euphoria.Euphoric.Objects;
using Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Hacks
{
    internal class Glow
    {
        public static void Start(Entity player, Settings.UserSettings.Visuals visuals,
            Settings.UserSettings.VisColors visColors)
        {
            if (!visuals.DisplayTeam && player.isTeam) return;

            var GlowObjectPtr = Memory.Read<int>(Memory.Client + Offsets.dwGlowObjectManager);
            var glowSettings = new GlowSettings(true, true, visuals.FullBloom);
            var glowColor = new GlowColor();

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

            Memory.Write(GlowObjectPtr + player.GlowIndex * 0x38 + 0x4, glowColor);
            Memory.Write(GlowObjectPtr + player.GlowIndex * 0x38 + 0x24, glowSettings);

            if (visuals.PseudoChams)
                Memory.Write(GlowObjectPtr + player.GlowIndex * 0x38 + 0x2C, 1);
        }
    }
}