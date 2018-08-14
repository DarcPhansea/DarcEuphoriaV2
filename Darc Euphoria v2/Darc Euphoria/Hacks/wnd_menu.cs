using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Controls;
using Darc_Euphoria.Euphoric.Objects;
using Darc_Euphoria.Hacks;
using Darc_Euphoria.Hacks.Injection;
using static Darc_Euphoria.Euphoric.Structs;
using Darc_Euphoria.Euphoric.Config;

namespace Darc_Euphoria
{
    public partial class wnd_menu : Form
    {
        public string ConfigName = "Default";
        public double ConfigVerson = 1;

        public void UpdateConfig(bool init)
        {
            Settings.userSettings.ConfigName = ConfigName;
            Settings.userSettings.ConfigVersion = ConfigVerson;

            #region AimbotSettings
            Settings.UserSettings.Aimbot.ASettings _Aimbot = 
                new Settings.UserSettings.Aimbot.ASettings();
            _Aimbot.Enabled = aimbot_chk_enable.Checked;
            _Aimbot.SpottedCheck = aimbot_chk_spotted.Checked;
            _Aimbot.VisiblitiyCheck = aimbot_chk_visible.Checked;
            _Aimbot.TargetTeam = aimbot_chk_targetTeam.Checked;
            _Aimbot.AutoShoot = aimbot_chk_autoShoot.Checked;
            _Aimbot.AutoPistol = aimbot_chk_autoPistol.Checked;
            _Aimbot.RcsYaw = aimbot_slider_rcsYaw.Value;
            _Aimbot.RcsPitch = aimbot_slider_rcsPitch.Value;
            _Aimbot.SmoothYaw = aimbot_slider_smoothYaw.Value;
            _Aimbot.SmoothPitch = aimbot_slider_smoothPitch.Value;
            _Aimbot.AimbotPriority = (Settings.AimPriority)aimbot_combox_priority.SelectedIndex;
            _Aimbot.Fov = (int)aimbot_slider_fov.Value;

            int c = 0;
            if (!aimbot_rdb_aim_head.Checked)
            {
                c++;
                if (!aimbot_rdb_aim_neck.Checked)
                {
                    c++;
                    if (!aimbot_rdb_aim_upperChest.Checked)
                    {
                        c++;
                        if (!aimbot_rdb_aim_midChest.Checked) c++;
                    }
                }
            }
            _Aimbot.AimbotTarget = (Settings.AimTarget)c;

            _Aimbot.Randomized = aimbot_slider_randomize.Value;
            _Aimbot.Delay = (int)aimbot_slider_delay.Value;

            if (init)
            {
                Settings.userSettings.AimbotSettings.Pistol = _Aimbot;
                Settings.userSettings.AimbotSettings.Rifle = _Aimbot;
                Settings.userSettings.AimbotSettings.Smg = _Aimbot;
                Settings.userSettings.AimbotSettings.Sniper = _Aimbot;
                Settings.userSettings.AimbotSettings.Shotgun = _Aimbot;
                Settings.userSettings.AimbotSettings.Lmg = _Aimbot;
                Settings.userSettings.AimbotSettings.Key = 0x1;
            }
            else
            {
                switch (aimbot_list_weaponCFG.SelectedIndex)
                {
                    case 0:
                        Settings.userSettings.AimbotSettings.Pistol = _Aimbot; break;
                    case 1:
                        Settings.userSettings.AimbotSettings.Rifle = _Aimbot; break;
                    case 2:
                        Settings.userSettings.AimbotSettings.Smg = _Aimbot; break;
                    case 3:
                        Settings.userSettings.AimbotSettings.Sniper = _Aimbot; break;
                    case 4:
                        Settings.userSettings.AimbotSettings.Shotgun = _Aimbot; break;
                    case 5:
                        Settings.userSettings.AimbotSettings.Lmg = _Aimbot; break;
                }
            }
            #endregion

            #region TriggerbotSettings
            Settings.UserSettings.Triggerbot.TSettings 
                _Triggerbot = new Settings.UserSettings.Triggerbot.TSettings();
            _Triggerbot.Enabled = triggerbot_chk_enabled.Checked;
            _Triggerbot.TriggerbotMode = (Settings.TriggerMode)triggerbot_combox_mode.SelectedIndex;
            _Triggerbot.TargetTeam = triggerbot_chk_targetTeam.Checked;
            _Triggerbot.BurstAmount = (int)triggerbot_slider_burstAmount.Value;
            _Triggerbot.Delay = (int)triggerbot_slider_delay.Value;

            if (init)
            {
                _Triggerbot.BurstAmount = 2;
                Settings.userSettings.TriggerbotSettings.Pistol = _Triggerbot;
                Settings.userSettings.TriggerbotSettings.Rifle = _Triggerbot;
                Settings.userSettings.TriggerbotSettings.Smg = _Triggerbot;
                Settings.userSettings.TriggerbotSettings.Sniper = _Triggerbot;
                Settings.userSettings.TriggerbotSettings.Shotgun = _Triggerbot;
                Settings.userSettings.TriggerbotSettings.Lmg = _Triggerbot;
            }
            else
            {
                switch (triggerbot_list_weaponCFG.SelectedIndex)
                {
                    case 0:
                        Settings.userSettings.TriggerbotSettings.Pistol = _Triggerbot; break;
                    case 1:
                        Settings.userSettings.TriggerbotSettings.Rifle = _Triggerbot; break;
                    case 2:
                        Settings.userSettings.TriggerbotSettings.Smg = _Triggerbot; break;
                    case 3:
                        Settings.userSettings.TriggerbotSettings.Sniper = _Triggerbot; break;
                    case 4:
                        Settings.userSettings.TriggerbotSettings.Shotgun = _Triggerbot; break;
                    case 5:
                        Settings.userSettings.TriggerbotSettings.Lmg = _Triggerbot; break;
                }
            }
            Settings.userSettings.TriggerbotSettings.Knifebot = triggerbot_chk_knifeBot.Checked;
            #endregion

            #region VisualSettings
            Settings.userSettings.VisualSettings.Enabled = visuals_chk_enabled.Checked;
            Settings.userSettings.VisualSettings.DisplayTeam = visuals_chk_displayTeam.Checked;
            Settings.userSettings.VisualSettings.BoxEsp = (Settings.BoxDisplay)visuals_combox_boxDisplay.SelectedIndex;
            Settings.userSettings.VisualSettings.BoxOutline = visuals_chk_boxOutline.Checked;
            Settings.userSettings.VisualSettings.Glow = visuals_chk_Glow.Checked;
            Settings.userSettings.VisualSettings.FullBloom = visuals_chk_fullBloom.Checked;
            Settings.userSettings.VisualSettings.HealthPostion = (Settings.HealthDisplay)visuals_combox_healthDisplay.SelectedIndex;
            Settings.userSettings.VisualSettings.HealthNumber = visuals_chk_hpNumber.Checked;
            Settings.userSettings.VisualSettings.Name = visuals_chk_name.Checked;
            Settings.userSettings.VisualSettings.ActiveWeapon = visuals_chk_activeWeapon.Checked;
            Settings.userSettings.VisualSettings.Rank = visuals_chk_rank.Checked;
            Settings.userSettings.VisualSettings.Snaplines = visuals_chk_snaplines.Checked;
            Settings.userSettings.VisualSettings.PseudoChams = visuals_chk_pseudoChams.Checked;
            Settings.userSettings.VisualSettings.ItemEsp = visuals_chk_itemEsp.Checked;
            Settings.userSettings.VisualSettings.ChickenEsp = visuals_chk_chickenEsp.Checked;
            Settings.userSettings.VisualSettings.PlantedC4Esp = visuals_chk_plantedC4Esp.Checked;
            Settings.userSettings.VisualSettings.DefuserEsp = visuals_chk_defuserEsp.Checked;
            Settings.userSettings.VisualSettings.GrenadesEsp = visuals_chk_grenadesEsp.Checked;
            Settings.userSettings.VisualSettings.DrawAimbotFov = visuals_chk_aimbotFov.Checked;
            Settings.userSettings.VisualSettings.RecoilCrosshair = visuals_chk_recoilCrosshair.Checked;
            Settings.userSettings.VisualSettings.SniperCrosshair = visuals_chk_sniperCrosshair.Checked;
            Settings.userSettings.VisualSettings.NoHands = visuals_chk_noHands.Checked;
            Settings.userSettings.VisualSettings.NoScope = visuals_chk_noScope.Checked;
            #endregion

            #region MiscSettings
            Settings.userSettings.MiscSettings.BunnyHop = misc_chk_bunnyHop.Checked;
            Settings.userSettings.MiscSettings._3rdPerson = misc_chk_3rdPerson.Checked;
            Settings.userSettings.MiscSettings.NoPostProcessing = misc_chk_noPostProcessing.Checked;
            Settings.userSettings.MiscSettings.Spectators = misc_chk_spectators.Checked;
            Settings.userSettings.MiscSettings.C4Countdown = misc_chk_c4Countdown.Checked;
            Settings.userSettings.MiscSettings.FlashAlpha = (int)misc_slider_flashAlpha.Value;
            Settings.userSettings.MiscSettings.Fov = (int)misc_slider_fov.Value;
            Settings.userSettings.MiscSettings.FakeLag = misc_chk_fakeLag.Checked;
            Settings.userSettings.MiscSettings.FakeLagAmount = (int)misc_slider_lagAmount.Value;
            Settings.userSettings.MiscSettings.Watermark = misc_chk_watermark.Checked;
            Settings.userSettings.MiscSettings.LocalTime = misc_chk_localTime.Checked;
            Settings.userSettings.MiscSettings.RankRevealer = misc_chk_rankRevealer.Checked;
            Settings.userSettings.MiscSettings._3rdPersonKey = 0xA4;
            Settings.userSettings.ForceUpdateKey = 0x70;
            Settings.userSettings.MiscSettings.ChatSpammer = misc_chk_chatspam.Checked;
            #endregion
        }

