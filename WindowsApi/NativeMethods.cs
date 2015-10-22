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

        public static void ReportWin32Exception()
        {
            throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        [DllImport(Kernel32, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle([In] IntPtr handle);

        #region DuplicateHandle
        public static SafeTokenHandle DuplicateHandle(SafeTokenHandle sourceProcessHandle, SafeTokenHandle sourceHandle, SafeTokenHandle targetProcessHandle,
                                                      uint desiredAccess, bool inheritHandle, DuplicateOptions options)
        {
            Contract.Requires<ArgumentNullException>(sourceProcessHandle != null, "sourceProcessHandle cannot be null");
            Contract.Requires<ArgumentNullException>(sourceHandle != null, "sourceHandle cannot be null");
            Contract.Requires<ArgumentNullException>(targetProcessHandle != null, "targetProcessHandle cannot be null");

            IntPtr handle;

            if (!DuplicateHandle(sourceProcessHandle.DangerousGetHandle(), sourceHandle.DangerousGetHandle(), targetProcessHandle.DangerousGetHandle(),
                                 out handle, desiredAccess, inheritHandle, (uint) options)) {
                ReportWin32Exception();
            }

            return new SafeTokenHandle(handle);
        }

        [DllImport(Kernel32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DuplicateHandle(
            [In] IntPtr hSourceProcessHandle,
            [In] IntPtr hSourceHandle,
            [In] IntPtr hTargetProcessHandle,
            [Out] out IntPtr lpTargetHandle,
            [In] uint dwDesiredAccess,
            [In, MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
            [In] uint dwOptions);
        #endregion

        #region GetExitCodeProcess
        public static uint GetExitCodeProcess(SafeTokenHandle processHandle)
        {
            Contract.Requires<ArgumentNullException>(processHandle != null, "processHandle cannot be null");

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