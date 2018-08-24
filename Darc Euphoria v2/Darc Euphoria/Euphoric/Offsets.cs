namespace Darc_Euphoria.Euphoric
{
    internal static class Offsets
    {
        public const int m_bDormant = 0xE9;


        public const int m_szArmsModel = 0x38D7;
        public const int m_iHideHUD = 0x2FF4;
        public const int m_viewFOV = 0x330C;
        public static int dwClientState;
        public static int dwClientState_GetLocalPlayer;
        public static int dwClientState_IsHLTV;
        public static int dwClientState_Map;
        public static int dwClientState_MapDirectory;
        public static int dwClientState_MaxPlayer;
        public static int dwClientState_PlayerInfo;
        public static int dwClientState_State;
        public static int dwClientState_ViewAngles;
        public static int m_dwClientState_LastOutgoingCommand = 0x4CA8;
        public static int dwEntityList;
        public static int dwForceAttack;
        public static int dwForceAttack2;
        public static int dwGameDir;
        public static int dwGameRulesProxy;
        public static int dwGlobalVars;
        public static int dwGlowObjectManager;
        public static int dwInput;
        public static int dwLocalPlayer;
        public static int dwPlayerResource;
        public static int dwRadarBase;
        public static int dwSetClanTag;
        public static int dw_SetConVar;
        public static int dwViewMatrix;
        public static int dwWeaponTable;
        public static int dwWeaponTableIndex;
        public static int dw_RevealRankFn;
        public static int dwSendPackets;
        public static int dwForceJump;
        public static int LastOutGoingCommand;
        public static int dwEntityListLength;
        public static int dw_clientCmd;
        public static int s_bOverridePostProcessingDisable;

        public static int m_nModelIndex = 0x254;

        public static int noarms;
        /*
        public static int dw_CLobbyScreen;
        public static int dw_AcceptMatch;
        public static int dw_MatchAccepted;
        public static int dw_MatchFound;
        public static int CL_Move = 0x0D7E1000;
        

        public static int m_hViewModel = 0x32FC;
        public static int m_iViewModelIndex = 0x31E0;
        public static int m_iWorldModelIndex = 0x31E4;
        public static int m_iWorldDroppedModelIndex = 0x31E8;
        */
    }

    public static class Netvars
    {
        public static int m_ArmorValue;
        public static int m_OriginalOwnerXuidHigh;
        public static int m_OriginalOwnerXuidLow;
        public static int m_aimPunchAngle;
        public static int m_aimPunchAngleVel;
        public static int m_bHasDefuser;
        public static int m_bHasHelmet;
        public static int m_bIsDefusing;
        public static int m_bIsScoped;
        public static int m_bSpotted;
        public static int m_bSpottedByMask;
        public static int m_dwBoneMatrix;
        public static int m_fFlags;
        public static int m_flFallbackWear;
        public static int m_flFlashMaxAlpha;
        public static int m_flNextPrimaryAttack;
        public static int m_hActiveWeapon;
        public static int m_hMyWeapons;
        public static int m_hObserverTarget;
        public static int m_hOwner;
        public static int m_hOwnerEntity;
        public static int m_iAccountID;
        public static int m_iClip1;
        public static int m_iCompetitiveRanking;
        public static int m_iCompetitiveWins;
        public static int m_iCrosshairId;
        public static int m_iEntityQuality;
        public static int m_iGlowIndex;
        public static int m_iHealth;
        public static int m_iItemDefinitionIndex;
        public static int m_iItemIDHigh;
        public static int m_iItemIDLow;
        public static int m_iObserverMode;
        public static int m_iShotsFired;
        public static int m_iState;
        public static int m_iTeamNum;
        public static int m_lifeState;
        public static int m_nFallbackPaintKit;
        public static int m_nFallbackSeed;
        public static int m_nFallbackStatTrak;
        public static int m_nForceBone;
        public static int m_nTickBase;
        public static int m_szCustomName;
        public static int m_szLastPlaceName;
        public static int m_vecOrigin;
        public static int m_vecVelocity;
        public static int m_vecViewOffset;
        public static int m_viewPunchAngle;
        public static int m_thirdPersonViewAngles;
        public static int m_clrRender;
        public static int m_zoomLevel;
        public static int m_bDrawViewmodel;
        public static int m_iFOVStart;
        public static int m_flC4Blow;
        public static int m_fAccuracyPenalty;
    }
}