        public void UpdateClanChanger(bool init)
        {
            if (init)
            {
                Settings.userSettings.MiscSettings.ClanChangerTheme = Settings.ClanChangerTheme.Off;
                Settings.userSettings.MiscSettings.ClanChanger = "";
                Settings.userSettings.MiscSettings.NameChanger = "";
            }
            else
            {
                Settings.userSettings.MiscSettings.ClanChangerTheme = (Settings.ClanChangerTheme)misc_combox_clanChangerTheme.SelectedIndex;
                Settings.userSettings.MiscSettings.ClanChanger = misc_txt_clanChanger.Text;
                Settings.userSettings.MiscSettings.NameChanger = misc_txt_nameChanger.Text;
            }
        }

        public void UpdateSkinChanger(bool init)
        {
            Settings._SkinSettings skin = new Settings._SkinSettings();
            if (init)
            {
                skin.Name = "";
                skin.SkinID = 1337;
                skin.Seed = 0;
                skin.Wear = 0;
                skin.StatTrak = -1;
            }
            else
            {
                skin.Name = skin_txt_name.Text;
                skin.Wear = (float)skins_slider_wear.Value;
                skin.StatTrak = int.Parse(skin_txt_statTrak.Text);
                skin.Seed = int.Parse(skin_txt_seed.Text);
                var skinEnum = Enum.GetNames(typeof(Enums.SKINID));

                string skinS = skin_list_skinList.SelectedIndex >= 0 ? skin_list_skinList.SelectedItem.ToString() : String.Empty;
                if (skinS != String.Empty)
                {
                    foreach (var skinName in skinEnum)
                    {
                        if (skinS == skinName)
                            skin.SkinID = (int)Enum.Parse(typeof(Enums.SKINID), skinS);
                    }
                }
                else
                {
                    skin.SkinID = 1337;
                }
            }

            if (init)
            {
                Settings.userSkinSettings.Cz75a = skin;
                Settings.userSkinSettings.Deagle = skin;
                Settings.userSkinSettings.DuelBerettas = skin;
                Settings.userSkinSettings.FiveSeven = skin;
                Settings.userSkinSettings.Glock18 = skin;
                Settings.userSkinSettings.P2000 = skin;
                Settings.userSkinSettings.P250 = skin;
                Settings.userSkinSettings.R8Revolver = skin;
                Settings.userSkinSettings.Tec9 = skin;
                Settings.userSkinSettings.USPS = skin;
                Settings.userSkinSettings.AK47 = skin;
                Settings.userSkinSettings.Aug = skin;
                Settings.userSkinSettings.Awp = skin;
                Settings.userSkinSettings.Famas = skin;
                Settings.userSkinSettings.G3sg1 = skin;
                Settings.userSkinSettings.Galilar = skin;
                Settings.userSkinSettings.M4a1s = skin;
                Settings.userSkinSettings.M4a4 = skin;
                Settings.userSkinSettings.Scar20 = skin;
                Settings.userSkinSettings.Sg553 = skin;
                Settings.userSkinSettings.Ssg08 = skin;
                Settings.userSkinSettings.Mac10 = skin;
                Settings.userSkinSettings.Mp7 = skin;
                Settings.userSkinSettings.Mp9 = skin;
                Settings.userSkinSettings.PPBizon = skin;
                Settings.userSkinSettings.P90 = skin;
                Settings.userSkinSettings.Ump45 = skin;
                Settings.userSkinSettings.Mag7 = skin;
                Settings.userSkinSettings.Nova = skin;
                Settings.userSkinSettings.SawedOff = skin;
                Settings.userSkinSettings.Xm1014 = skin;
                Settings.userSkinSettings.M249 = skin;
                Settings.userSkinSettings.Negev = skin;
            }
            else
            {
                switch(skin_list_weaponList.SelectedIndex)
                {
                    case 0:
                        Settings.userSkinSettings.Cz75a = skin; break;
                    case 1:
                        Settings.userSkinSettings.Deagle = skin; break;
                    case 2:
                        Settings.userSkinSettings.DuelBerettas = skin; break;
                    case 3:
                        Settings.userSkinSettings.FiveSeven = skin; break;
                    case 4:
                        Settings.userSkinSettings.Glock18 = skin; break;
                    case 5:
                        Settings.userSkinSettings.P2000 = skin; break;
                    case 6:
                        Settings.userSkinSettings.P250 = skin; break;
                    case 7:
                        Settings.userSkinSettings.R8Revolver = skin; break;
                    case 8:
                        Settings.userSkinSettings.Tec9 = skin; break;
                    case 9:
                        Settings.userSkinSettings.USPS = skin; break;
                    case 10:
                        Settings.userSkinSettings.AK47 = skin; break;
                    case 11:
                        Settings.userSkinSettings.Aug = skin; break;
                    case 12:
                        Settings.userSkinSettings.Awp = skin; break;
                    case 13:
                        Settings.userSkinSettings.Famas = skin; break;
                    case 14:
                        Settings.userSkinSettings.G3sg1 = skin; break;
                    case 15:
                        Settings.userSkinSettings.Galilar = skin; break;
                    case 16:
                        Settings.userSkinSettings.M4a1s = skin; break;
                    case 17:
                        Settings.userSkinSettings.M4a4 = skin; break;
                    case 18:
                        Settings.userSkinSettings.Scar20 = skin; break;
                    case 19:
                        Settings.userSkinSettings.Sg553 = skin; break;
                    case 20:
                        Settings.userSkinSettings.Ssg08 = skin; break;
                    case 21:
                        Settings.userSkinSettings.Mac10 = skin; break;
                    case 22:
                        Settings.userSkinSettings.Mp7 = skin; break;
                    case 23:
                        Settings.userSkinSettings.Mp9 = skin; break;
                    case 24:
                        Settings.userSkinSettings.PPBizon = skin; break;
                    case 25:
                        Settings.userSkinSettings.P90 = skin; break;
                    case 26:
                        Settings.userSkinSettings.Ump45 = skin; break;
                    case 27:
                        Settings.userSkinSettings.Mag7 = skin; break;
                    case 28:
                        Settings.userSkinSettings.Nova = skin; break;
                    case 29:
                        Settings.userSkinSettings.SawedOff = skin; break;
                    case 30:
                        Settings.userSkinSettings.Xm1014 = skin; break;
                    case 31:
                        Settings.userSkinSettings.M249 = skin; break;
                    case 32:
                        Settings.userSkinSettings.Negev = skin; break;
                }
                Settings.userSkinSettings.Enable = skin_chk_enable.Checked;
            }
        }

