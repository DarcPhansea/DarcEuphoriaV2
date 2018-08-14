using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darc_Euphoria.Euphoric
{
    internal static class Offsets
    {
        public static Int32 dwClientState;
        public static Int32 dwClientState_GetLocalPlayer;
        public static Int32 dwClientState_IsHLTV;
        public static Int32 dwClientState_Map;
        public static Int32 dwClientState_MapDirectory;
        public static Int32 dwClientState_MaxPlayer;
        public static Int32 dwClientState_PlayerInfo;
        public static Int32 dwClientState_State;
        public static Int32 dwClientState_ViewAngles;
        public static Int32 m_dwClientState_LastOutgoingCommand = 0x4CA8;
        public static Int32 dwEntityList;
        public static Int32 dwForceAttack;
        public static Int32 dwForceAttack2;
        public static Int32 dwGameDir;
        public static Int32 dwGameRulesProxy;
        public static Int32 dwGlobalVars;
        public static Int32 dwGlowObjectManager;
        public static Int32 dwInput;
        public static Int32 dwLocalPlayer;
        public static Int32 dwPlayerResource;
        public static Int32 dwRadarBase;
        public static Int32 dwSetClanTag;
        public static Int32 dw_SetConVar;
        public static Int32 dwViewMatrix;
        public static Int32 dwWeaponTable;
        public static Int32 dwWeaponTableIndex;
        public static Int32 dw_RevealRankFn;
        public static Int32 dwSendPackets;
        public static Int32 dwForceJump;
        public static Int32 LastOutGoingCommand;
        public static int dwEntityListLength;

        public static Int32 dw_clientCmd;
        public static Int32 s_bOverridePostProcessingDisable;

        public static Int32 noarms;
        public static Int32 dw_CLobbyScreen;
        public static Int32 dw_AcceptMatch;
        public static Int32 dw_MatchAccepted;
        public static Int32 dw_MatchFound;

        public const Int32 m_bDormant = 0xE9;

             
        public const Int32 m_szArmsModel = 0x38D7;
        public const Int32 m_iHideHUD = 0x2FF4;
        public const Int32 m_viewFOV = 0x330C;
        public static Int32 CL_Move = 0x0D7E1000;


        public static Int32 m_hViewModel = 0x32FC;
        public static Int32 m_nModelIndex = 0x254;
        public static Int32 m_iViewModelIndex = 0x31E0;
        public static Int32 m_iWorldModelIndex = 0x31E4;
        public static Int32 m_iWorldDroppedModelIndex = 0x31E8;
        
    }

    public static class Netvars
    {
        public static Int32 m_ArmorValue;
        public static Int32 m_OriginalOwnerXuidHigh;
        public static Int32 m_OriginalOwnerXuidLow;
        public static Int32 m_aimPunchAngle;
        public static Int32 m_aimPunchAngleVel;
        public static Int32 m_bHasDefuser;
        public static Int32 m_bHasHelmet;
        public static Int32 m_bIsDefusing;
        public static Int32 m_bIsScoped;
        public static Int32 m_bSpotted;
        public static Int32 m_bSpottedByMask;
        public static Int32 m_dwBoneMatrix;
        public static Int32 m_fFlags;
        public static Int32 m_flFallbackWear;
        public static Int32 m_flFlashMaxAlpha;
        public static Int32 m_flNextPrimaryAttack;
        public static Int32 m_hActiveWeapon;
        public static Int32 m_hMyWeapons;
        public static Int32 m_hObserverTarget;
        public static Int32 m_hOwner;
        public static Int32 m_hOwnerEntity;
        public static Int32 m_iAccountID;
        public static Int32 m_iClip1;
        public static Int32 m_iCompetitiveRanking;
        public static Int32 m_iCompetitiveWins;
        public static Int32 m_iCrosshairId;
        public static Int32 m_iEntityQuality;
        public static Int32 m_iGlowIndex;
        public static Int32 m_iHealth;
        public static Int32 m_iItemDefinitionIndex;
        public static Int32 m_iItemIDHigh;
        public static Int32 m_iItemIDLow;
        public static Int32 m_iObserverMode;
        public static Int32 m_iShotsFired;
        public static Int32 m_iState;
        public static Int32 m_iTeamNum;
        public static Int32 m_lifeState;
        public static Int32 m_nFallbackPaintKit;
        public static Int32 m_nFallbackSeed;
        public static Int32 m_nFallbackStatTrak;
        public static Int32 m_nForceBone;
        public static Int32 m_nTickBase;
        public static Int32 m_szCustomName;
        public static Int32 m_szLastPlaceName;
        public static Int32 m_vecOrigin;
        public static Int32 m_vecVelocity;
        public static Int32 m_vecViewOffset;
        public static Int32 m_viewPunchAngle;
        public static Int32 m_thirdPersonViewAngles;
        public static Int32 m_clrRender;
        public static Int32 m_zoomLevel;
        public static Int32 m_bDrawViewmodel;
        public static Int32 m_iFOVStart;
        public static Int32 m_flC4Blow;
        public static Int32 m_fAccuracyPenalty;
    }
}
