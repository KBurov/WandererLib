using System;
using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.Nt
{
    /// <summary>
    /// A NT API functions.
    /// </summary>
    internal static class Functions
    {
        private const string NtDll = "ntdll.dll";

        [DllImport(NtDll)]
        public static extern NtStatus NtResumeProcess([In] IntPtr processHandle);

        [DllImport(NtDll)]
        public static extern NtStatus NtSuspendProcess([In] IntPtr processHandle);
    }
}
