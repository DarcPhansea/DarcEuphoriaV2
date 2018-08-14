using Darc_Euphoria.Euphoric;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Hacks.Injection
{
    public static class RankRevealer
    {
        public static byte[] Shellcode =
        {
            0x68, 0x00, 0x00, 0x00, 0x00,
            0xB8, 0x00, 0x00, 0x00, 0x00,
            0xFF, 0xD0,
            0x83, 0xC4, 0x04,
            0xC2, 0x04, 0x00
        };

        public static byte[] Shellcode2 = {
            0x68,0x00,0x00,0x00,0x00,
            0x55,0x89,0xE5,
            0xB8,0x00,0x00,0x00,0x00,
            0xFF,0xD0,
            0x83,0xC4,0x04,
            0x5D,
            0xC3,
        };

        public static byte[] Shellcode3 = {
            0xA1,0x00,0x00,0x00,0x00,
            0xFF,0x35,0x00,0x00,0x00,0x00,
            0xFF,0xD0,
            0x83,0xC4,0x04,
            0xC2,0x04,
        };

        public static int Size = Shellcode.Length;
        public static IntPtr Address;

        public static void Show()
        {
            if (Address == IntPtr.Zero)
            {
                Address = gvar.Allocator.Alloc(Shellcode.Length + 12);

                if (Address == IntPtr.Zero)
                    return;

                Buffer.BlockCopy(BitConverter.GetBytes((int)Address + Shellcode.Length), 0, Shellcode, 1, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Memory.client + Offsets.dw_RevealRankFn), 0, Shellcode, 6, 4);
                
                WinAPI.WriteProcessMemory(Memory.pHandle, Address, Shellcode, Shellcode.Length, 0);
            }

            CreateThread.Execute(Address);
        }
    }
}
