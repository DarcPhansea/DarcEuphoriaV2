﻿using System.Runtime.InteropServices;

namespace Darc_Euphoria.Euphoric.BspParsing
{
    [StructLayout(LayoutKind.Sequential)]
    public struct StaticPropDictLump_t
    {
        public int m_DictEntries;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct StaticPropDictLumpName
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string m_Name;
    }
}