using System.Runtime.InteropServices;
using Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Euphoric.BspParsing
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct dplane_t
    {
        public Vector3 m_Normal;
        public float m_Distance;
        public byte m_Type;
    }
}