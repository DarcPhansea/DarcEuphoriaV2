using System.Runtime.InteropServices;

namespace Darc_Euphoria.Euphoric.Structs
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CUserCmd
    {
        public int command_number; //0x4
        public int tick_count; //0x8
        public Vector3 viewangles; //0xC
        public Vector3 aimdirection; //0x18
        public float forwardmove; //0x1C
        public float sidemove; //0x20
        public float upmove; //0x24
        public int buttons; //0x28
        public char impulse; //0x2C
        public int weaponselect; //0x2E
        public int weaponsubtype; //0x32
        public int random_seed; //0x36
        public short mousedx; //2
        public short mousedy; //2

        [MarshalAs(UnmanagedType.U1, SizeConst = 1)]
        public bool hasbeenpredicted; //1

        public Vector3 headangles; //12
        public Vector3 headoffset; //12

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x2)]
        public char[] unknown;
    }
}