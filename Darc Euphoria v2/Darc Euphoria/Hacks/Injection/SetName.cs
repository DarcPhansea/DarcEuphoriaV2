    using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Darc_Euphoria.Hacks.Injection
{
    public static class SetName
    {
        public static byte[] Shellcode = {
            0x55,
            0x8B, 0xEC,
            0x83, 0xE4, 0xF8,
            0x83, 0xEC, 0x44,
            0x53,
            0x56,
            0x57,
            0xBF, 0x00, 0x00, 0x00, 0x00,
            0xBE, 0x00, 0x00, 0x00, 0x00,
            0xB8, 0x00, 0x00, 0x00, 0x00,
            0xFF, 0xE0,
            0x6E, 0x61, 0x6D, 0x65, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
        };

        public static int Size = Shellcode.Length;
        public static IntPtr Address;
        public static string PREVNAME = String.Empty;

        public static void Set(string name)
        {
            if (Address == IntPtr.Zero)
            {
                Address = gvar.Allocator.Alloc(Size);

                if (Address == IntPtr.Zero)
                    return;

                Buffer.BlockCopy(BitConverter.GetBytes((int)Address + 0x1D), 0, Shellcode, 0xD, 4);
                Buffer.BlockCopy(BitConverter.GetBytes((int)Address + 0x22), 0, Shellcode, 0x12, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Memory.engine + Offsets.dw_SetConVar), 0, Shellcode, 0x17, 4);
                
            }

            if (!Local.InGame) return;

            if (name == PREVNAME) return;
            else PREVNAME = name;

            byte[] reset = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            byte[] name_bytes;
            if (name == "\n")
            {
                name_bytes = Encoding.UTF8.GetBytes('\n' + "\0");
            }
            else
            {
                name_bytes = Encoding.UTF8.GetBytes(name + "\0");
            }

            Buffer.BlockCopy(reset, 0, Shellcode, 0x22, reset.Length);
            Buffer.BlockCopy(name_bytes, 0, Shellcode, 0x22, name_bytes.Length);
            WinAPI.WriteProcessMemory(Memory.pHandle, Address, Shellcode, Shellcode.Length, 0);

            for (int i = 0; i < 1000; i++)
            {
                CreateThread.Execute(Address);
                Thread.Sleep(1);
            }
        }

    }
}
