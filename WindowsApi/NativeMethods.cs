using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

using Wanderer.Library.WindowsApi.SafeHandles;

namespace Wanderer.Library.WindowsApi
{
    /// <summary>
    /// Common Windows API functions.
    /// </summary>
    internal static class NativeMethods
    {
        private const string Advapi32 = "advapi32.dll";
        private const string Kernel32 = "kernel32.dll";
        private const string User32 = "user32.dll";

        private const string ProcessHandleExceptionMessage = "processHandle cannot be null";

        public static void ReportWin32Exception()
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        [DllImport(Kernel32, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle([In] IntPtr handle);

        #region GetExitCodeProcess
        public static uint GetExitCodeProcess(SafeTokenHandle processHandle)
        {
            Contract.Requires<ArgumentNullException>(processHandle != null, ProcessHandleExceptionMessage);

            uint exitCode;

            if (!GetExitCodeProcess(processHandle.DangerousGetHandle(), out exitCode)) {
                ReportWin32Exception();
            }

            return exitCode;
        }

        [DllImport(Kernel32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetExitCodeProcess(
            [In] IntPtr hProcess,
            [Out] out uint lpExitCode);
        #endregion
    }
}