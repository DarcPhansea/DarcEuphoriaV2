using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Darc_Euphoria.Euphoric
{
    internal static class Memory
    {
        public static IntPtr pHandle;
        public static Process process;

        public static int Client = 0;
        public static int client_size = 0;

        public static int Engine = 0;
        public static int engine_size = 0;

        public static bool OpenProcess(string name)
        {
            try
            {
                process = Process.GetProcessesByName(name)[0];
                pHandle = process.Handle;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static T GetStructure<T>(byte[] bytes)
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            var structure = (T) Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return structure;
        }

        public static byte[] GetStructBytes<T>(T str)
        {
            var size = MarshalSize<T>.Size;

            var arr = new byte[size];

            var ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(str, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        public static T Read<T>(int address)
        {
            var length = MarshalSize<T>.Size;

            if (typeof(T) == typeof(bool))
                length = 1;

            var buffer = new byte[length];
            var nBytesRead = 0u;

            try
            {
                var success =
                    WinAPI.ReadProcessMemory(pHandle, (IntPtr) address, buffer, (uint) length, out nBytesRead);
            }
            catch
            {
                gvar.isShuttingDown = true;
                Environment.Exit(Environment.ExitCode);
            }

            return GetStructure<T>(buffer);
        }

        public static void Write<T>(int address, T value)
        {
            var length = MarshalSize<T>.Size;
            var buffer = new byte[length];

            var ptr = Marshal.AllocHGlobal(length);
            Marshal.StructureToPtr(value, ptr, true);
            Marshal.Copy(ptr, buffer, 0, length);
            Marshal.FreeHGlobal(ptr);

            var nBytesRead = 0u;
            try
            {
                WinAPI.WriteProcessMemory(pHandle, (IntPtr) address, buffer, (IntPtr) length, out nBytesRead);
            }
            catch
            {
                gvar.isShuttingDown = true;
                Environment.Exit(Environment.ExitCode);
            }
        }

        public static byte[] ReadBytes(int address, int length)
        {
            var buffer = new byte[length];
            var nBytesRead = uint.MinValue;
            var success = WinAPI.ReadProcessMemory(pHandle, (IntPtr) address, buffer, (uint) length, out nBytesRead);
            return buffer;
        }

        public static void WriteBytes(int address, byte[] value)
        {
            var nBytesRead = uint.MinValue;
            WinAPI.WriteProcessMemory(pHandle, (IntPtr) address, value, (IntPtr) value.Length, out nBytesRead);
        }

        public static string ReadString(int address, int bufferSize, Encoding enc)
        {
            var buffer = new byte[bufferSize];
            uint nBytesRead = 0;
            var success =
                WinAPI.ReadProcessMemory(pHandle, (IntPtr) address, buffer, (uint) bufferSize, out nBytesRead);
            var text = enc.GetString(buffer);
            if (text.Contains('\0'))
                text = text.Substring(0, text.IndexOf('\0'));

            return text;
        }
    }
}