﻿using Darc_Euphoria.Euphoric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darc_Euphoria.Hacks.Injection
{
    public static class CreateThread
    {
        public static void Create(IntPtr address, byte[] shellcode)
        {
            WinAPI.WriteProcessMemory(Memory.pHandle, address, shellcode, shellcode.Length, 0);
            IntPtr _Thread = WinAPI.CreateRemoteThread(Memory.pHandle, (IntPtr)null, (IntPtr)null, address, (IntPtr)null, 0, (IntPtr)null);
            WinAPI.WaitForSingleObject(_Thread, 0xFFFFFFFF);
            WinAPI.CloseHandle(_Thread);
        }

        public static void Execute(IntPtr address)
        {
            IntPtr _Thread = WinAPI.CreateRemoteThread(Memory.pHandle, (IntPtr)null, (IntPtr)null, address, (IntPtr)null, 0, (IntPtr)null);
            WinAPI.WaitForSingleObject(_Thread, 0xFFFFFFFF);
            WinAPI.CloseHandle(_Thread);
        }
    }
}