        public void UpdateColors(bool init)
        {
            if (init)
            {
                Settings.userSettings.VisualColors.Team_Box_Visible =       Color.FromArgb(255, 0, 0, 255); 
                Settings.userSettings.VisualColors.Team_Box_Hidden =        Color.FromArgb(255, 0, 165, 255);
                Settings.userSettings.VisualColors.Team_Chams =             Color.FromArgb(255, 0, 0, 255);
                Settings.userSettings.VisualColors.Team_Glow_Visible =      Color.FromArgb(255, 0, 0, 255);
                Settings.userSettings.VisualColors.Team_Glow_Hidden =       Color.FromArgb(255, 0, 165, 255);
                Settings.userSettings.VisualColors.Team_Snaplines_Visible = Color.FromArgb(255, 0, 0, 255);
                Settings.userSettings.VisualColors.Team_Snaplines_Hidden =  Color.FromArgb(255, 0, 165, 255);
                Settings.userSettings.VisualColors.Team_Text =              Color.FromArgb(255, 255, 255, 255);

                Settings.userSettings.VisualColors.Enemy_Box_Visible =      Color.FromArgb(255, 255, 0, 0);
                Settings.userSettings.VisualColors.Enemy_Box_Hidden =       Color.FromArgb(255, 255, 165, 0);
                Settings.userSettings.VisualColors.Enemy_Chams =            Color.FromArgb(255, 255, 0, 0);
                Settings.userSettings.VisualColors.Enemy_Glow_Visible =     Color.FromArgb(255, 255, 0, 0);
                Settings.userSettings.VisualColors.Enemy_Glow_Hidden =      Color.FromArgb(255, 255, 165, 0);
                Settings.userSettings.VisualColors.Enemy_Snaplines_Visible =Color.FromArgb(255, 255, 0, 0);
                Settings.userSettings.VisualColors.Enemy_Snaplines_Hidden = Color.FromArgb(255, 255, 165, 0);
                Settings.userSettings.VisualColors.Enemy_Text =             Color.FromArgb(255, 255, 255, 255);

                Settings.userSettings.VisualColors.World_Text =             Color.FromArgb(255, 255, 255, 255);
            }
            else
            {
                switch(colors_list_colorList.SelectedIndex)
                {
                    case 0:
                        Settings.userSettings.VisualColors.Team_Box_Visible = euphColorPicker.SelectedColor; break;
                    case 1:
                        Settings.userSettings.VisualColors.Team_Box_Hidden = euphColorPicker.SelectedColor; break;
                    case 2:
                        Settings.userSettings.VisualColors.Team_Chams = euphColorPicker.SelectedColor; break;
                    case 3:
                        Settings.userSettings.VisualColors.Team_Glow_Visible = euphColorPicker.SelectedColor; break;
                    case 4:
                        Settings.userSettings.VisualColors.Team_Glow_Hidden = euphColorPicker.SelectedColor; break;
                    case 5:
                        Settings.userSettings.VisualColors.Team_Snaplines_Visible = euphColorPicker.SelectedColor; break;
                    case 6:
                        Settings.userSettings.VisualColors.Team_Snaplines_Hidden = euphColorPicker.SelectedColor; break;
                    case 7:
                        Settings.userSettings.VisualColors.Team_Text = euphColorPicker.SelectedColor; break;
                    case 8:
                        Settings.userSettings.VisualColors.Enemy_Box_Visible = euphColorPicker.SelectedColor; break;
                    case 9:
                        Settings.userSettings.VisualColors.Enemy_Box_Hidden = euphColorPicker.SelectedColor; break;
                    case 10:
                        Settings.userSettings.VisualColors.Enemy_Chams = euphColorPicker.SelectedColor; break;
                    case 11:
                        Settings.userSettings.VisualColors.Enemy_Glow_Visible = euphColorPicker.SelectedColor; break;
                    case 12:
                        Settings.userSettings.VisualColors.Enemy_Glow_Hidden = euphColorPicker.SelectedColor; break;
                    case 13:
                        Settings.userSettings.VisualColors.Enemy_Snaplines_Visible = euphColorPicker.SelectedColor; break;
                    case 14:
                        Settings.userSettings.VisualColors.Enemy_Snaplines_Hidden = euphColorPicker.SelectedColor; break;
                    case 15:
                        Settings.userSettings.VisualColors.Enemy_Text = euphColorPicker.SelectedColor; break;
                    case 16:
                        Settings.userSettings.VisualColors.World_Text = euphColorPicker.SelectedColor; break;
                }
            }
        }

        public void SwitchColors(int i)
        {
            switch (i)
            {
                case 0: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Team_Box_Visible; break;
                case 1: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Team_Box_Hidden; break;
                case 2: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Team_Chams; break;
                case 3: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Team_Glow_Visible; break;
                case 4: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Team_Glow_Hidden;break;
                case 5: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Team_Snaplines_Visible; break;
                case 6: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Team_Snaplines_Hidden; break;
                case 7: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Team_Text; break;
                case 8: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Enemy_Box_Visible; break;
                case 9: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Enemy_Box_Hidden; break;
                case 10: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Enemy_Chams; break;
                case 11: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Enemy_Glow_Visible; break;
                case 12: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Enemy_Glow_Hidden; break;
                case 13: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Enemy_Snaplines_Visible; break;
                case 14: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Enemy_Snaplines_Hidden; break;
                case 15: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.Enemy_Text; break;
                case 16: euphColorPicker.SelectedColor = Settings.userSettings.VisualColors.World_Text; break;
            }
        }

