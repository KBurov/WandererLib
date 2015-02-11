using System;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

using Wanderer.Library.WindowsApi.SafeHandles;

namespace Wanderer.Library.WindowsApi.Synchronization
{
    /// <summary>
    /// Synchronization Windows API functions.
    /// </summary>
    internal static class NativeMethods
    {
        private const string Kernel32 = "kernel32.dll";

        /// <summary>
        /// Infinite wait timeout value.
        /// </summary>
        public const uint InfiniteTimeout = 0xFFFFFFFF;

        #region WaitForSingleObject
        public static bool WaitForSingleObject(SafeTokenHandle tokenHandle, uint milliseconds = InfiniteTimeout)
        {
            Contract.Requires<ArgumentNullException>(tokenHandle != null, "tokenHandle cannot be null");

            var waitResult = (WaitResult) WaitForSingleObject(tokenHandle.DangerousGetHandle(), milliseconds);

            if (waitResult == WaitResult.Failed) {
                WindowsApi.NativeMethods.ReportWin32Exception();
            }

            return waitResult != WaitResult.Timeout;
        }

        [DllImport(Kernel32, SetLastError = true)]
        private static extern uint WaitForSingleObject(
            [In] IntPtr hHandle,
            [In] uint dwMilliseconds);
        #endregion
    }
}