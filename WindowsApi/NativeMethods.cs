using System;
using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi
{
    /// <summary>
    /// Common Windows API functions.
    /// </summary>
    internal static class NativeMethods
    {
        private const string Kernel32 = "kernel32.dll";

        [DllImport(Kernel32)]
        public static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);
    }
}
