using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Darc_Euphoria.Euphoric.BspParsing
{
    [StructLayout(LayoutKind.Sequential)]
    public struct dheader_t
    {
        public int m_Ident;
        public int m_Version;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = BSPFlags.HEADER_LUMPS)]
        public lump_t[] m_Lumps;
        public int m_MapRevision;
    }
}
