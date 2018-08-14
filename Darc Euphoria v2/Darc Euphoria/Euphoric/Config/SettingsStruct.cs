using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Darc_Euphoria.Euphoric.Config
{
    public static class configmaker
    {
        private static string EncryptionKey = "SDAFGDJ324J89DD";
        private static string Decfilename = Path.GetTempFileName().Replace(".tmp", ".xml");

        private static void Decrypt(string inputFilePath, string outputfilePath)
        {
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                            fsOutput.Close();
                        }
                    }
                    
                }
            }
        }

        private static void Encrypt(string inputFilePath, string outputfilePath)
        {
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
            File.Delete(inputFilePath);
        }

        public static void save<t>(object inst, string name)
        {
            var serializer = new XmlSerializer(typeof(t));
            
            using (var writer = XmlWriter.Create(name, new XmlWriterSettings() { Indent = true }))
                serializer.Serialize(writer, inst);

            Encrypt(name, name.Replace(".xml", ".dec"));
        }

        public static t load<t>(string name)
        {
            var serializer = new XmlSerializer(typeof(t));

            Decrypt(name.Replace(".xml", ".dec"), Decfilename);

            t buffer;
            using (var reader = XmlReader.Create(Decfilename))
                buffer = (t)serializer.Deserialize(reader);

            File.Delete(Decfilename);

            return buffer;
        }
    }

    public static class Settings
    {
        public static UserSettings userSettings;
        public static SkinChanger userSkinSettings;

        public enum AimPriority
        {
            Fov = 0,
            Distance = 1,
            Health = 2,
        };

        public enum TriggerMode
        {
            Single = 0,
            Burst = 1,
            Auto = 2,
        };

        public enum AimTarget
        {
            Head = 0,
            Neck = 1,
            UpperChest = 2,
            MiddleChest = 3,
            LowerChest = 4,
        };

        public enum BoxDisplay
        {
            Off = 0,
            _2D = 1,
            Edges = 2,
            _3D = 3,
        };

        public enum HealthDisplay
        {
            Off = 0,
            Left = 1,
            Right = 2,
            Top = 3,
            Bottom = 4,
        };
        
        public enum ClanChangerTheme
        {
            Off = 0,
            Static = 1,
            SkeetTheme = 2,
            SignatureTheme = 3,
            Hidden = 4,
            Custom = 5,
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1), 
            Serializable, Obfuscation(Exclude = true, ApplyToMembers = true)]
        public struct UserSettings
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct Aimbot
            {
                [StructLayout(LayoutKind.Sequential)]
                public struct ASettings
                {
                    public bool Enabled;
                    public bool SpottedCheck;
                    public bool VisiblitiyCheck;
                    public bool TargetTeam;
                    public bool AutoShoot;
                    public bool AutoPistol;
                    public double RcsYaw;
                    public double RcsPitch;
                    public double SmoothYaw;
                    public double SmoothPitch;
                    public AimPriority AimbotPriority;
                    public AimTarget AimbotTarget;
                    public double Randomized;
                    public int Delay;
                    public int Fov;
                };

                public ASettings Pistol;
                public ASettings Rifle;
                public ASettings Smg;
                public ASettings Sniper;
                public ASettings Shotgun;
                public ASettings Lmg;
                public int Key;
            };

            [StructLayout(LayoutKind.Sequential)]
            public struct Triggerbot
            {
                [StructLayout(LayoutKind.Sequential)]
                public struct TSettings
                {
                    public bool Enabled;
                    public TriggerMode TriggerbotMode;
                    public bool TargetTeam;
                    public int BurstAmount;
                    public int Delay;
                };

                public TSettings Pistol;
                public TSettings Rifle;
                public TSettings Smg;
                public TSettings Sniper;
                public TSettings Shotgun;
                public TSettings Lmg;
                public bool Knifebot;
            };

            [StructLayout(LayoutKind.Sequential)]
            public struct Visuals
            {
                public bool Enabled;
                public bool DisplayTeam;
                public BoxDisplay BoxEsp;
                public bool BoxOutline;
                public bool Glow;
                public bool FullBloom;
                public HealthDisplay HealthPostion;
                public bool HealthNumber;
                public bool Name;
                public bool ActiveWeapon;
                public bool Rank;
                public bool Snaplines;
                public bool PseudoChams;
                public bool ItemEsp;
                public bool ChickenEsp;
                public bool PlantedC4Esp;
                public bool DefuserEsp;
                public bool GrenadesEsp;
                public bool DrawAimbotFov;
                public bool RecoilCrosshair;
                public bool SniperCrosshair;
                public bool NoHands;
                public bool NoScope;
            };

            [StructLayout(LayoutKind.Sequential)]
            public struct Misc
            {
                public bool BunnyHop;
                public bool _3rdPerson;
                public bool NoPostProcessing;
                public bool Spectators;
                public bool C4Countdown;
                public int FlashAlpha;
                public int Fov;
                public bool FakeLag;
                public int FakeLagAmount;
                public bool Watermark;
                public bool LocalTime;
                public ClanChangerTheme ClanChangerTheme;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
                public string NameChanger;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
                public string ClanChanger;
                public bool RankRevealer;
                public int _3rdPersonKey;
                public bool ChatSpammer;
                public bool inGameRadar;
            };

            [StructLayout(LayoutKind.Sequential)]
            public struct VisColors
            {
                public Color Team_Box_Visible;
                public Color Team_Box_Hidden;
                public Color Team_Chams;
                public Color Team_Glow_Visible;
                public Color Team_Glow_Hidden;
                public Color Team_Snaplines_Visible;
                public Color Team_Snaplines_Hidden;
                public Color Team_Text;
                public Color Enemy_Box_Visible;
                public Color Enemy_Box_Hidden;
                public Color Enemy_Chams;
                public Color Enemy_Glow_Visible;
                public Color Enemy_Glow_Hidden;
                public Color Enemy_Snaplines_Visible;
                public Color Enemy_Snaplines_Hidden;
                public Color Enemy_Text;
                public Color World_Text;
            };


            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string ConfigName;
            public double ConfigVersion;
            public Aimbot AimbotSettings;
            public Triggerbot TriggerbotSettings;
            public Visuals VisualSettings;
            public Misc MiscSettings;
            public VisColors VisualColors;
            public int ForceUpdateKey;
        };

        
        [StructLayout(LayoutKind.Sequential)]
        public struct SkinChanger
        {
            public _SkinSettings Cz75a;
            public _SkinSettings Deagle;
            public _SkinSettings DuelBerettas;
            public _SkinSettings FiveSeven;
            public _SkinSettings Glock18;
            public _SkinSettings P2000;
            public _SkinSettings P250;
            public _SkinSettings R8Revolver;
            public _SkinSettings Tec9;
            public _SkinSettings USPS;
            public _SkinSettings AK47;
            public _SkinSettings Aug;
            public _SkinSettings Awp;
            public _SkinSettings Famas;
            public _SkinSettings G3sg1;
            public _SkinSettings Galilar;
            public _SkinSettings M4a1s;
            public _SkinSettings M4a4;
            public _SkinSettings Scar20;
            public _SkinSettings Sg553;
            public _SkinSettings Ssg08;
            public _SkinSettings Mac10;
            public _SkinSettings Mp7;
            public _SkinSettings Mp9;
            public _SkinSettings PPBizon;
            public _SkinSettings P90;
            public _SkinSettings Ump45;
            public _SkinSettings Mag7;
            public _SkinSettings Nova;
            public _SkinSettings SawedOff;
            public _SkinSettings Xm1014;
            public _SkinSettings M249;
            public _SkinSettings Negev;
            public bool Enable;
            public string ConfigName;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct _SkinSettings
        {
            public int SkinID;
            public int Seed;
            public float Wear;
            public int StatTrak;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
            public string Name;
        };   
    }
}

