using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Euphoric
{
    internal static class Sig
    {
        private static byte[] _dump;

        private static void Dump(IntPtr module, Int32 moduleSize)
        {
            _dump = Memory.ReadBytes((Int32)module, moduleSize);
        }

        private static bool CheckSig(int index, Signature signature)
        {
            for (int i = 0; i < signature.ByteArray.Length; i++)
            {
                if (signature.Mask[i] == '?')
                    continue;

                if (signature.ByteArray[i] != _dump[index + i])
                    return false;
            }
            return true;
        }

        public static Int32 COffset(string sig, int offset = 0, int extra = 0)
        {
            return GetOffset(sig, offset, extra, (IntPtr)Memory.client, Memory.client_size);
        }

        public static Int32 EOffset(string sig, int offset = 0, int extra = 0)
        {
            return GetOffset(sig, offset, extra, (IntPtr)Memory.engine, Memory.engine_size);
        }

        public static Int32 GetOffset(string sig, int offset, int extra, IntPtr module, Int32 moduleSize)
        {
            Dump(module, moduleSize);

            Structs.Signature signature = new Structs.Signature(sig, offset);

            for (int i = 0; i < moduleSize; i++)
            {
                if (signature.Address == IntPtr.Zero && CheckSig(i, signature))
                {
                    var _offset = signature.Offset;
                    signature = new Structs.Signature(module + i + _offset);

                    if (signature.Address != IntPtr.Zero)
                        return BitConverter.ToInt32(Memory.ReadBytes((Int32)signature.Address, 4), 0) + extra - module.ToInt32();

                }
            }
            MessageBox.Show(String.Format("The Signature {0}\nCouldn't Be Found.", sig));
            return Int32.MinValue;
        }

        public static Signature GetSignature(Signature sig, IntPtr module, Int32 moduleSize)
        {
            byte[] dumped = Memory.ReadBytes((Int32)module, moduleSize);

            for (int i = 0; i < moduleSize; i++)
            {
                if (sig.Address == IntPtr.Zero && CheckSig(i, sig))
                {
                    var offset = sig.Offset;
                    sig = new Signature(module + i + offset);

                    if (sig.Address != IntPtr.Zero)
                    {
                        return sig;
                    }
                }
            }
            return sig;
        }

        public static int FindPattern(byte[] pattern, string mask, int moduleBase, int moduleSize)
        {
            byte[] moduleBytes = new byte[moduleSize];
            uint numBytes = 0;

            if (WinAPI.ReadProcessMemory(Memory.pHandle, (IntPtr)moduleBase, moduleBytes, (uint)moduleSize, out numBytes))
            {
                for (int i = 0; i < moduleSize; i++)
                {
                    bool found = true;

                    for (int l = 0; l < mask.Length; l++)
                    {
                        found = mask[l] == '?' || moduleBytes[l + i] == pattern[l];

                        if (!found)
                            break;
                    }

                    if (found)
                        return i;
                }
            }

            return 0;
        }

        public static bool Init()
        {
            if (!Memory.OpenProcess("csgo"))
                return false;

            foreach (ProcessModule module in Memory.process.Modules)
                if (module.ModuleName == "client.dll" || module.ModuleName == "client_panorama.dll")
                {
                    if (module.ModuleName == "client_panorama.dll")
                        gvar.isPanorama = true;

                    Memory.client = (Int32)module.BaseAddress;
                    Memory.client_size = module.ModuleMemorySize;
                }
                else if (module.ModuleName == "engine.dll")
                {
                    Memory.engine = (Int32)module.BaseAddress;
                    Memory.engine_size = module.ModuleMemorySize;
                }

            if ((IntPtr)Memory.client == IntPtr.Zero || (IntPtr)Memory.engine == IntPtr.Zero)
                return false;

            if (Memory.client_size == 0 || Memory.engine_size == 0)
                return false;

            return true;
        }

        public static void InitOffsets()
        {
            Offsets.dwEntityList = COffset("BB ? ? ? ? 83 FF 01 0F 8C ? ? ? ? 3B F8", 0x1);
            Offsets.dwForceAttack = COffset("89 0D ? ? ? ? 8B 0D ? ? ? ? 8B F2 8B C1 83 CE 04", 0x2);
            Offsets.dwForceAttack2 = COffset("23 C8 89 ? ? ? ? ? 8B ? ? ? ? ? 8B F2 8B C1 81 CE 00 20", 0x4);
            //Offsets.dwForceAttack2 = COffset("89 0D ? ? ? ? 8B 0D ? ? ? ? 8B F2 8B C1 83 CE 04", 0x2, -84);
            Offsets.dwGameRulesProxy = COffset("A1 ? ? ? ? 85 C0 0F 84 ? ? ? ? 80 B8 ? ? ? ? ? 0F 84 ? ? ? ? 0F 10 05", 0x1);
            Offsets.dwGlowObjectManager = COffset("A1 ? ? ? ? A8 01 75 4B", 0x1, 4);
            Offsets.dwInput = COffset("B9 ? ? ? ? F3 0F 11 04 24 FF 50 10", 0x1);
            
            Offsets.dwLocalPlayer = COffset("A3 ? ? ? ? C7 05 ? ? ? ? ? ? ? ? E8 ? ? ? ? 59 C3 6A ?", 0x1, 16);
            Offsets.dwPlayerResource = COffset("8B 3D ? ? ? ? 85 FF 0F 84 ? ? ? ? 81 C7", 0x2);
            Offsets.dwRadarBase = COffset("A1 ? ? ? ? 8B 0C B0 8B 01 FF 50 ? 46 3B 35 ? ? ? ? 7C EA 8B 0D", 0x1);
            Offsets.dwViewMatrix = COffset("0F 10 05 ? ? ? ? 8D 85 ? ? ? ? B9", 0x3, 176);
            Offsets.dwWeaponTable = COffset("B9 ? ? ? ? 6A 00 FF 50 08 C3", 0x1);
            Offsets.dwWeaponTableIndex = COffset("39 86 ? ? ? ? 74 06 89 86 ? ? ? ? 8B 86", 0x2);
            Offsets.s_bOverridePostProcessingDisable = COffset("80 3D ? ? ? ? ? 53 56 57 0F 85", 0x2);
            Offsets.dwForceJump = COffset("8B 0D ? ? ? ? 8B D6 8B C1 83 CA 02", 0x2);
            //Offsets.dw_RevealRankFn = COffset("55 8B EC 8B 0D ? ? ? ? 68");
            //Offsets.dw_RevealRankFn = COffset("55 8B EC 8B 0D ? ? ? ? 85 C9 75 ? A1 ? ? ? ? 68 ? ? ? ? 8B 08 8B 01 FF 50 ? 85 C0 74 ? 8B C8 E8 ? ? ? ? 8B C8 EB ? 33 C9 89 0D ? ? ? ? 8B 45 ? FF 70 ? E8 ? ? ? ? B0 ? 5D");
            Offsets.dw_RevealRankFn = COffset("C7 00 ? ? ? ? E8 ? ? ? ? 83 EC 08 8D 4E 74", 2);

            Offsets.LastOutGoingCommand = Memory.engine + EOffset("C7 80 ? ? ? ? ? ? ? ? A1 ? ? ? ? F2 0F 10 05 ? ? ? ? F2 0F 11 80 ? ? ? ? FF 15", 0x2);
            Offsets.dwClientState = EOffset("A1 ? ? ? ? 33 D2 6A 00 6A 00 33 C9 89 B0", 0x1);
            Offsets.dwEntityListLength = EOffset("89 01 89 0D ? ? ? ? 66", -0x4);

            Offsets.dwClientState_GetLocalPlayer = Convert.ToInt32(EOffset("8B 80 ? ? ? ? 40 C3", 0x2).ToString("X").Substring(4), 16);
            Offsets.dwClientState_IsHLTV = Convert.ToInt32(EOffset("80 BF ? ? ? ? ? 0F 84 ? ? ? ? 32 DB", 0x2).ToString("X").Substring(4), 16);
            Offsets.dwClientState_Map = Convert.ToInt32(EOffset("05 ? ? ? ? C3 CC CC CC CC CC CC CC A1", 0x1).ToString("X").Substring(4), 16);
            Offsets.dwClientState_MapDirectory = Convert.ToInt32(EOffset("05 ? ? ? ? C3 CC CC CC CC CC CC CC 80 3D", 0x1).ToString("X").Substring(4), 16);
            Offsets.dwClientState_MaxPlayer = Convert.ToInt32(EOffset("A1 ? ? ? ? 8B 80 ? ? ? ? C3 CC CC CC CC 55 8B EC 8A 45 08", 0x7).ToString("X").Substring(4), 16);
            Offsets.dwClientState_PlayerInfo = Convert.ToInt32(EOffset("8B 89 ? ? ? ? 85 C9 0F 84 ? ? ? ? 8B 01", 0x2).ToString("X").Substring(4), 16);
            Offsets.dwClientState_State = Convert.ToInt32(EOffset("83 B8 ? ? ? ? ? 0F 94 C0 C3", 0x2).ToString("X").Substring(4), 16);
            Offsets.dwClientState_ViewAngles = Convert.ToInt32(EOffset("F3 0F 11 80 ? ? ? ? D9 46 04 D9 05", 0x4).ToString("X").Substring(4), 16);

            Offsets.dwGameDir = EOffset("68 ? ? ? ? 8D 85 ? ? ? ? 50 68 ? ? ? ? 68", 0x1);
            Offsets.dwGlobalVars = EOffset("68 ? ? ? ? 68 ? ? ? ? FF 50 08 85 C0", 0x1);
            Offsets.dwSetClanTag = FindPattern(new byte[] { 0x53, 0x56, 0x57, 0x8B, 0xDA, 0x8B, 0xF9, 0xFF, 0x15 }, "xxxxxxxxx", Memory.engine, Memory.engine_size);
            Offsets.dw_SetConVar = FindPattern(new byte[] { 0x8D, 0x4C, 0x24, 0x1C, 0xE8, 0x00, 0x00, 0x00, 0x00, 0x56 }, "xxxxx????x", Memory.engine, Memory.engine_size);
            //Offsets.dw_clientCmd = FindPattern(new byte[] { 0x55, 0x8B, 0xEC, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x33, 0xC9, 0x8B, 0x55, 0x08 }, "xxxx????xxxxx", Memory.engine, Memory.engine_size);
            Offsets.dw_clientCmd = FindPattern(new byte[] { 0x55, 0x8B, 0xEC, 0x8B, 0x0D, 0x00, 0x00, 0x00, 0x00, 0x81, 0xF9, 0x00, 0x00, 0x00, 0x00, 0x75, 0x0C, 0xA1, 0x00, 0x00, 0x00, 0x00, 0x35, 0x00, 0x00, 0x00, 0x00, 0xEB, 0x05, 0x8B, 0x01, 0xFF, 0x50, 0x34, 0x50, 0xA1 }, "xxxxx????xx????xxx????x????xxxxxxxxx", Memory.engine, Memory.engine_size);

            Offsets.dwSendPackets = (int)GetSignature(new Signature("B3 01 8B 01 8B 40 10 FF D0 84 C0 74 0F 80 BF ? ? ? ? ? 0F 84", 0x1), (IntPtr)Memory.engine, Memory.engine_size).Address - Memory.engine;
            Offsets.noarms = Memory.Read<int>(Offsets.dwLocalPlayer + 0x254);
            //
            //55 8B EC 8B 0D ? ? ? ? 81 F9 ? ? ? ? 75 0C A1 ? ? ? ? 35 ? ? ? ? EB 05 8B 01 FF 50 34 50 A1
            NetVarManager.Init();
        }

        public static void InitNetvars(Dictionary<string, Dictionary<string, int>> _tables)
        {
            Netvars.m_ArmorValue =              _tables["DT_CSPlayer"]["m_ArmorValue"];
            Netvars.m_OriginalOwnerXuidHigh =   _tables["DT_BaseAttributableItem"]["m_OriginalOwnerXuidHigh"];
            Netvars.m_OriginalOwnerXuidLow =    _tables["DT_BaseAttributableItem"]["m_OriginalOwnerXuidLow"];
            Netvars.m_aimPunchAngle =           _tables["DT_BasePlayer"]["m_aimPunchAngle"];
            Netvars.m_aimPunchAngleVel =        _tables["DT_BasePlayer"]["m_aimPunchAngleVel"];
            Netvars.m_bHasDefuser =             _tables["DT_CSPlayer"]["m_bHasDefuser"];
            Netvars.m_bHasHelmet =              _tables["DT_CSPlayer"]["m_bHasHelmet"];
            Netvars.m_bIsDefusing =             _tables["DT_CSPlayer"]["m_bIsDefusing"];
            Netvars.m_bIsScoped =               _tables["DT_CSPlayer"]["m_bIsScoped"];
            Netvars.m_bSpotted =                _tables["DT_BaseEntity"]["m_bSpotted"];
            Netvars.m_bSpottedByMask =          _tables["DT_BaseEntity"]["m_bSpottedByMask"];
            Netvars.m_dwBoneMatrix =            _tables["DT_BaseAnimating"]["m_nForceBone"] + 28;
            Netvars.m_fFlags =                  _tables["DT_CSPlayer"]["m_fFlags"];
            Netvars.m_flFallbackWear =          _tables["DT_BaseAttributableItem"]["m_flFallbackWear"];
            Netvars.m_flFlashMaxAlpha =         _tables["DT_CSPlayer"]["m_flFlashMaxAlpha"];
            Netvars.m_flNextPrimaryAttack =     _tables["DT_BaseCombatWeapon"]["m_flNextPrimaryAttack"];
            Netvars.m_hActiveWeapon =           _tables["DT_BasePlayer"]["m_hActiveWeapon"];
            Netvars.m_hMyWeapons =              _tables["DT_BasePlayer"]["m_hActiveWeapon"] - 256;
            Netvars.m_hObserverTarget =         _tables["DT_BasePlayer"]["m_hObserverTarget"];
            Netvars.m_hOwner =                  _tables["DT_PredictedViewModel"]["m_hOwner"];
            Netvars.m_hOwnerEntity =            _tables["DT_CSPlayer"]["m_hOwnerEntity"];
            Netvars.m_iAccountID =              _tables["DT_BaseAttributableItem"]["m_iAccountID"];
            Netvars.m_iClip1 =                  _tables["DT_BaseCombatWeapon"]["m_iClip1"];
            Netvars.m_iCompetitiveRanking =     _tables["DT_CSPlayerResource"]["m_iCompetitiveRanking"];
            Netvars.m_iCompetitiveWins =        _tables["DT_CSPlayerResource"]["m_iCompetitiveWins"];
            Netvars.m_iCrosshairId =            _tables["DT_CSPlayer"]["m_bHasDefuser"] + 92;
            Netvars.m_iEntityQuality =          _tables["DT_BaseAttributableItem"]["m_iEntityQuality"];
            Netvars.m_iGlowIndex =              _tables["DT_CSPlayer"]["m_flFlashDuration"] + 24;
            Netvars.m_iHealth =                 _tables["DT_BasePlayer"]["m_iHealth"];
            Netvars.m_iItemDefinitionIndex =    _tables["DT_BaseCombatWeapon"]["m_iItemDefinitionIndex"];
            Netvars.m_iItemIDHigh =             _tables["DT_BaseAttributableItem"]["m_iItemIDHigh"];
            Netvars.m_iItemIDLow =              _tables["DT_BaseAttributableItem"]["m_iItemIDLow"];
            Netvars.m_iObserverMode =           _tables["DT_BasePlayer"]["m_iObserverMode"];
            Netvars.m_iShotsFired =             _tables["DT_CSPlayer"]["m_iShotsFired"];
            Netvars.m_iState =                  _tables["DT_BaseCombatWeapon"]["m_iState"];
            Netvars.m_iTeamNum =                _tables["DT_BasePlayer"]["m_iTeamNum"];
            Netvars.m_lifeState =               _tables["DT_CSPlayer"]["m_lifeState"];
            Netvars.m_nFallbackPaintKit =       _tables["DT_BaseAttributableItem"]["m_nFallbackPaintKit"];
            Netvars.m_nFallbackSeed =           _tables["DT_BaseAttributableItem"]["m_nFallbackSeed"];
            Netvars.m_nFallbackStatTrak =       _tables["DT_BaseAttributableItem"]["m_nFallbackStatTrak"];
            Netvars.m_nForceBone =              _tables["DT_BaseAnimating"]["m_nForceBone"];
            Netvars.m_nTickBase =               _tables["DT_BasePlayer"]["m_nTickBase"];
            Netvars.m_szCustomName =            _tables["DT_BaseAttributableItem"]["m_szCustomName"];
            Netvars.m_szLastPlaceName =         _tables["DT_CSPlayer"]["m_szLastPlaceName"];
            Netvars.m_vecOrigin =               _tables["DT_BasePlayer"]["m_vecOrigin"];
            Netvars.m_vecVelocity =             _tables["DT_CSPlayer"]["m_vecVelocity[0]"];
            Netvars.m_vecViewOffset =           _tables["DT_CSPlayer"]["m_vecViewOffset[0]"];
            Netvars.m_viewPunchAngle =          _tables["DT_BasePlayer"]["m_viewPunchAngle"];
            Netvars.m_thirdPersonViewAngles =   _tables["DT_CSPlayer"]["deadflag"] + 4; //0x31C8; //offset4
            Netvars.m_clrRender =               _tables["DT_BaseEntity"]["m_clrRender"];
            Netvars.m_zoomLevel =               _tables["DT_WeaponCSBaseGun"]["m_zoomLevel"];
            Netvars.m_bDrawViewmodel =          _tables["DT_BasePlayer"]["m_bDrawViewmodel"];
            Netvars.m_iFOVStart =               _tables["DT_CSPlayer"]["m_iFOVStart"];
            Netvars.m_flC4Blow =                _tables["DT_PlantedC4"]["m_flC4Blow"];
            Netvars.m_fAccuracyPenalty =        _tables["DT_WeaponCSBase"]["m_fAccuracyPenalty"];
        }


    }
}
