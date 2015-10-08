using System;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

using Wanderer.Library.WindowsApi.SafeHandles;

namespace Wanderer.Library.WindowsApi.Desktop
{
    /// <summary>
    /// Windows Station and Desktop Windows API functions.
    /// </summary>
    internal static class NativeMethods
    {
        private const string User32 = "user32.dll";

        [DllImport(User32, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseWindowStation([In] IntPtr hWinSta);

        [DllImport(User32, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern IntPtr GetProcessWindowStation();

        #region OpenWindowStation
        public static SafeWindowStationHandle OpenWindowStation(string windowStationName, uint desiredAccess, bool inherit = false)
        {
            var windowStationHandle = OpenWindowStation(windowStationName, inherit, desiredAccess);

            if (windowStationHandle == IntPtr.Zero)
            {
                WindowsApi.NativeMethods.ReportWin32Exception();
            }

            return new SafeWindowStationHandle(windowStationHandle);
        }

        [DllImport(User32, CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        private static extern IntPtr OpenWindowStation(
            [In] string lpszWinSta,
            [In, MarshalAs(UnmanagedType.Bool)] bool fInherit,
            [In] uint dwDesiredAccess);
        #endregion

        #region SetProcessWindowStation
        public static void SetProcessWindowStation(SafeWindowStationHandle windowStationHandle)
        {
            Contract.Requires<ArgumentNullException>(windowStationHandle != null, $"{nameof(windowStationHandle)} cannot be null");

            if (!SetProcessWindowStation(windowStationHandle.DangerousGetHandle()))
            {
                WindowsApi.NativeMethods.ReportWin32Exception();
            }
        }

        [DllImport(User32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetProcessWindowStation([In] IntPtr hWinSta);
        #endregion
    }
}