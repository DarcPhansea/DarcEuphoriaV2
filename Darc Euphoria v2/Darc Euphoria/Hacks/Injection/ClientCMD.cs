using Darc_Euphoria.Euphoric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darc_Euphoria.Hacks.Injection
{
    public static class ClientCMD
    {
        public static int Size = 256;
        public static IntPtr Address;

        public static void Exec(string szCmd)
        {
            if (Address == IntPtr.Zero)
            {
                Address = gvar.Allocator.Alloc(Size);
                if (Address == IntPtr.Zero)
                    return;
            }
            if (szCmd.Length > 255)
                szCmd = szCmd.Substring(0, 255);

            var szCmd_bytes = Encoding.UTF8.GetBytes(szCmd + "\0");

            WinAPI.WriteProcessMemory(Memory.pHandle, Address, szCmd_bytes, szCmd_bytes.Length, 0);
            IntPtr Thread = WinAPI.CreateRemoteThread(Memory.pHandle, (IntPtr)null, IntPtr.Zero, new IntPtr(Memory.engine + Offsets.dw_clientCmd), Address, 0, (IntPtr)null);
            WinAPI.CloseHandle(Thread);
            WinAPI.WaitForSingleObject(Thread, 0xFFFFFFFF);
        }
    }
}