        private void updateCheatList()
        {
            configs_list_cheatCfg.Items.Clear();
            config_list_skinCfg.Items.Clear();
            var list = Directory.GetFiles(Application.StartupPath);
            foreach (var item in list)
            {
                if (item.EndsWith(".dec") && !item.EndsWith(".skins.dec"))
                {
                    configs_list_cheatCfg.Items.Add(item.Substring(Application.StartupPath.Length));
                }

                if (item.EndsWith(".skins.dec"))
                {
                    config_list_skinCfg.Items.Add(item.Substring(Application.StartupPath.Length));
                }
            }

        }

        public void LoadConfig()
        {
            for(int i = 0; i < 10; i++)
            {
                Settings.userSettings = configmaker.load<Settings.UserSettings>(Application.StartupPath + configs_list_cheatCfg.SelectedItem.ToString());
            
                #region Aimbot
                aimbot_list_weaponCFG.SelectedIndex = 0;
                aimbot_chk_enable.Enabled = Settings.userSettings.AimbotSettings.Pistol.Enabled;
                aimbot_btn_key.Text = "0x" + Settings.userSettings.AimbotSettings.Key.ToString("x").ToUpper();
                aimbot_chk_spotted.Checked = Settings.userSettings.AimbotSettings.Pistol.SpottedCheck;
                aimbot_chk_visible.Checked = Settings.userSettings.AimbotSettings.Pistol.VisiblitiyCheck;
                aimbot_chk_targetTeam.Checked= Settings.userSettings.AimbotSettings.Pistol.TargetTeam;
                aimbot_chk_autoShoot.Checked = Settings.userSettings.AimbotSettings.Pistol.AutoShoot;
                aimbot_chk_autoPistol.Checked = Settings.userSettings.AimbotSettings.Pistol.AutoPistol;
                aimbot_slider_rcsYaw.Value = Settings.userSettings.AimbotSettings.Pistol.RcsYaw;
                aimbot_slider_rcsPitch.Value = Settings.userSettings.AimbotSettings.Pistol.RcsPitch;
                aimbot_slider_smoothYaw.Value = Settings.userSettings.AimbotSettings.Pistol.SmoothYaw;
                aimbot_slider_smoothPitch.Value = Settings.userSettings.AimbotSettings.Pistol.SmoothPitch;
                aimbot_combox_priority.SelectedIndex = (int)Settings.userSettings.AimbotSettings.Pistol.AimbotPriority;
                aimbot_slider_fov.Value = Settings.userSettings.AimbotSettings.Pistol.Fov;
                aimbot_slider_randomize.Value = Settings.userSettings.AimbotSettings.Pistol.Randomized;
                aimbot_slider_delay.Value = Settings.userSettings.AimbotSettings.Pistol.Delay;

                if (Settings.userSettings.AimbotSettings.Pistol.AimbotTarget == Settings.AimTarget.Head)
                    aimbot_rdb_aim_head.Checked = true;
                else if (Settings.userSettings.AimbotSettings.Pistol.AimbotTarget == Settings.AimTarget.Neck)
                    aimbot_rdb_aim_neck.Checked = true;
                else if (Settings.userSettings.AimbotSettings.Pistol.AimbotTarget == Settings.AimTarget.UpperChest)
                    aimbot_rdb_aim_upperChest.Checked = true;
                else if (Settings.userSettings.AimbotSettings.Pistol.AimbotTarget == Settings.AimTarget.MiddleChest)
                    aimbot_rdb_aim_midChest.Checked = true;
                else if (Settings.userSettings.AimbotSettings.Pistol.AimbotTarget == Settings.AimTarget.LowerChest)
                    aimbot_rdb_aim_lowerChest.Checked = true;
                #endregion

                #region Triggerbot
                triggerbot_list_weaponCFG.SelectedIndex = 0;
                triggerbot_chk_enabled.Checked = Settings.userSettings.TriggerbotSettings.Pistol.Enabled;
                triggerbot_chk_knifeBot.Checked = Settings.userSettings.TriggerbotSettings.Knifebot;
                triggerbot_slider_burstAmount.Value = Settings.userSettings.TriggerbotSettings.Pistol.BurstAmount;
                triggerbot_slider_delay.Value = Settings.userSettings.TriggerbotSettings.Pistol.Delay;
                triggerbot_chk_targetTeam.Checked = Settings.userSettings.TriggerbotSettings.Pistol.TargetTeam;
                triggerbot_combox_mode.SelectedIndex = (int)Settings.userSettings.TriggerbotSettings.Pistol.TriggerbotMode;
                #endregion

                #region Visuals
                visuals_chk_enabled.Checked = Settings.userSettings.VisualSettings.Enabled;
                visuals_chk_displayTeam.Checked = Settings.userSettings.VisualSettings.DisplayTeam;
                visuals_chk_boxOutline.Checked = Settings.userSettings.VisualSettings.BoxOutline;
                visuals_chk_Glow.Checked = Settings.userSettings.VisualSettings.Glow;
                visuals_chk_fullBloom.Checked = Settings.userSettings.VisualSettings.FullBloom;
                visuals_chk_hpNumber.Checked = Settings.userSettings.VisualSettings.HealthNumber;
                visuals_chk_pseudoChams.Checked = Settings.userSettings.VisualSettings.PseudoChams;
                visuals_chk_name.Checked = Settings.userSettings.VisualSettings.Name;
                visuals_chk_activeWeapon.Checked = Settings.userSettings.VisualSettings.ActiveWeapon;
                visuals_chk_rank.Checked = Settings.userSettings.VisualSettings.Rank;
                visuals_chk_snaplines.Checked = Settings.userSettings.VisualSettings.Snaplines;
                visuals_chk_itemEsp.Checked = Settings.userSettings.VisualSettings.ItemEsp;
                visuals_chk_chickenEsp.Checked = Settings.userSettings.VisualSettings.ChickenEsp;
                visuals_chk_plantedC4Esp.Checked = Settings.userSettings.VisualSettings.PlantedC4Esp;
                visuals_chk_defuserEsp.Checked = Settings.userSettings.VisualSettings.DefuserEsp;
                visuals_chk_grenadesEsp.Checked =  Settings.userSettings.VisualSettings.GrenadesEsp;
                visuals_chk_aimbotFov.Checked = Settings.userSettings.VisualSettings.DrawAimbotFov;
                visuals_chk_recoilCrosshair.Checked = Settings.userSettings.VisualSettings.RecoilCrosshair;
                visuals_chk_sniperCrosshair.Checked = Settings.userSettings.VisualSettings.SniperCrosshair;
                visuals_chk_noHands.Checked = Settings.userSettings.VisualSettings.NoHands;
                visuals_chk_noScope.Checked = Settings.userSettings.VisualSettings.NoScope;
                visuals_combox_boxDisplay.SelectedIndex = (int)Settings.userSettings.VisualSettings.BoxEsp;
                visuals_combox_healthDisplay.SelectedIndex = (int)Settings.userSettings.VisualSettings.HealthPostion;
                #endregion

                #region Misc
                misc_chk_bunnyHop.Checked = Settings.userSettings.MiscSettings.BunnyHop;
                misc_chk_noPostProcessing.Checked = Settings.userSettings.MiscSettings.NoPostProcessing;
                misc_chk_spectators.Checked = Settings.userSettings.MiscSettings.Spectators;
                misc_chk_c4Countdown.Checked = Settings.userSettings.MiscSettings.C4Countdown;
                misc_chk_3rdPerson.Checked = Settings.userSettings.MiscSettings._3rdPerson;
                misc_chk_rankRevealer.Checked = Settings.userSettings.MiscSettings.RankRevealer;
                misc_slider_flashAlpha.Value = Settings.userSettings.MiscSettings.FlashAlpha;
                misc_slider_fov.Value = Settings.userSettings.MiscSettings.Fov;
                misc_chk_fakeLag.Checked = Settings.userSettings.MiscSettings.FakeLag;
                misc_slider_lagAmount.Value = Settings.userSettings.MiscSettings.FakeLagAmount;
                misc_chk_watermark.Checked = Settings.userSettings.MiscSettings.Watermark;
                misc_chk_localTime.Checked = Settings.userSettings.MiscSettings.LocalTime;
                misc_chk_chatspam.Checked = Settings.userSettings.MiscSettings.ChatSpammer;
                misc_btn_3rdpersonkey.Text = "0x" + Settings.userSettings.MiscSettings._3rdPersonKey.ToString("x").ToUpper();
                misc_txt_nameChanger.Text = Settings.userSettings.MiscSettings.NameChanger;
                misc_txt_clanChanger.Text = Settings.userSettings.MiscSettings.ClanChanger;
                misc_combox_clanChangerTheme.SelectedIndex = (int)Settings.userSettings.MiscSettings.ClanChangerTheme;
                #endregion

                skin_btn_forceUpdate.Text = "0x" + Settings.userSettings.ForceUpdateKey.ToString("x").ToUpper();
                UpdateColors(false);
            }
        }

