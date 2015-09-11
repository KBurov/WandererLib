using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;

using Wanderer.Library.WindowsApi.SafeHandles;

namespace Wanderer.Library.WindowsApi.Authorization
{
    /// <summary>
    /// Authorization Windows API functions.
    /// </summary>
    internal static class NativeMethods
    {
        private const string Advapi32 = "advapi32.dll";

        private static readonly Dictionary<Privilege, Luid> _luid = new Dictionary<Privilege, Luid>();
        private static readonly ReaderWriterLockSlim _luidLock = new ReaderWriterLockSlim();

        public static string GetPrivilegeName(Privilege privilege)
        {
            switch (privilege) {
                case Privilege.AssignPrimaryToken:
                    return "SeAssignPrimaryTokenPrivilege";
                case Privilege.Audit:
                    return "SeAuditPrivilege";
                case Privilege.Backup:
                    return "SeBackupPrivilege";
                case Privilege.ChangeNotify:
                    return "SeChangeNotifyPrivilege";
                case Privilege.CreateGlobal:
                    return "SeCreateGlobalPrivilege";
                case Privilege.CreatePagefile:
                    return "SeCreatePagefilePrivilege";
                case Privilege.CreatePermanent:
                    return "SeCreatePermanentPrivilege";
                case Privilege.CreateSymbolicLink:
                    return "SeCreateSymbolicLinkPrivilege";
                case Privilege.CreateToken:
                    return "SeCreateTokenPrivilege";
                case Privilege.Debug:
                    return "SeDebugPrivilege";
                case Privilege.EnableDelegation:
                    return "SeEnableDelegationPrivilege";
                case Privilege.Impersonate:
                    return "SeImpersonatePrivilege";
                case Privilege.IncreaseBasePriority:
                    return "SeIncreaseBasePriorityPrivilege";
                case Privilege.IncreaseQuota:
                    return "SeIncreaseQuotaPrivilege";
                case Privilege.IncreaseWorkingSet:
                    return "SeIncreaseWorkingSetPrivilege";
                case Privilege.LoadDriver:
                    return "SeLoadDriverPrivilege";
                case Privilege.LockMemory:
                    return "SeLockMemoryPrivilege";
                case Privilege.MachineAccount:
                    return "SeMachineAccountPrivilege";
                case Privilege.ManageVolume:
                    return "SeManageVolumePrivilege";
                case Privilege.ProfileSingleProcess:
                    return "SeProfileSingleProcessPrivilege";
                case Privilege.Relabel:
                    return "SeRelabelPrivilege";
                case Privilege.RemoteShutdown:
                    return "SeRemoteShutdownPrivilege";
                case Privilege.Restore:
                    return "SeRestorePrivilege";
                case Privilege.Security:
                    return "SeSecurityPrivilege";
                case Privilege.Shutdown:
                    return "SeShutdownPrivilege";
                case Privilege.SyncAgent:
                    return "SeSyncAgentPrivilege";
                case Privilege.SystemEnvironment:
                    return "SeSystemEnvironmentPrivilege";
                case Privilege.SystemProfile:
                    return "SeSystemProfilePrivilege";
                case Privilege.Systemtime:
                    return "SeSystemtimePrivilege";
                case Privilege.TakeOwnership:
                    return "SeTakeOwnershipPrivilege";
                case Privilege.Tcb:
                    return "SeTcbPrivilege";
                case Privilege.TimeZone:
                    return "SeTimeZonePrivilege";
                case Privilege.TrustedCredManAccess:
                    return "SeTrustedCredManAccessPrivilege";
                case Privilege.Undock:
                    return "SeUndockPrivilege";
                case Privilege.UnsolicitedInput:
                    return "SeUnsolicitedInputPrivilege";
                default:
                    throw new ArgumentException("Unknown parameter privilege value");
            }
        }

        public static Luid LuidFromPrivilege(Privilege privilege)
        {
            Luid result;

            result.LowPart = 0;
            result.HighPart = 0;

            using (_luidLock.GetUpgradeableReadLock()) {
                if (_luid.ContainsKey(privilege)) {
                    result = _luid[privilege];
                }
                else {
                    using (_luidLock.GetWriteLock()) {
                        LookupPrivilegeValue(privilege, out result);

                        _luid[privilege] = result;
                    }
                }
            }

            return result;
        }

        private static Privilege PrivilegeFromLuid(Luid luid)
        {
            using (_luidLock.GetReadLock()) {
                return _luid.First(kv => kv.Value.LowPart == luid.LowPart && kv.Value.HighPart == luid.HighPart).Key;
            }
        }

