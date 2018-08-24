using System.Runtime.InteropServices;

namespace Darc_Euphoria.Euphoric.BspParsing
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StaticPropLeafLump_t
    {
        public int m_LeafEntries;
    }
}