﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Darc_Euphoria.Euphoric.BspParsing
{
    [StructLayout(LayoutKind.Sequential)]
    public struct dbrushside_t
    {
        public ushort m_Planenum;
        public short m_Texinfo;
        public short m_Dispinfo;
        public byte m_Bevel;
        public byte m_Thin;
    }
}
