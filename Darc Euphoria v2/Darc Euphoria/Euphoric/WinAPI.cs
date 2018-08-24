using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Darc_Euphoria.Euphoric.Enums;
using Darc_Euphoria.Euphoric.Structs;

namespace Darc_Euphoria.Euphoric
{
    internal static class WinAPI
    {
        public const int PAGE_READWRITE = 0x40;

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out Rect RECT);

        [DllImport("user32.dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, out Point point);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect RECT);

        [DllImport("user32.dll")]
        public static extern bool BlockInput(bool fBlockIt);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        [DllImport("dwmapi.dll")]
        public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int GetAsyncKeyState(int vKey);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess,
            [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        internal static extern bool CloseHandle(IntPtr hProcess);

        [DllImport("Kernel32.dll")]
        internal static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,
            uint nSize, out uint lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer,
            IntPtr nSize, out uint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, int size,
            int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize,
            FreeType dwFreeType);

        [DllImport("kernel32.dll")]
        internal static extern uint WaitForSingleObject(IntPtr hProcess, uint dwMilliseconds);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, IntPtr dwStackSize,
            IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize,
            uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, uint dwFreeType);

        [DllImport("gdi32.dll")]
        public static extern unsafe bool SetDeviceGammaRamp(int hdc, void* ramp);

        internal delegate int ThreadProc(IntPtr param);
    }
}