        List<Control> controlList;
        private IEnumerable<Control> GetControlHierarchy(Control root)
        {
            var queue = new Queue<Control>();

            queue.Enqueue(root);

            do
            {
                var control = queue.Dequeue();

                yield return control;

                foreach (var child in control.Controls.OfType<Control>())
                    queue.Enqueue(child);

            } while (queue.Count > 0);
        }

        #region Form Rounded
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,      
            int nRightRect,    
            int nBottomRect,  
            int nWidthEllipse, 
            int nHeightEllipse
        );
        #endregion

        #region Threads
        Thread aimbotThread = new Thread(new ThreadStart(Aimbot.Start))
        {
            Priority = ThreadPriority.Highest,
            IsBackground = true,
        };
        Thread entityUpdate = new Thread(new ThreadStart(EntityList.Start))
        {
            Priority = ThreadPriority.Highest,
            IsBackground = true,
        };
        Thread miscThread = new Thread(new ThreadStart(Misc.Start))
        {
            Priority = ThreadPriority.Highest,
            IsBackground = true,
        };
        Thread fakeLagThread = new Thread(new ThreadStart(FakeLag.Start))
        {
            Priority = ThreadPriority.Highest,
            IsBackground = true,
        };
        #endregion

        #region WndMove
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void wnd_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }
        #endregion

        public wnd_menu()
        {
            InitializeComponent();

            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            if (!Sig.Init())
            {
                MessageBox.Show("Failed to Load!");
                Environment.Exit(Environment.ExitCode);
            }
            Sig.InitOffsets();

            controlList = GetControlHierarchy(this).ToList();

            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint
                , true);

            #region Region Setup
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 11, 11));

            label13.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0,
                label13.Width,
                label13.Height, 9, 9));

            lbl_CheatHeader.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0,
                lbl_CheatHeader.Width + 10,
                lbl_CheatHeader.Height + 10, 11, 11));

            menu_cheats.DrawMode = TabDrawMode.OwnerDrawFixed;
            menu_cheats.Region = new Region(new RectangleF(tab_aimbot.Left, tab_aimbot.Top, tab_aimbot.Width, tab_aimbot.Height));
            #endregion


            Local._bsp.FileName = "null";
            Thread.Sleep(1);
            entityUpdate.Start();
            miscThread.Start();
            aimbotThread.Start();
            fakeLagThread.Start();

            ClientCMD.Exec("clear; echo Darc Euphoria Loaded");

            //This Is For Me
            ClientCMD.Exec("exec autoexec");
        }

        #region Updates
        private void Cc_MouseDown(object sender, MouseEventArgs e)
        {
            UpdateConfig(false);
        }

        private void Cc_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateConfig(false);
        }

        private void Cc_CheckedChanged(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(CheckBox))
            {
                CheckBox Snd = sender as CheckBox;

                if (Snd.Name == "visuals_chk_pseudoChams" || Snd.Name == "visuals_chk_enabled" ||
                    Snd.Name == "visuals_chk_displayTeam")
                {
                    if (!((CheckBox)sender).Checked)
                        Local.ForceUpdate();

                }
            }

            UpdateConfig(false);
        }

        private void Cc_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateConfig(false);
        }

        private void wnd_menu_Load(object sender, EventArgs e)
        {
            
            #region InitControls

            foreach (Control c in controlList)
            {
                if (c.GetType() == typeof(CheckBox))
                    c.Paint += Master_Toggle_Paint;

                if (c.GetType() == typeof(ListBox))
                {
                    ListBox cc = c as ListBox;
                    if (cc.Items.Count > 0)
                        cc.SelectedIndex = 0;

                    cc.DrawItem += List_DrawItem;

                    cc.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0,
                        cc.Width,
                        cc.Height, 9, 9));
                }

                if (c.GetType() == typeof(TextBox) && c.Parent.GetType() != typeof(EuphoricColorPicker))
                {
                    TextBox cc = c as TextBox;
                    cc.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0,
                        cc.Width,
                        cc.Height, 5, 5));
                }

                if (c.GetType() == typeof(Button))
                {
                    Button cc = c as Button;
                    cc.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0,
                        cc.Width,
                        cc.Height, 9, 9));
                }

                if (c.GetType() == typeof(ComboBox))
                {
                    ComboBox cc = c as ComboBox;
                    cc.SelectedIndex = 0;
                    cc.DrawItem += Combo_DrawItem;
                }

                if (c.Parent == aimbot_grp_aim)
                    c.Paint += Master_Radio_Paint;
            }

            foreach (Control c in controlList)
            {
                if (c.GetType() == typeof(ComboBox))
                {
                    ComboBox cc = c as ComboBox;
                    cc.SelectedIndexChanged += Cc_SelectedIndexChanged;
                }
                if (c.GetType() == typeof(CheckBox))
                {
                    CheckBox cc = c as CheckBox;
                    cc.CheckedChanged += Cc_CheckedChanged;
                }
                if (c.GetType() == typeof(EuphoricSlider))
                {
                    EuphoricSlider cc = c as EuphoricSlider;
                    cc.MouseDown += Cc_MouseDown;
                    cc.MouseMove += Cc_MouseMove;
                }
                if (c.Parent == aimbot_grp_aim)
                {
                    RadioButton cc = c as RadioButton;
                    cc.CheckedChanged += Cc_CheckedChanged;
                }

            }
            #endregion

            var skinEnum = Enum.GetNames(typeof(Enums.SKINID));
            foreach (var skin in skinEnum)
            {
                skin_list_skinList.Items.Add(skin);
            }
            skin_list_skinList.Sorted = true;

            UpdateConfig(true);
            UpdateSkinChanger(true);
            UpdateClanChanger(true);
            UpdateColors(true);
            SwitchColors(0);
            updateCheatList();


            menu_Thread.RunWorkerAsync();

            wnd_overlay overlay = new wnd_overlay();
            overlay.Show();


            Rect rect = new Rect();
            WinAPI.GetClientRect(Memory.process.MainWindowHandle, out rect);

            this.Left = (rect.Right / 2) - (this.Width / 2);
            this.Top = (rect.Bottom / 2) - (this.Height / 2);
        }
        #endregion
        
        #region PAINTING
        private void Combo_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox combox = sender as ComboBox;

            bool HotLight = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            bool editArea = (e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit;

            if (HotLight && !editArea)
                using (Brush b = new SolidBrush(lbl_CheatHeader.BackColor))
                    e.Graphics.FillRectangle(b, e.Bounds);
            else
                using (Brush b = new SolidBrush(this.BackColor))
                    e.Graphics.FillRectangle(b, e.Bounds);


            using (Brush b = new SolidBrush(this.ForeColor))
                e.Graphics.DrawString(combox.Items[e.Index].ToString(), e.Font, b, e.Bounds.X, e.Bounds.Y + 1);

        }

        private void Master_Toggle_Paint(object sender, PaintEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            using (Brush b = new SolidBrush(tab_aimbot.BackColor))
                e.Graphics.FillRectangle(b, 0, 0, cb.Width, cb.Height);

            using (Brush b = new SolidBrush(this.BackColor))
                e.Graphics.FillRectangle(b, 0, 6, 18, 18);

            using (Brush b = new SolidBrush(this.ForeColor))
                e.Graphics.DrawString(cb.Text, cb.Font, b, 20, 6);

            if (!cb.Checked) return;

            using (Brush b = new SolidBrush(lbl_CheatHeader.BackColor))
                e.Graphics.FillRectangle(b, 2, 8, 14, 14);
        }

        private void Master_Radio_Paint(object sender, PaintEventArgs e)
        {
            RadioButton cb = sender as RadioButton;

            using (Brush b = new SolidBrush(cb.BackColor))
                e.Graphics.FillRectangle(b, 0, 0, cb.Width, cb.Height);

            using (Brush b = new SolidBrush(this.BackColor))
                e.Graphics.FillEllipse(b, 0, 6, 19, 19);

            using (Brush b = new SolidBrush(this.ForeColor))
                e.Graphics.DrawString(cb.Text, cb.Font, b, 20, 7);

            if (!cb.Checked) return;

            using (Brush b = new SolidBrush(lbl_CheatHeader.BackColor))
                e.Graphics.FillEllipse(b, 3, 9, 13, 13);
        }

        private void List_DrawItem(object sender, DrawItemEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            bool selelected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;

            if (selelected)
            {
                using (Brush b = new SolidBrush(lbl_CheatHeader.BackColor))
                    e.Graphics.FillRectangle(b, e.Bounds);
            }
            else
            {
                using (Brush b = new SolidBrush(listBox.BackColor))
                    e.Graphics.FillRectangle(b, e.Bounds);
            }

            try
            {
                using (Brush b = new SolidBrush(this.ForeColor))
                    e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, b, e.Bounds.X, e.Bounds.Y);
            }
            catch { }
        }
        #endregion

        #region FormInteraction
        private void cheatTabs_CheckedChanged(object sender, EventArgs e)
        {
            menu_cheats.SuspendLayout();

            RadioButton tab = sender as RadioButton;
            if (!tab.Checked) return;
            lbl_CheatHeader.Text = tab.Text;
            int index = Int32.Parse(tab.Name.Substring(4 + tab.Text.Length, tab.Name.Length - tab.Text.Length - 4));
            index -= 1;
            menu_cheats.SelectedIndex = index;

            menu_cheats.ResumeLayout();
        }

        private void aimbot_list_weaponCFG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (aimbot_list_weaponCFG.SelectedIndex == 0)
                aimbot_chk_autoPistol.Visible = true;
            else
                aimbot_chk_autoPistol.Visible = false;

            Settings.UserSettings.Aimbot.ASettings _Aimbot =
                new Settings.UserSettings.Aimbot.ASettings();

            switch (aimbot_list_weaponCFG.SelectedIndex)
            {
                case 0:
                    _Aimbot = Settings.userSettings.AimbotSettings.Pistol; break;
                case 1:
                    _Aimbot = Settings.userSettings.AimbotSettings.Rifle; break;
                case 2:
                    _Aimbot = Settings.userSettings.AimbotSettings.Smg; break;
                case 3:
                    _Aimbot = Settings.userSettings.AimbotSettings.Sniper; break;
                case 4:
                    _Aimbot = Settings.userSettings.AimbotSettings.Shotgun; break;
                case 5:
                    _Aimbot = Settings.userSettings.AimbotSettings.Lmg; break;
            }

            aimbot_chk_enable.Checked = _Aimbot.Enabled;
            aimbot_chk_spotted.Checked = _Aimbot.SpottedCheck;
            aimbot_chk_visible.Checked = _Aimbot.VisiblitiyCheck;
            aimbot_chk_targetTeam.Checked = _Aimbot.TargetTeam;
            aimbot_chk_autoShoot.Checked = _Aimbot.AutoShoot;
            aimbot_chk_autoPistol.Checked = _Aimbot.AutoPistol;
            aimbot_slider_rcsYaw.Value = _Aimbot.RcsYaw;
            aimbot_slider_rcsPitch.Value = _Aimbot.RcsPitch;
            aimbot_slider_smoothYaw.Value = _Aimbot.SmoothYaw;
            aimbot_slider_smoothPitch.Value = _Aimbot.SmoothPitch;
            aimbot_combox_priority.SelectedIndex = (int)_Aimbot.AimbotPriority;
            aimbot_slider_randomize.Value = _Aimbot.Randomized;
            aimbot_slider_delay.Value = _Aimbot.Delay;
            aimbot_slider_fov.Value = _Aimbot.Fov;

            switch ((int)_Aimbot.AimbotTarget)
            {
                case 0: aimbot_rdb_aim_head.Checked = true; break;
                case 1: aimbot_rdb_aim_neck.Checked = true; break;
                case 2: aimbot_rdb_aim_upperChest.Checked = true; break;
                case 3: aimbot_rdb_aim_midChest.Checked = true; break;
                case 4: aimbot_rdb_aim_lowerChest.Checked = true; break;
            }

            menu_cheats.Refresh();
        }

        private void triggerbot_list_weaponCFG_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.UserSettings.Triggerbot.TSettings _Triggerbot =
                new Settings.UserSettings.Triggerbot.TSettings();

            switch (triggerbot_list_weaponCFG.SelectedIndex)
            {
                case 0:
                    _Triggerbot = Settings.userSettings.TriggerbotSettings.Pistol; break;
                case 1:
                    _Triggerbot = Settings.userSettings.TriggerbotSettings.Rifle; break;
                case 2:
                    _Triggerbot = Settings.userSettings.TriggerbotSettings.Smg; break;
                case 3:
                    _Triggerbot = Settings.userSettings.TriggerbotSettings.Sniper; break;
                case 4:
                    _Triggerbot = Settings.userSettings.TriggerbotSettings.Shotgun; break;
                case 5:
                    _Triggerbot = Settings.userSettings.TriggerbotSettings.Lmg; break;
            }

            triggerbot_chk_knifeBot.Checked = Settings.userSettings.TriggerbotSettings.Knifebot;
            triggerbot_combox_mode.SelectedIndex = (int)_Triggerbot.TriggerbotMode;
            triggerbot_slider_burstAmount.Value = _Triggerbot.BurstAmount > 1 ? _Triggerbot.BurstAmount : 2;
            triggerbot_slider_delay.Value = _Triggerbot.Delay;
            triggerbot_chk_enabled.Checked = _Triggerbot.Enabled;
            triggerbot_chk_targetTeam.Checked = _Triggerbot.TargetTeam;

        }

        private void colors_list_colorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchColors(colors_list_colorList.SelectedIndex);
        }

        private void Text_Numerical_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void skin_chk_enable_MouseEnter(object sender, EventArgs e)
        {
            EuphoricToolTip.Show("Buggy For Some Reason", skin_chk_enable);
        }

        private void triggerbot_chk_enabled_MouseEnter(object sender, EventArgs e)
        {
            EuphoricToolTip.Show("Who even uses this? Am I right.", triggerbot_chk_enabled);
        }

        private void misc_btn_apply_Click(object sender, EventArgs e)
        {
            string s = Settings.userSettings.MiscSettings.NameChanger;
            UpdateClanChanger(false);
            if (Settings.userSettings.MiscSettings.ClanChangerTheme == Settings.ClanChangerTheme.Off)
                SetClanTag.Set("");

            if (Settings.userSettings.MiscSettings.NameChanger != String.Empty && Settings.userSettings.MiscSettings.NameChanger != s)
                SetName.Set(Settings.userSettings.MiscSettings.NameChanger);
        }

        private void skin_btn_apply_Click(object sender, EventArgs e)
        {
            UpdateSkinChanger(false);
        }

        private void skin_btn_clear_Click(object sender, EventArgs e)
        {
            Settings._SkinSettings skin = new Settings._SkinSettings();
            skin.Name = "";
            skin.SkinID = 1337;
            skin.Seed = 0;
            skin.Wear = 0;
            skin.StatTrak = -1;

            skin_list_skinList.SelectedIndex = -1;
            skin_txt_name.Text = "";
            skins_slider_wear.Value = 0;
            skin_txt_statTrak.Text = "-1";
            skin_txt_seed.Text = "0";

            switch (skin_list_weaponList.SelectedIndex)
            {
                case 0:
                    Settings.userSkinSettings.Cz75a = skin; break;
                case 1:
                    Settings.userSkinSettings.Deagle = skin; break;
                case 2:
                    Settings.userSkinSettings.DuelBerettas = skin; break;
                case 3:
                    Settings.userSkinSettings.FiveSeven = skin; break;
                case 4:
                    Settings.userSkinSettings.Glock18 = skin; break;
                case 5:
                    Settings.userSkinSettings.P2000 = skin; break;
                case 6:
                    Settings.userSkinSettings.P250 = skin; break;
                case 7:
                    Settings.userSkinSettings.R8Revolver = skin; break;
                case 8:
                    Settings.userSkinSettings.Tec9 = skin; break;
                case 9:
                    Settings.userSkinSettings.USPS = skin; break;
                case 10:
                    Settings.userSkinSettings.AK47 = skin; break;
                case 11:
                    Settings.userSkinSettings.Aug = skin; break;
                case 12:
                    Settings.userSkinSettings.Awp = skin; break;
                case 13:
                    Settings.userSkinSettings.Famas = skin; break;
                case 14:
                    Settings.userSkinSettings.G3sg1 = skin; break;
                case 15:
                    Settings.userSkinSettings.Galilar = skin; break;
                case 16:
                    Settings.userSkinSettings.M4a1s = skin; break;
                case 17:
                    Settings.userSkinSettings.M4a4 = skin; break;
                case 18:
                    Settings.userSkinSettings.Scar20 = skin; break;
                case 19:
                    Settings.userSkinSettings.Sg553 = skin; break;
                case 20:
                    Settings.userSkinSettings.Ssg08 = skin; break;
                case 21:
                    Settings.userSkinSettings.Mac10 = skin; break;
                case 22:
                    Settings.userSkinSettings.Mp7 = skin; break;
                case 23:
                    Settings.userSkinSettings.Mp9 = skin; break;
                case 24:
                    Settings.userSkinSettings.PPBizon = skin; break;
                case 25:
                    Settings.userSkinSettings.P90 = skin; break;
                case 26:
                    Settings.userSkinSettings.Ump45 = skin; break;
                case 27:
                    Settings.userSkinSettings.Mag7 = skin; break;
                case 28:
                    Settings.userSkinSettings.Nova = skin; break;
                case 29:
                    Settings.userSkinSettings.SawedOff = skin; break;
                case 30:
                    Settings.userSkinSettings.Xm1014 = skin; break;
                case 31:
                    Settings.userSkinSettings.M249 = skin; break;
                case 32:
                    Settings.userSkinSettings.Negev = skin; break;
            }
        }

        private void colors_btn_apply_Click(object sender, EventArgs e)
        {
            UpdateColors(false);
        }

        private void colors_btn_reset_Click(object sender, EventArgs e)
        {
            switch(colors_list_colorList.SelectedIndex)
            {
                case 0: Settings.userSettings.VisualColors.Team_Box_Visible = Color.FromArgb(255, 0, 0, 255); break;
                case 1: Settings.userSettings.VisualColors.Team_Box_Hidden = Color.FromArgb(255, 0, 165, 255); break;
                case 2: Settings.userSettings.VisualColors.Team_Chams = Color.FromArgb(255, 0, 0, 255); break;
                case 3: Settings.userSettings.VisualColors.Team_Glow_Visible = Color.FromArgb(255, 0, 0, 255); break;
                case 4: Settings.userSettings.VisualColors.Team_Glow_Hidden = Color.FromArgb(255, 0, 165, 255); break;
                case 5: Settings.userSettings.VisualColors.Team_Snaplines_Visible = Color.FromArgb(255, 0, 0, 255); break;
                case 6: Settings.userSettings.VisualColors.Team_Snaplines_Hidden = Color.FromArgb(255, 0, 165, 255); break;
                case 7: Settings.userSettings.VisualColors.Team_Text = Color.FromArgb(255, 255, 255, 255); break;
                case 8: Settings.userSettings.VisualColors.Enemy_Box_Visible = Color.FromArgb(255, 255, 0, 0); break;
                case 9: Settings.userSettings.VisualColors.Enemy_Box_Hidden = Color.FromArgb(255, 255, 165, 0); break;
                case 10: Settings.userSettings.VisualColors.Enemy_Chams = Color.FromArgb(255, 255, 0, 0); break;
                case 11: Settings.userSettings.VisualColors.Enemy_Glow_Visible = Color.FromArgb(255, 255, 0, 0); break;
                case 13: Settings.userSettings.VisualColors.Enemy_Glow_Hidden = Color.FromArgb(255, 255, 165, 0); break;
                case 14: Settings.userSettings.VisualColors.Enemy_Snaplines_Visible = Color.FromArgb(255, 255, 0, 0); break;
                case 15: Settings.userSettings.VisualColors.Enemy_Snaplines_Hidden = Color.FromArgb(255, 255, 165, 0); break;
                case 16: Settings.userSettings.VisualColors.Enemy_Text = Color.FromArgb(255, 255, 255, 255); break;
                case 17: Settings.userSettings.VisualColors.World_Text = Color.FromArgb(255, 255, 255, 255); break;
        }
            SwitchColors(colors_list_colorList.SelectedIndex);
        }

        private void skin_btn_forceUpdate_Click(object sender, EventArgs e)
        {
            Local.ForceUpdate();
        }

        private void aimbot_btn_key_Click(object sender, EventArgs e)
        {
            aimbot_btn_key.Text = "";
            aimbot_btn_key.Refresh();
            int key = 0;
            while (true)
            {
                for (int i = 0; i < 200; i++)
                {
                    if ((WinAPI.GetAsyncKeyState(i) & 0x8000) > 0)
                    {
                        key = i;
                        break;
                    }
                }
                if (key != 0)
                    break;
            }
            aimbot_btn_key.Text = "0x" + key.ToString("x").ToUpper();
            Settings.userSettings.AimbotSettings.Key = key;
        }

        private void misc_btn_3rdpersonkey_Click(object sender, EventArgs e)
        {
            misc_btn_3rdpersonkey.Text = "";
            misc_btn_3rdpersonkey.Refresh();
            int key = 0;
            while (true)
            {
                for (int i = 0; i < 200; i++)
                {
                    if ((WinAPI.GetAsyncKeyState(i) & 0x8000) > 0)
                    {
                        key = i;
                        break;
                    }
                }
                if (key != 0)
                    break;
            }
            misc_btn_3rdpersonkey.Text = "0x" + key.ToString("x").ToUpper();
            Settings.userSettings.MiscSettings._3rdPersonKey = key;
        }

        private void skin_chk_enable_CheckedChanged(object sender, EventArgs e)
        {
            if (!skin_chk_enable.Checked)
                Local.ForceUpdate();
        }

        private void skins_btn_updateKey_Click(object sender, EventArgs e)
        {
            skins_btn_updateKey.Text = "";
            skins_btn_updateKey.Refresh();
            int key = 0;
            while (true)
            {
                for (int i = 0; i < 200; i++)
                {
                    if ((WinAPI.GetAsyncKeyState(i) & 0x8000) > 0)
                    {
                        key = i;
                        break;
                    }
                }
                if (key != 0)
                    break;
            }
            skins_btn_updateKey.Text = "0x" + key.ToString("x").ToUpper();
            Settings.userSettings.ForceUpdateKey = key;
        }

        private void skin_list_weaponList_SelectedIndexChanged(object sender, EventArgs e)
        {
            skin_list_skinList.SelectedIndex = -1;
            skins_slider_wear.Value = 0;
            skin_txt_name.Text = string.Empty;
            skin_txt_statTrak.Text = "-1";
            skin_txt_seed.Text = "0";
        }

        private void skin_txt_skinSearch_KeyPress(object sender, KeyEventArgs e)
        {
            skin_list_skinList.Items.Clear();

            var skinEnum = Enum.GetNames(typeof(Enums.SKINID));

            if (skin_txt_skinSearch.Text == string.Empty)
            {
                foreach (var skin in skinEnum)
                {
                    skin_list_skinList.Items.Add(skin);
                }
                skin_list_skinList.Sorted = true;
            }
            else
            {
                string search = skin_txt_skinSearch.Text.ToLower();
                search = search.Replace("-", string.Empty);
                search = search.Replace("_", string.Empty);
                search = search.Replace(" ", string.Empty);

                foreach (var skin in skinEnum)
                {
                    string skinname = skin.ToLower();
                    skinname = skinname.Replace("-", string.Empty);
                    skinname = skinname.Replace("_", string.Empty);
                    skinname = skinname.Replace(" ", string.Empty);

                    if (skinname.Contains(search))
                    {
                        skin_list_skinList.Items.Add(skin);
                    }
                }
            }

        }

        private void configs_btn_saveCheat_Click(object sender, EventArgs e)
        {
            if (configs_txt_saveCheat.Text != String.Empty)
                Settings.userSettings.ConfigName = configs_txt_saveCheat.Text;
            else if (configs_list_cheatCfg.SelectedIndex != -1)
                Settings.userSettings.ConfigName = configs_list_cheatCfg.SelectedItem.ToString();
            else
                return;

            Settings.userSettings.ConfigVersion = ConfigVerson;
            configmaker.save<Settings.UserSettings>(Settings.userSettings,
                Application.StartupPath + "\\" + Settings.userSettings.ConfigName + ".xml");
        }

        private void configs_btn_loadCheat_Click(object sender, EventArgs e)
        {
            if (configs_list_cheatCfg.SelectedIndex >= 0)
            {
                LoadConfig();
            }
        }

        private void configs_btn_saveSkin_Click(object sender, EventArgs e)
        {
            if (configs_txt_saveSkin.Text != String.Empty)
                Settings.userSkinSettings.ConfigName = configs_txt_saveSkin.Text;
            else if (config_list_skinCfg.SelectedIndex != -1)
                Settings.userSkinSettings.ConfigName = config_list_skinCfg.SelectedItem.ToString();
            else
                return;

            Settings.userSettings.ConfigVersion = ConfigVerson;

            configmaker.save<Settings.SkinChanger>(Settings.userSkinSettings,
                Application.StartupPath + "\\" + Settings.userSkinSettings.ConfigName + ".skins" + ".xml");

            updateCheatList();
        }

        private void configs_btn_loadSkin_Click(object sender, EventArgs e)
        {
            if (config_list_skinCfg.SelectedIndex != -1)
            {
                Settings.userSkinSettings = configmaker.load<Settings.SkinChanger>(Application.StartupPath + config_list_skinCfg.SelectedItem.ToString());
            }
        }
        #endregion

        private void menu_Thread_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (gvar.isShuttingDown)
                {
                    break;
                }

                Thread.Sleep(10);

                this.Invoke((MethodInvoker)delegate ()
                {
                    if ((WinAPI.GetAsyncKeyState(0x2D) & 0x1) > 0)
                    {
                        this.Visible = !this.Visible;
                        gvar.isMenu = Visible;
                        this.Left = (gvar.OverlaySize.Width / 2) - (this.Width / 2);
                        this.Top = (gvar.OverlaySize.Height / 2) - (this.Height / 2);
                    }
                });
            }
        }

        private void wnd_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!gvar.isShuttingDown)
            {
                MessageBox.Show("Please exit using the exit button.\nThis ensures all threads are closed\nand that your computer wont be impacted.");
                e.Cancel = true;
            }
            else
            {
                while (gvar.SHUTDOWN != 0)
                    Thread.Sleep(1);

                try
                {
                    Local._bsp.Dispose();
                }
                catch { }

                Local.ThirdPerson = false;
                SetClanTag.Set("");
                Local.SendPackets = true;
                Local.ForceUpdate();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void configs_btn_exit_Click(object sender, EventArgs e)
        {
            gvar.isShuttingDown = true;

            Thread.Sleep(100);

            while (gvar.SHUTDOWN != 0)
            {
                this.Refresh();
                Thread.Sleep(1);
            }

            Local._bsp.Dispose();

            Local.ThirdPerson = false;
            SetClanTag.Set("");
            Local.SendPackets = true;
            Local.ForceUpdate();

            Environment.Exit(Environment.ExitCode);
        }

        
    }
}
