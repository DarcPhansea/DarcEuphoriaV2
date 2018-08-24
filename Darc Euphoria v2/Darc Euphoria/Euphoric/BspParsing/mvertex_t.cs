using System.Runtime.InteropServices;
using Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Euphoric.BspParsing
{
    [StructLayout(LayoutKind.Sequential)]
    public struct mvertex_t
    {
        public Vector3 m_Position;
    }
}