using System;
using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.Authorization
{
    /// <summary>
    /// Contains information about a set of privileges for an access token.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct TokenPrivileges
    {
        /// <summary>
        /// This must be set to the number of entries in the <see cref="Privileges"/> array.
        /// </summary>
        public UInt32 PrivilegeCount;
        /// <summary>
        /// Specifies an array of <see cref="LuidAndAttributes"/> structures.
        /// Each structure contains the <see cref="Luid"/> and attributes of a privilege.
        /// To get the name of the privilege associated with a <see cref="Luid"/>,
        /// call the <see cref="NativeMethods.LookupPrivilegeName"/> function,
        /// passing the address of the <see cref="Luid"/> as the value of the luid parameter.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = NativeConstants.AnySizeArray)]
        public LuidAndAttributes[] Privileges;
    }
}