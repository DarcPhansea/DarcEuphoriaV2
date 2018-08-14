using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darc_Euphoria.Euphoric
{
    internal static class Enums
    {
        internal enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        private enum EncodingType
        {
            ASCII,
            Unicode,
            UTF7,
            UTF8
        }
        [Flags]
        internal enum FreeType
        {
            Decommit = 0x4000,
            MEM_COMMIT = 0x1000,
            MEM_RELEASE = 0x8000,
            MEM_RESERVE = 0x2000,
        }

        public enum WEAPONID
        {
            Deagle = 1,
            DuelBerettas = 2,
            FiveSeven = 3,
            Glock18 = 4,
            AK47 = 7,
            AUG = 8,
            AWP = 9,
            FAMAS = 10,
            G3SG1 = 11,
            GalilAR = 13,
            M249 = 14,
            M4A4 = 16,
            Mac10 = 17,
            P90 = 19,
            UMP45 = 24,
            XM1014 = 25,
            PPBizon = 26,
            Mag7 = 27,
            Negev = 28,
            SawedOff = 29,
            Tec9 = 30,
            p2000 = 32,
            MP7 = 33,
            MP9 = 34,
            Nova = 35,
            p250 = 36,
            Scar20 = 38,
            SG556 = 39,
            SSG08 = 40,
            M4A1S = 60,
            USPS = 61,
            CZ75A = 63,
            R8Revolver = 64,
        };

        public enum VirtualKeys
        : ushort
        {
            
            LeftButton = 0x01,
            
            RightButton = 0x02,
            
            Cancel = 0x03,
            
            MiddleButton = 0x04,
            
            ExtraButton1 = 0x05,
            
            ExtraButton2 = 0x06,
            
            Back = 0x08,
            
            Tab = 0x09,
            
            Clear = 0x0C,
            
            Return = 0x0D,
            
            Shift = 0x10,
            
            Control = 0x11,
            
            Menu = 0x12,
            
            Pause = 0x13,
            
            CapsLock = 0x14,
            
            Kana = 0x15,
            
            Hangeul = 0x15,
            
            Hangul = 0x15,
            
            Junja = 0x17,
            
            Final = 0x18,
            
            Hanja = 0x19,
            
            Kanji = 0x19,
            
            Escape = 0x1B,
            
            Convert = 0x1C,
            
            NonConvert = 0x1D,
            
            Accept = 0x1E,
            
            ModeChange = 0x1F,
            
            Space = 0x20,
            
            Prior = 0x21,
            
            Next = 0x22,
            
            End = 0x23,
            
            Home = 0x24,
            
            Left = 0x25,
            
            Up = 0x26,
            
            Right = 0x27,
            
            Down = 0x28,
            
            Select = 0x29,
            
            Print = 0x2A,
            
            Execute = 0x2B,
            
            Snapshot = 0x2C,
            
            Insert = 0x2D,
            
            Delete = 0x2E,
            
            Help = 0x2F,
            
            N0 = 0x30,
            
            N1 = 0x31,
            
            N2 = 0x32,
            
            N3 = 0x33,
            
            N4 = 0x34,
            
            N5 = 0x35,
            
            N6 = 0x36,
            
            N7 = 0x37,
            
            N8 = 0x38,
            
            N9 = 0x39,
            
            A = 0x41,
            
            B = 0x42,
            
            C = 0x43,
            
            D = 0x44,
            
            E = 0x45,
            
            F = 0x46,
            
            G = 0x47,
            
            H = 0x48,
            
            I = 0x49,
            
            J = 0x4A,
            
            K = 0x4B,
            
            L = 0x4C,
            
            M = 0x4D,
            
            N = 0x4E,
            
            O = 0x4F,
            
            P = 0x50,
            
            Q = 0x51,
            
            R = 0x52,
            
            S = 0x53,
            
            T = 0x54,
            
            U = 0x55,
            
            V = 0x56,
            
            W = 0x57,
            
            X = 0x58,
            
            Y = 0x59,
            
            Z = 0x5A,
            
            LeftWindows = 0x5B,
            
            RightWindows = 0x5C,
            
            Application = 0x5D,
            
            Sleep = 0x5F,
            
            Numpad0 = 0x60,
            
            Numpad1 = 0x61,
            
            Numpad2 = 0x62,
            
            Numpad3 = 0x63,
            
            Numpad4 = 0x64,
            
            Numpad5 = 0x65,
            
            Numpad6 = 0x66,
            
            Numpad7 = 0x67,
            
            Numpad8 = 0x68,
            
            Numpad9 = 0x69,
            
            Multiply = 0x6A,
            
            Add = 0x6B,
            
            Separator = 0x6C,
            
            Subtract = 0x6D,
            
            Decimal = 0x6E,
            
            Divide = 0x6F,
            
            F1 = 0x70,
            
            F2 = 0x71,
            
            F3 = 0x72,
            
            F4 = 0x73,
            
            F5 = 0x74,
            
            F6 = 0x75,
            
            F7 = 0x76,
            
            F8 = 0x77,
            
            F9 = 0x78,
            
            F10 = 0x79,
            
            F11 = 0x7A,
            
            F12 = 0x7B,
            
            F13 = 0x7C,
            
            F14 = 0x7D,
            
            F15 = 0x7E,
            
            F16 = 0x7F,
            
            F17 = 0x80,
            
            F18 = 0x81,
            
            F19 = 0x82,
            
            F20 = 0x83,
            
            F21 = 0x84,
            
            F22 = 0x85,
            
            F23 = 0x86,
            
            F24 = 0x87,
            
            NumLock = 0x90,
            
            ScrollLock = 0x91,
            
            NEC_Equal = 0x92,
            
            Fujitsu_Jisho = 0x92,
            
            Fujitsu_Masshou = 0x93,
            
            Fujitsu_Touroku = 0x94,
            
            Fujitsu_Loya = 0x95,
            
            Fujitsu_Roya = 0x96,
            
            LeftShift = 0xA0,
            
            RightShift = 0xA1,
            
            LeftControl = 0xA2,
            
            RightControl = 0xA3,
            
            LeftAlt = 0xA4,
            
            RightAlt = 0xA5,
            
            BrowserBack = 0xA6,
            
            BrowserForward = 0xA7,
            
            BrowserRefresh = 0xA8,
            
            BrowserStop = 0xA9,
            
            BrowserSearch = 0xAA,
            
            BrowserFavorites = 0xAB,
            
            BrowserHome = 0xAC,
            
            VolumeMute = 0xAD,
            
            VolumeDown = 0xAE,
            
            VolumeUp = 0xAF,
            
            MediaNextTrack = 0xB0,
            
            MediaPrevTrack = 0xB1,
            
            MediaStop = 0xB2,
            
            MediaPlayPause = 0xB3,
            
            LaunchMail = 0xB4,
            
            LaunchMediaSelect = 0xB5,
            
            LaunchApplication1 = 0xB6,
            
            LaunchApplication2 = 0xB7,
            
            OEM1 = 0xBA,
            
            OEMPlus = 0xBB,
            
            OEMComma = 0xBC,
            
            OEMMinus = 0xBD,
            
            OEMPeriod = 0xBE,
            
            OEM2 = 0xBF,
            
            OEM3 = 0xC0,
            
            OEM4 = 0xDB,
            
            OEM5 = 0xDC,
            
            OEM6 = 0xDD,
            
            OEM7 = 0xDE,
            
            OEM8 = 0xDF,
            
            OEMAX = 0xE1,
            
            OEM102 = 0xE2,
            
            ICOHelp = 0xE3,
            
            ICO00 = 0xE4,
            
            ProcessKey = 0xE5,
            
            ICOClear = 0xE6,
            
            Packet = 0xE7,
            
            OEMReset = 0xE9,
            
            OEMJump = 0xEA,
            
            OEMPA1 = 0xEB,
            
            OEMPA2 = 0xEC,
            
            OEMPA3 = 0xED,
            
            OEMWSCtrl = 0xEE,
            
            OEMCUSel = 0xEF,
            
            OEMATTN = 0xF0,
            
            OEMFinish = 0xF1,
            
            OEMCopy = 0xF2,
            
            OEMAuto = 0xF3,
            
            OEMENLW = 0xF4,
            
            OEMBackTab = 0xF5,
            
            ATTN = 0xF6,
            
            CRSel = 0xF7,
            
            EXSel = 0xF8,
            
            EREOF = 0xF9,
            
            Play = 0xFA,
            
            Zoom = 0xFB,
            
            Noname = 0xFC,
            
            PA1 = 0xFD,
            
            OEMClear = 0xFE
        }

        public enum SKINID
        {
            MP7_Groundwater = 2,
            CandyApple = 3,
            ForestDDPAT = 5,
            ArcticCamo = 6,
            DesertStorm = 8,
            BengalTiger = 9,
            Copperhead = 10,
            Skulls = 11,
            CrimsonWeb = 12,
            BlueStreak = 13,
            RedLaminate = 14,
            Gunsmoke = 15,
            JungleTiger = 16,
            UranDDPAT = 17,
            Virus = 20,
            GraniteMarleized = 21,
            ContrastSpray = 22,
            ForestLeaves = 25,
            LichenDashed = 26,
            BoneMask = 27,
            AUG_AnodizedNavy = 28,
            SnakeCamo = 30,
            Silver = 32,
            MP9_HotRod = 33,
            MetallicDDPAT = 34,
            Ossified = 36,
            Blaze = 37,
            GLOCK_Fade = 38,
            Bulldozer = 39,
            Night = 40,
            Copper = 41,
            BlueSteel = 42,
            Stained = 43,
            CaseHardened = 44,
            Contractor = 46,
            Colony = 47,
            DragonTattoo = 48,
            LightningStrike = 51,
            Slaughter = 59,
            DarkWater = 60,
            Hypnotic = 61,
            Bloomstick = 62,
            ColdBlooded = 67,
            CaronFier = 70,
            Scorpion = 71,
            SafariMesh = 72,
            Wings = 73,
            PolarCamo = 74,
            BlizzardMarleized = 75,
            WinterForest = 76,
            BorealForest = 77,
            ForestNight = 78,
            OrangeDDPAT = 83,
            PinkDDPAT = 84,
            Mudder = 90,
            Cyanospatter = 92,
            Caramel = 93,
            Grassland = 95,
            BlueSpruce = 96,
            Ultraviolet = 98,
            GLOCK_SandDune = 99,
            Storm = 100,
            TEC9_Tornado = 101,
            Whiteout = 102,
            GrasslandLeaves = 104,
            PolarMesh = 107,
            Condemned = 110,
            GlacierMesh = 111,
            SandMesh = 116,
            SageSpray = 119,
            JungleSpray = 122,
            SandSpray = 124,
            UranPerforated = 135,
            WavesPerforated = 136,
            OrangePeel = 141,
            UranMasked = 143,
            JungleDashed = 147,
            SandDashed = 148,
            UranDashed = 149,
            XM_Jungle = 151,
            Demolition = 153,
            Afterimage = 154,
            BulletRain = 155,
            DeathyKitty = 156,
            SCAR20_Palm = 157,
            Walnut = 158,
            Brass = 159,
            Splash = 162,
            ModernHunter = 164,
            SplashJam = 165,
            BlazeOrange = 166,
            M4A4_RadiationHazard = 167,
            TEC9_NuclearThreat = 168,
            P90_FalloutWarning = 169,
            Predator = 170,
            IrradiatedAlert = 171,
            BlackLaminate = 172,
            BOOM = 174,
            Scorched = 175,
            FadedZera = 176,
            Memento = 177,
            Doomkitty = 178,
            TEC9_NuclearThreat_2 = 179,
            FireSerpent = 180,
            AWP_Corticera = 181,
            EmeraldDragon = 182,
            Overgrowth = 183,
            P2000_Corticera = 184,
            GoldenKoi = 185,
            WaveSpray = 186,
            Zirka = 187,
            Graven = 188,
            M4A1S_BrightWater = 189,
            BlackLima = 190,
            Tempest = 191,
            Shattered = 192,
            BonePile = 193,
            Spitfire = 194,
            Demeter = 195,
            SCAR20_Emerald = 196,
            AUG_AnodizedNavy_2 = 197,
            Hazard = 198,
            DrySeason = 199,
            MayanDreams = 200,
            NEGEV_Palm = 201,
            JungleDDPAT = 202,
            RustCoat = 203,
            Mosaico = 204,
            XM_Jungle_2 = 205,
            TEC9_Tornado_2 = 206,
            Facets = 207,
            GLOCK_SandDune_2 = 208,
            MP7_Groundwater_2 = 209,
            AnodizedGunmetal = 210,
            P2000_OceanFoam = 211,
            AWP_Graphite = 212,
            P2000_OceanFoam_2 = 213,
            NOVA_Graphite = 214,
            X_Ray = 215,
            BlueTitanium = 216,
            BloodTiger = 217,
            Hexane = 218,
            Hive = 219,
            Hemogloin = 220,
            Serum = 221,
            BloodintheWater = 222,
            Nightshade = 223,
            WaterSigil = 224,
            GhostCamo = 225,
            BlueLaminate = 226,
            ElectricHive = 227,
            BlindSpot = 228,
            AzureZera = 229,
            SteelDisruption = 230,
            CoaltDisruption = 231,
            SCAR20_CrimsonWeb = 232,
            TropicalStorm = 233,
            AshWood = 234,
            VariCamo = 235,
            NightOps = 236,
            UranRule = 237,
            VariCamoBlue = 238,
            CaliCamo = 240,
            HuntingBlind = 241,
            ArmyMesh = 242,
            GatorMesh = 243,
            Teardown = 244,
            ArmyRecon = 245,
            P2000_AmberFade = 246,
            SG553_DamascusSteel = 247,
            RedQuartz = 248,
            CoaltQuartz = 249,
            FullStop = 250,
            PitViper = 251,
            SilverQuartz = 252,
            AcidFade = 253,
            CZ75A_Nitro = 254,
            M4A4_Asiimov = 255,
            TheKraken = 256,
            M4A1S_Guardian = 257,
            Mehndi = 258,
            AWP_Redline = 259,
            FAMAS_Pulse = 260,
            Marina = 261,
            RoseIron = 262,
            RisingSkull = 263,
            GALILAR_Sandstorm = 264,
            FIVESEVEN_Kami = 265,
            Magma = 266,
            CoaltHalftone = 267,
            TreadPlate = 268,
            TheFuschiaIsNow = 269,
            Victoria = 270,
            Undertow = 271,
            TitaniumBit = 272,
            Heirloom = 273,
            CopperGalaxy = 274,
            RedFragCam = 275,
            Panther = 276,
            Stainless = 277,
            BlueFissure = 278,
            AWP_Asiimov = 279,
            Chameleon = 280,
            Corporal = 281,
            AK47_Redline = 282,
            Trigon = 283,
            MAC10_Heat = 284,
            Terrain = 285,
            NOVA_Antique = 286,
            SG553_Pulse = 287,
            Sergeant = 288,
            TEC9_Sandstorm = 289,
            USPS_Guardian = 290,
            MAG7_HeavenGuard = 291,
            DeathRattle = 293,
            GreenApple = 294,
            Franklin = 295,
            Meteorite = 296,
            Tuxedo = 297,
            ArmySheen = 298,
            CagedSteel = 299,
            EmeraldPinstripe = 300,
            AtomicAlloy = 301,
            Vulcan = 302,
            Isaac = 303,
            Slashed = 304,
            AUG_Torque = 305,
            PPBIZON_Antique = 306,
            Retriution = 307,
            GalilAR_Kami = 308,
            Howl = 309,
            Curse = 310,
            DesertWarfare = 311,
            SCAR20_Cyrex = 312,
            Orion = 313,
            XM1014_HeavenGuard = 314,
            PoisonDart = 315,
            Jaguar = 316,
            Bratatat = 317,
            RoadRash = 318,
            Detour = 319,
            RedPython = 320,
            MasterPiece = 321,
            CZ75A_Nitro_2 = 322,
            RustCoat_2 = 323,
            Chalice = 325,
            Knight = 326,
            Chainmail = 327,
            HandCannon = 328,
            DarkAge = 329,
            Briar = 330,
            RoyalBlue = 332,
            Indigo = 333,
            Twist = 334,
            Module = 335,
            M4A4_Desert_Strike = 336,
            Tatter = 337,
            Pulse = 338,
            Caiman = 339,
            JetSet = 340,
            AK47_FirstClass = 341,
            Leather = 342,
            Commuter = 343,
            DragonLore = 344,
            SAWEDOFF_FirstClass = 345,
            CoachClass = 346,
            Pilot = 347,
            RedLeather = 348,
            Osiris = 349,
            Tigris = 350,
            Conspiracy = 351,
            FowlPlay = 352,
            WaterElemental = 353,
            UranHazard = 354,
            NEGEV_Desert_Strike = 355,
            Koi = 356,
            Ivory = 357,
            Supernova = 358,
            P90_Asiimov = 359,
            M4A1S_Cyrex = 360,
            Ayss = 361,
            Layrinth = 362,
            Traveler = 363,
            BusinessClass = 364,
            OlivePlaid = 365,
            GreenPlaid = 366,
            Reactor = 367,
            SettingSun = 368,
            NuclearWaste = 369,
            BoneMachine = 370,
            Styx = 371,
            NuclearGarden = 372,
            Contamination = 373,
            Toxic = 374,
            AUG_RadiationHazard = 375,
            ChemicalGreen = 376,
            HotShot = 377,
            SG553_FalloutWarning = 378,
            Cererus = 379,
            AK47_WastelandRebel = 380,
            Grinder = 381,
            Murky = 382,
            Basilisk = 383,
            Griffin = 384,
            Firestarter = 385,
            Dart = 386,
            UrbanHazard = 387,
            P250_Cartel = 388,
            FireElemental = 389,
            Highwayman = 390,
            Cardiac = 391,
            Delusion = 392,
            Tranquility = 393,
            AK47_Cartel = 394,
            AWP_Manowar = 395,
            UranShock = 396,
            Naga = 397,
            Chatterox = 398,
            Catacoms = 399,
            DragonKing = 400,
            SystemLock = 401,
            Malachite = 402,
            DeadlyPoison = 403,
            Muertos = 404,
            Serenity = 405,
            Grotto = 406,
            Quicksilver = 407,
            TigerTooth = 409,
            DamascusSteel = 410,
            M9BAYONET_DamascusSteel = 411,
            MarleFade = 413,
            RustCoat_3 = 414,
            Doppler_RED = 415,
            Doppler_BLUE = 416,
            Doppler_BLACKPEARL = 417,
            Doppler_PHASE1 = 418,
            Doppler_PHASE2 = 419,
            Doppler_PHASE3 = 420,
            Doppler_PHASE4 = 421,
            AK47_EliteBuild = 422,
            ArmorCore = 423,
            WormGod = 424,
            BronzeDeco = 425,
            P250_Valence = 426,
            MonkeyBusiness = 427,
            Eco = 428,
            Djinn = 429,
            M4A1S_HyperBeast = 430,
            MAG7_Heat = 431,
            NEGEV_Manowar = 432,
            NeonRider = 433,
            Origami = 434,
            PolePosition = 435,
            GrandPrix = 436,
            TwilightGalaxy = 437,
            Chronos = 438,
            Hades = 439,
            IcarusFell = 440,
            MinotaursLabyrinth = 441,
            Asterion = 442,
            Pathfinder = 443,
            Daedalus = 444,
            M4A1S_HotRod = 445,
            Medusa = 446,
            Duelist = 447,
            MP9_PandorasBox = 448,
            Poseidon = 449,
            MooninLira = 450,
            SuninLeo = 451,
            ShippingForecast = 452,
            CZ7A_Emerald = 453,
            ParaGreen = 454,
            AkihabaraAccept = 455,
            Hydroponic = 456,
            BamooPrint = 457,
            BamooShadow = 458,
            BamooForest = 459,
            AquaTerrace = 460,
            CounterTerrace = 462,
            Terrace = 463,
            NeonKimono = 464,
            OrangeKimono = 465,
            CrimsonKimono = 466,
            MintKimono = 467,
            MidnightStorm = 468,
            SunsetStorm = 469,
            SunsetStorm_2 = 470,
            Dayreak = 471,
            ImpactDrill = 472,
            Seaird = 473,
            AquamarineRevenge = 474,
            AWP_HyperBeast = 475,
            YellowJacket = 476,
            NeuralNet = 477,
            RocketPop = 478,
            BunsenBurner = 479,
            EvilDaimyo = 480,
            Nemesis = 481,
            RuyPoisonDart = 482,
            Loudmouth = 483,
            Ranger = 484,
            Handgun = 485,
            P90_EliteBuild = 486,
            SG553_Cyrex = 487,
            Riot = 488,
            USPS_Torque = 489,
            FrontsideMisty = 490,
            DualingDragons = 491,
            SurvivorZ = 492,
            Flux = 493,
            StoneCold = 494,
            Wraiths = 495,
            NeulaCrusader = 496,
            GoldenCoil = 497,
            Rangeen = 498,
            CoaltCore = 499,
            SpecialDelivery = 500,
            Wingshot = 501,
            GreenMarine = 502,
            BigIron = 503,
            KillConfirmed = 504,
            XM1014_Scumria = 505,
            PointDisarray = 506,
            Ricochet = 507,
            FuelRod = 508,
            Corinthian = 509,
            Retroution = 510,
            TheExecutioner = 511,
            RoyalPaladin = 512,
            PowerLoader = 514,
            Imperial = 515,
            Shapewood = 516,
            Yorick = 517,
            Outreak = 518,
            TigerMoth = 519,
            Avalanche = 520,
            TecluBurner = 521,
            R8_Fade = 522,
            R8_AmberFade = 523,
            AK47_FuelInjector = 524,
            EliteBuild = 525,
            PhoticZone = 526,
            KumichoDragon = 527,
            Cartel = 528,
            FAMAS_Valence = 529,
            Triumvirate = 530,
            RoyalLegion = 532,
            TheBattlestar = 533,
            LapisGator = 534,
            Praetorian = 535,
            Impire = 536,
            NOVA_HyperBeast = 537,
            Necropos = 538,
            Jamiya = 539,
            LeadConduit = 540,
            Doppler_1 = 617,
            Doppler_2 = 618,
            Doppler_3 = 619,
            Ultraviolet_1 = 620,
            Ultraviolet_2 = 621,
            FleetFlock = 541,
            JudgementofAnuis = 542,
            RedAstor = 543,
            Ventilators = 544,
            OrangeCrash = 545,
            Firefight = 546,
            Spectre = 547,
            Chanticos_Fire = 548,
            Bioleak = 549,
            Oceanic = 550,
            P250_Asiimov = 551,
            Fuar = 552,
            Atlas = 553,
            GhostCrusader = 554,
            Re_Entry = 555,
            PrimalSaer = 556,
            BlackTie = 557,
            Lore_BAYONET = 558,
            Lore_FLIP = 559,
            Lore_GUT = 560,
            Lore_KARAMBIT = 561,
            Lore_M9BAYONET = 562,
            BlackLaminate_BAYONET = 563,
            BlackLaminate_FLIP = 564,
            BlackLaminate_GUT = 565,
            BlackLaminate_KARAMBIT = 566,
            BlackLaminate_M9BAYONET = 567,
            GammaDoppler_EMERALD = 568,
            GammaDoppler_PHASE1 = 569,
            GammaDoppler_PHASE2 = 570,
            GammaDoppler_PHASE3 = 571,
            GammaDoppler_PHASE4 = 572,
            Autotronic_BAYONET = 573,
            Autotronic_FLIP = 574,
            Autotronic_GUT = 575,
            Autotronic_KARAMBIT = 576,
            Autotronic_M9BAYONET = 577,
            BrightWater = 578,
            BrightWater_M9BAYONET = 579,
            Freehand = 580,
            Freehand_M9BAYONET = 581,
            Freehand_KARAMBIT = 582,
            Aristocrat = 583,
            Phoos = 584,
            ViolentDaimyo = 585,
            GLOCK_WastelandRebel = 586,
            M4A1S_MechaIndustries = 587,
            DesolateSpace = 588,
            Carnivore = 589,
            Exo = 590,
            ImperialDragon = 591,
            IronClad = 592,
            Chopper = 593,
            Harvester = 594,
            Reoot = 595,
            Limelight = 596,
            SCAR20_Bloodsport = 597,
            Aerial = 598,
            IceCap = 599,
            NeonRevolution = 600,
            SydMead = 601,
            Imprint = 602,
            Directive = 603,
            RollCage = 604,
            FIVESEVEN_Scumria = 605,
            Ventilator = 606,
            Weasel = 607,
            Petroglyph = 608,
            Airlock = 609,
            Dazzle = 610,
            Grim = 611,
            Powercore = 612,
            Triarch = 613,
            TEC9_FuelInjector = 614,
            Briefing = 615,
            Slipstream = 616,
            Polymer = 622,
            Ironwork = 623,
            Dragonfire = 624,
            RoyalConsorts = 625,
            FAMEAS_MechaIndustries = 626,
            Cirrus = 627,
            Stinger = 628,
            BlackSand = 629,
            SandScale = 630,
            Flashack = 631,
            BuzzKill = 632,
            Sonar = 633,
            Gila = 634,
            Turf = 635,
            ShallowGrave = 636,
            USPS_Cyrex = 637,
            WastelandPrincess = 638,
            AK47_Bloodsport = 639,
            FeverDream = 640,
            JungleSlipstream = 641,
            SCAR20_Blueprint = 642,
            Xiangliu = 643,
            Decimator = 644,
            OxideBlaze = 645,
            Capillary = 646,
            CrimsonTsunami = 647,
            EmeraldPoisonDart = 648,
            Akoen = 649,
            Ripple = 650,
            LastDive = 651,
            Scaffold = 652,
            USPS_Neo_Noir = 653,
            Seasons = 654,
            Zander = 655,
            OritMk01 = 656,
            USPS_Blueprint = 657,
            CoraStrike = 658,
            Macare = 659,
            FIVESEVEN_HyperBeast = 660,
            SugarRush = 661,
            OniTaiji = 662,
            Briefing_2 = 663,
            Hellfire = 664,
            Aloha = 665,
            HardWater = 666,
            Woodsman = 667,
            RedRock = 668,
            DeathGrip = 669,
            sHead = 670,
            CutOut = 671,
            MetalFlowers = 672,
            Morris = 673,
            Triqua = 674,
            TheEmpress = 675,
            HighRoller = 676,
            Hunter = 677,
            SeeYaLater = 678,
            Goo = 679,
            OffWorld = 680,
            LeadedGlass = 681,
            Oceanic_2 = 682,
            LlamaCannon = 683,
            CrackedOpal = 684,
            JungleSlipstream_2 = 685,
            Phantom = 686,
            Tacticat = 687,
            Exposure = 688,
            Ziggy = 689,
            Stymphalian = 690,
            Mortis = 691,
            NightRiot = 692,
            FlameTest = 693,
            Moonrise = 694,
            M4A1S_Neo_Noir = 695,
            MP7_Bloodsport = 696,
            BlackSand_2 = 697,
            Lionfish = 698,
            WildSix = 699,
            UranHazard_2 = 700,
            Grip = 701,
            Aloha_2 = 702,
            SWAG_7 = 703,
            ArcticWolf = 704,
            Cortex = 705,
            OxideBlaze_2 = 706,
            AmberSlipstream = 708,
            Shred = 710,
            Warhawk = 713,
            MP9_Capillary = 715,
            Traction = 717,
            Survivalist = 721,
            Snek_9 = 722,
            CZ75A_Eco = 709,
            High_Seas = 712,
            Toy_Soilder = 716,
            Paw = 718,
            MP7_Powercore = 719,
            Nightmare = 714,
            Devourer = 720,
            Eye_of_Athena = 723,
            Neon_Rider = 707,
            Code_Red = 711,
        };
    }
}
