using System;
using Darc_Euphoria.Euphoric;

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

        public static int Size = Shellcode.Length;
        public static IntPtr Address;

        public static void Show()
        {
            if (Address == IntPtr.Zero)
            {
                Address = gvar.Allocator.Alloc(Shellcode.Length + 12);

                if (Address == IntPtr.Zero)
                    return;

                Buffer.BlockCopy(BitConverter.GetBytes((int) Address + Shellcode.Length), 0, Shellcode, 1, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Memory.Client + Offsets.dw_RevealRankFn), 0, Shellcode, 6, 4);

                uint nBytesWritten = 0;
                WinAPI.WriteProcessMemory(Memory.pHandle, Address, Shellcode, (IntPtr) Shellcode.Length,
                    out nBytesWritten);
            }

            CreateThread.Execute(Address);
        }
    }
}