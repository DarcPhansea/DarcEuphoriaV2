using System.Runtime.InteropServices;

namespace Darc_Euphoria.Euphoric.BspParsing
{
    [StructLayout(LayoutKind.Sequential)]
    public struct dbrush_t
    {
        public int m_Firstside;
        public int m_Numsides;
        public int m_Contents;
    }
}