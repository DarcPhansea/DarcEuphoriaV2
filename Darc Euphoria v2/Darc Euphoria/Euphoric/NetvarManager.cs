﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Darc_Euphoria.Euphoric
{
    public static class NetVarManager
    {
        public static Dictionary<string, Dictionary<string, int>> _tables = new Dictionary<string, Dictionary<string, int>>();

        public static int FirstTable(string pattern_str, int offset)
        {
            List<byte> temp = new List<byte>();
            string mask = "";

            foreach (string l in pattern_str.Split(' '))
                if (l == "?" || l == "00")
                {
                    temp.Add(0x00);
                    mask += "?";
                }
                else
                {
                    temp.Add((byte)int.Parse(l, System.Globalization.NumberStyles.HexNumber));
                    mask += "x";
                }

            byte[] pattern = temp.ToArray();

            byte[] moduleBytes = new byte[Memory.client_size];
            uint numBytes = 0;

            if (WinAPI.ReadProcessMemory(Memory.pHandle, (IntPtr)Memory.client, moduleBytes, (uint)Memory.client_size, out numBytes))
            {
                for (int i = 0; i < Memory.client_size; i++)
                {
                    bool found = true;

                    for (int l = 0; l < mask.Length; l++)
                    {
                        found = mask[l] == '?' || moduleBytes[l + i] == pattern[l];

                        if (!found) break;
                    }

                    if (found)
                    {
                        i += Memory.client;
                        i = Memory.Read<int>(i + offset);
                        return i;
                    }
                }
            }
            return 0;
        }

        public static void ScanTable(IntPtr table, int level, int offset, string name)
        {
            var count = Memory.Read<Int32>((Int32)table + 0x4);

            for(var i = 0; i < count; i++)
            {
                int propID = Memory.Read<Int32>((Int32)table) + i * 0x3C;
                string propName = Memory.ReadString(Memory.Read<Int32>(propID), 64, Encoding.Default);
                var isBaseClass = propName.IndexOf("baseclass") == 0;
                var propOffset = offset + Memory.Read<Int32>(propID + 0x2C);
                if (!isBaseClass)
                {
                    if (!_tables.ContainsKey(name))
                        _tables.Add(name, new Dictionary<string, int>());

                    if (!_tables[name].ContainsKey(propName))
                    {
                        _tables[name].Add(propName, propOffset);
                    }

                }

                var child = Memory.Read<IntPtr>(propID + 0x28);

                if (child == IntPtr.Zero)
                    continue;

                if (!isBaseClass)
                    --level;

                ScanTable(child, ++level, propOffset, name);
            }
        }

        public static void Init()
        {
            var _firstclass = new IntPtr(FirstTable(
                "A1 ? ? ? ? C3 CC CC CC CC CC CC CC CC CC CC A1 ? ? ? ? B9", 0x1));



            _firstclass = Memory.Read<IntPtr>((Int32)_firstclass);

            if (_firstclass == IntPtr.Zero)
            {
                MessageBox.Show("Error Has Occured While Getting The NetVars...\nExiting Now.");
                Environment.Exit(-1);
            }

            do
            {
                var table = Memory.Read<IntPtr>((Int32)_firstclass + 0xC);
                if (table != IntPtr.Zero)
                {
                    string table_name = Memory.ReadString(Memory.Read<Int32>((Int32)table + 0xC), 32, Encoding.Default);
                    ScanTable(table, 0, 0, table_name);
                }
                _firstclass = Memory.Read<IntPtr>((Int32)_firstclass + 0x10);
            } while (_firstclass != IntPtr.Zero);

            Sig.InitNetvars(_tables);
        }
    }

}
