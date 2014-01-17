using System;
using System.Runtime.InteropServices;

using Wanderer.Library.WindowsApi.SafeHandles;

namespace Wanderer.Library.WindowsApi.Authentication
{
    /// <summary>
    /// Authentication Windows functions.
    /// </summary>
    internal static class NativeMethods
    {
        private const string Advapi32 = "advapi32.dll";

        #region LogonUser
        public static SafeTokenHandle LogonUser(string userName, string domain, string password, LogonType logonType, LogonProvider logonProvider)
        {
            IntPtr token;

            if (!LogonUser(userName, domain, password, (int)logonType, (int)logonProvider, out token))
            {
                WindowsApi.NativeMethods.ReportWin32Exception();
            }

            return new SafeTokenHandle(token);
        }

        [DllImport(Advapi32, SetLastError = true, CharSet = CharSet.Auto, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool LogonUser(
            [In] string lpszUsername,
            [In] string lpszDomain,
            [In] string lpszPassword,
            [In] int dwLogonType,
            [In] int dwLogonProvider,
            [Out] out IntPtr phToken);
        #endregion
    }
}