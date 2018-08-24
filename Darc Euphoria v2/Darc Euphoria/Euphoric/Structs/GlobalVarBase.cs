using System.Runtime.InteropServices;

namespace Darc_Euphoria.Euphoric.Structs
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GlobalVarBase
    {
        public float realtime;
        public int framecount;
        public float absolute_frametime;
        public float absolute_framestarttimestddev;
        public float curtime;
        public float frameTime;
        public int maxClients;
        public int tickcount;
        public float interval_per_tick;
        public float interpolation_amount;
        public int simThicksThisFrame;
        public int network_protocol;
    }
}