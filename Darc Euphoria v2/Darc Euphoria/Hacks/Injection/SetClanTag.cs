﻿using System;
using System.Text;
using Darc_Euphoria.Euphoric;
using Darc_Euphoria.Euphoric.Objects;

namespace Darc_Euphoria.Hacks.Injection
{
    public static class SetClanTag
    {
        public static byte[] Shellcode =
        {
            0xB9, 0x00, 0x00, 0x00, 0x00,
            0xBA, 0x00, 0x00, 0x00, 0x00,
            0xB8, 0x00, 0x00, 0x00, 0x00,
            0xFF, 0xD0,
            0xC3,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
        };

        public static int Size = Shellcode.Length;
        public static IntPtr Address;
        public static string PREVNAME = string.Empty;

        public static void Set(string tag)
        {
            if (Address == IntPtr.Zero)
            {
                Address = gvar.Allocator.Alloc(Size);
                gvar.Allocator.Free();

                if (Address == IntPtr.Zero)
                    return;

                Buffer.BlockCopy(BitConverter.GetBytes((int) (Address + 18)), 0, Shellcode, 1, 4);
                Buffer.BlockCopy(BitConverter.GetBytes((int) (Address + 18)), 0, Shellcode, 6, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Memory.Engine + Offsets.dwSetClanTag), 0, Shellcode, 11, 4);
            }

            if (!Local.InGame) return;

            if (tag == PREVNAME) return;
            PREVNAME = tag;

            var tag_bytes = Encoding.UTF8.GetBytes(tag + "\0");
            byte[] reset =
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            Buffer.BlockCopy(reset, 0, Shellcode, 18, reset.Length);
            Buffer.BlockCopy(tag_bytes, 0, Shellcode, 18, tag.Length > 15 ? 15 : tag.Length);
            CreateThread.Create(Address, Shellcode);
        }
    }
}