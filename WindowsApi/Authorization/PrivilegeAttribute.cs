using System;

namespace Wanderer.Library.WindowsApi.Authorization
{
    /// <summary>
    /// Privilege attributes.
    /// </summary>
    [Flags]
    internal enum PrivilegeAttribute : uint
    {
        /// <summary>
        /// The privilege is enabled by default.
        /// </summary>
        EnabledByDefault = 0x00000001,
        /// <summary>
        /// The privilege is enabled.
        /// </summary>
        Enabled = 0x00000002,
        /// <summary>
        /// Used to remove a privilege.
        /// </summary>
        Removed = 0X00000004,
        /// <summary>
        /// The privilege was used to gain access to an object or service.
        /// This flag is used to identify the relevant privileges in a set
        /// passed by a client application that may contain unnecessary privileges.
        /// </summary>
        UsedForAccess = 0x80000000
    }
}