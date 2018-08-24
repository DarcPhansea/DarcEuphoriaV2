using System;
using Darc_Euphoria.Euphoric;

namespace Darc_Euphoria.Hacks.Injection
{
    public static class CreateThread
    {
        public static void Create(IntPtr address, byte[] shellcode)
        {
            WinAPI.WriteProcessMemory(Memory.pHandle, address, shellcode, shellcode.Length, 0);
            var _Thread = WinAPI.CreateRemoteThread(Memory.pHandle, (IntPtr) null, (IntPtr) null, address,
                (IntPtr) null, 0, (IntPtr) null);
            WinAPI.WaitForSingleObject(_Thread, 0xFFFFFFFF);
            WinAPI.CloseHandle(_Thread);
        }

        public static void Execute(IntPtr address)
        {
            try
            {
                var _Thread = WinAPI.CreateRemoteThread(Memory.pHandle, (IntPtr) null, (IntPtr) null, address,
                    (IntPtr) null, 0, (IntPtr) null);
                WinAPI.WaitForSingleObject(_Thread, 0xFFFFFFFF);
                WinAPI.CloseHandle(_Thread);
            }
            catch
            {
            }
        }
    }
}