        #region AdjustTokenPrivileges
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke")]
        public static void AdjustTokenPrivileges(SafeTokenHandle tokenHandle, bool disableAllPrivileges, ref TokenPrivileges newState)
        {
            Contract.Requires<ArgumentNullException>(tokenHandle != null, "tokenHandle cannot be null");

            if (!AdjustTokenPrivileges(tokenHandle.DangerousGetHandle(), disableAllPrivileges, ref newState, 0u, IntPtr.Zero, IntPtr.Zero)) {
                WindowsApi.NativeMethods.ReportWin32Exception();
            }

            var error = Marshal.GetLastWin32Error();

            if (error == ErrorCodes.NotAllAssigned) {
                try {
                    throw new PrivilegeNotHeldException(GetPrivilegeName(PrivilegeFromLuid(newState.Privileges[0].Luid)));
                }
                catch (InvalidOperationException) {
                    throw new PrivilegeNotHeldException();
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke")]
        public static void AdjustTokenPrivileges(SafeTokenHandle tokenHandle, bool disableAllPrivileges, ref TokenPrivileges newState,
                                                 ref TokenPrivileges previousState)
        {
            Contract.Requires<ArgumentNullException>(tokenHandle != null, "tokenHandle cannot be null");

            uint temp;

            if (!AdjustTokenPrivileges(tokenHandle.DangerousGetHandle(),
                disableAllPrivileges, ref newState,
                (uint) Marshal.SizeOf(previousState), ref previousState, out temp)) {
                WindowsApi.NativeMethods.ReportWin32Exception();
            }

            var error = Marshal.GetLastWin32Error();

            if (error == ErrorCodes.NotAllAssigned) {
                try {
                    throw new PrivilegeNotHeldException(GetPrivilegeName(PrivilegeFromLuid(newState.Privileges[0].Luid)));
                }
                catch (InvalidOperationException) {
                    throw new PrivilegeNotHeldException();
                }
            }
        }

        [DllImport(Advapi32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AdjustTokenPrivileges(
            [In] IntPtr tokenHandle,
            [In, MarshalAs(UnmanagedType.Bool)] bool disableAllPrivileges,
            [In] ref TokenPrivileges newState,
            [In] UInt32 bufferLengthInBytes,
            [In, Out] ref TokenPrivileges previousState,
            [Out] out UInt32 returnLengthInBytes);

        [DllImport(Advapi32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool AdjustTokenPrivileges(
            [In] IntPtr tokenHandle,
            [In, MarshalAs(UnmanagedType.Bool)] bool disableAllPrivileges,
            [In] ref TokenPrivileges newState,
            [In] UInt32 bufferLengthInBytes,
            [Out] IntPtr previousState,
            [Out] IntPtr returnLengthInBytes);
        #endregion

        #region DuplicateTokenEx
        public static SafeTokenHandle DuplicateTokenEx(SafeTokenHandle existingToken, System.Security.Principal.TokenAccessLevels desiredAccess,
                                                       System.Security.Principal.TokenImpersonationLevel impersonationLevel, TokenType tokenType)
        {
            Contract.Requires<ArgumentNullException>(existingToken != null, "existingToken cannot be null");

            IntPtr token;

            if (!DuplicateTokenEx(existingToken.DangerousGetHandle(), (uint) desiredAccess, IntPtr.Zero, impersonationLevel, tokenType, out token)) {
                WindowsApi.NativeMethods.ReportWin32Exception();
            }

            return new SafeTokenHandle(token);
        }

        [DllImport(Advapi32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DuplicateTokenEx(
            [In] IntPtr hExistingToken,
            [In] uint dwDesiredAccess,
            [In, Optional] IntPtr lpTokenAttributes,
            // ReSharper disable InconsistentNaming
            [In] System.Security.Principal.TokenImpersonationLevel ImpersonationLevel,
            [In] TokenType TokenType,
            [Out] out IntPtr phNewToken);
        // ReSharper restore InconsistentNaming
        #endregion

        #region ImpersonateLoggedOnUser
        public static void ImpersonateLoggedOnUser(SafeTokenHandle tokenHandle)
        {
            Contract.Requires<ArgumentNullException>(tokenHandle != null, "tokenHandle cannot be null");

            if (!ImpersonateLoggedOnUser(tokenHandle.DangerousGetHandle())) {
                WindowsApi.NativeMethods.ReportWin32Exception();
            }
        }

        [DllImport(Advapi32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ImpersonateLoggedOnUser([In] IntPtr hToken);
        #endregion

        [DllImport(Advapi32, CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LookupPrivilegeName(
            [In] string lpSystemName,
            [In] ref Luid lpLuid,
            [Out] StringBuilder lpName,
            [In, Out] ref int cchName);

        #region LookupPrivilegeValue
        public static void LookupPrivilegeValue(Privilege privilege, out Luid luid)
        {
            LookupPrivilegeValue(null, privilege, out luid);
        }

        public static void LookupPrivilegeValue(string systemName, Privilege privilege, out Luid luid)
        {
            if (!LookupPrivilegeValue(systemName, GetPrivilegeName(privilege), out luid)) {
                WindowsApi.NativeMethods.ReportWin32Exception();
            }
        }

        [DllImport(Advapi32, CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool LookupPrivilegeValue(
            [In] string lpSystemName,
            [In] string lpName,
            [Out] out Luid lpLuid);
        #endregion

        #region OpenProcessToken
        public static SafeTokenHandle OpenProcessToken(IntPtr processHandle, System.Security.Principal.TokenAccessLevels tokenAccess)
        {
            return OpenProcessToken(processHandle, (uint) tokenAccess);
        }

        public static SafeTokenHandle OpenProcessToken(IntPtr processHandle, uint desiredAccess)
        {
            IntPtr token;

            if (!OpenProcessToken(processHandle, desiredAccess, out token)) {
                WindowsApi.NativeMethods.ReportWin32Exception();
            }

            return new SafeTokenHandle(token);
        }

        [DllImport(Advapi32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(
            [In] IntPtr processHandle,
            [In] UInt32 desiredAccess,
            [Out] out IntPtr tokenHandle);
        #endregion

        [DllImport(Advapi32, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RevertToSelf();
    }
}