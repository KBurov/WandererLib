using System;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// Contains the filter flags to use for down-level scopes.
    /// </summary>
    [Flags]
    internal enum DSOPDownLevelFilter : uint
    {
        /// <summary>
        /// Includes user objects.
        /// </summary>
        Users = 0x80000001,
        /// <summary>
        /// Includes all local groups.
        /// </summary>
        LocalGroups = 0x80000002,
        /// <summary>
        /// Includes all global groups.
        /// </summary>
        GlobalGroups = 0x80000004,
        /// <summary>
        /// Includes computer objects.
        /// </summary>
        Computers = 0x80000008,
        /// <summary>
        /// Includes the well-known security principal "World (Everyone)", a group that includes all users.
        /// </summary>
        World = 0x80000010,
        /// <summary>
        /// Includes the well-known security principal "Authenticated User",
        /// a group that includes all authenticated accounts in the target domain and its trusted domains.
        /// </summary>
        AuthenticatedUser = 0x80000020,
        /// <summary>
        /// Includes the well-known security principal "Anonymous", which refers to null session logons.
        /// </summary>
        Anonymous = 0x80000040,
        /// <summary>
        /// Includes the well-known security principal "Batch", which refers to batch server logons.
        /// </summary>
        Batch = 0x80000080,
        /// <summary>
        /// Includes the well-known security principal "Creator Owner".
        /// </summary>
        CreatorOwner = 0x80000100,
        /// <summary>
        /// Includes the well-known security principal "Creator Group".
        /// </summary>
        CreatorGroup = 0x80000200,
        /// <summary>
        /// Includes the well-known security principal "Dialup".
        /// </summary>
        Dialup = 0x80000400,
        /// <summary>
        /// Includes the well-known security principal "Interactive", which refers to users who log on to interactively use the computer.
        /// </summary>
        Interactive = 0x80000800,
        /// <summary>
        /// Includes the well-known security principal "Network", which refers to network logons for high performance servers.
        /// </summary>
        Network = 0x80001000,
        /// <summary>
        /// Includes the well-known security principal "Service", which refers to Win32 service logons.
        /// </summary>
        Service = 0x80002000,
        /// <summary>
        /// Includes the well-known security principal "System", which refers to the LocalSystem account.
        /// </summary>
        System = 0x80004000,
        /// <summary>
        /// Excludes local built-in groups returned by groups' enumeration.
        /// </summary>
        ExcludeBuiltInGroups = 0x80008000,
        /// <summary>
        /// Includes the "Terminal Server" well-known security principal.
        /// </summary>
        TerminalServer = 0x80010000,
        /// <summary>
        /// Includes all well-known security principals.
        /// This flag is the same as specifying all of the well-known security principal flags listed in this table.
        /// This flag should be used for forward compatibility because it causes any other down-level,
        /// well-known SIDs that might be added in the future your code to automatically be included.
        /// </summary>
        AllWellKnownSIDs = 0x80020000,
        /// <summary>
        /// Includes the "Local Service" well-known security principal.
        /// </summary>
        LocalService = 0x80040000,
        /// <summary>
        /// Includes the "Network Service" well-known security principal.
        /// </summary>
        NetworkService = 0x80080000,
        /// <summary>
        /// Includes the "Remote Logon" well-known security principal.
        /// </summary>
        RemoteLogon = 0x80100000,
        /// <summary>
        /// Includes the "Internet User" well-known security principal.
        /// </summary>
        InternetUser = 0x80200000,
        /// <summary>
        /// Includes the "Owner Rights" well-known security principal.
        /// </summary>
        OwnerRights = 0x80400000,
        /// <summary>
        /// Includes "Service SIDs" of all installed services.
        /// </summary>
        Services = 0x80800000
    }
}