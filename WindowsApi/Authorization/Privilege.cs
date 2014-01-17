namespace Wanderer.Library.WindowsApi.Authorization
{
    /// <summary>
    /// List of available privileges.
    /// </summary>
    public enum Privilege
    {
        /// <summary>
        /// Required to assign the primary token of a process.
        /// User Right: Replace a process-level token.
        /// </summary>
        AssignPrimaryToken,
        /// <summary>
        /// Required to generate audit-log entries. Give this privilege to secure servers.
        /// User Right: Generate security audits.
        /// </summary>
        Audit,
        // TODO: Fix reference to corresponding functions
        /// <summary>
        /// Required to perform backup operations.
        /// This privilege causes the system to grant all read access control to any file,
        /// regardless of the access control list (ACL) specified for the file.
        /// Any access request other than read is still evaluated with the ACL.
        /// This privilege is required by the RegSaveKey and RegSaveKeyEx functions.
        /// The following access rights are granted if this privilege is held:
        /// <see cref="AccessRights.Standard.ReadControl"/>
        /// <see cref="AccessRights.AccessSystemSecurity"/>
        /// <see cref="AccessRights.FileGeneric.Read"/>
        /// <see cref="AccessRights.File.Traverse"/>
        /// User Right: Back up files and directories.
        /// </summary>
        Backup,
        /// <summary>
        /// Required to receive notifications of changes to files or directories.
        /// This privilege also causes the system to skip all traversal access checks.
        /// It is enabled by default for all users.
        /// User Right: Bypass traverse checking.
        /// </summary>
        ChangeNotify,
        /// <summary>
        /// Required to create named file mapping objects in the global namespace during Terminal Services sessions.
        /// This privilege is enabled by default for administrators, services, and the local system account.
        /// User Right: Create global objects.
        /// </summary>
        CreateGlobal,
        /// <summary>
        /// Required to create a paging file.
        /// User Right: Create a pagefile.
        /// </summary>
        CreatePagefile,
        /// <summary>
        /// Required to create a permanent object.
        /// User Right: Create permanent shared objects.
        /// </summary>
        CreatePermanent,
        /// <summary>
        /// Required to create a symbolic link.
        /// User Right: Create symbolic links.
        /// </summary>
        CreateSymbolicLink,
        /// <summary>
        /// Required to create a primary token.
        /// User Right: Create a token object.
        /// You cannot add this privilege to a user account with the "Create a token object" policy.
        /// Additionally, you cannot add this privilege to an owned process using Windows APIs.
        /// Windows Server 2003 and Windows XP with SP1 and earlier: 
        /// Windows APIs can add this privilege to an owned process.
        /// </summary>
        CreateToken,
        /// <summary>
        /// Required to debug and adjust the memory of a process owned by another account.
        /// User Right: Debug programs.
        /// </summary>
        Debug,
        /// <summary>
        /// Required to mark user and computer accounts as trusted for delegation.
        /// User Right: Enable computer and user accounts to be trusted for delegation.
        /// </summary>
        EnableDelegation,
        /// <summary>
        /// Required to impersonate.
        /// User Right: Impersonate a client after authentication.
        /// </summary>
        Impersonate,
        /// <summary>
        /// Required to increase the base priority of a process.
        /// User Right: Increase scheduling priority.
        /// </summary>
        IncreaseBasePriority,
        /// <summary>
        /// Required to increase the quota assigned to a process.
        /// User Right: Adjust memory quotas for a process.
        /// </summary>
        IncreaseQuota,
        /// <summary>
        /// Required to allocate more memory for applications that run in the context of users.
        /// User Right: Increase a process working set.
        /// </summary>
        IncreaseWorkingSet,
        /// <summary>
        /// Required to load or unload a device driver.
        /// User Right: Load and unload device drivers.
        /// </summary>
        LoadDriver,
        /// <summary>
        /// Required to lock physical pages in memory.
        /// User Right: Lock pages in memory.
        /// </summary>
        LockMemory,
        /// <summary>
        /// Required to create a computer account.
        /// User Right: Add workstations to domain.
        /// </summary>
        MachineAccount,
        /// <summary>
        /// Required to enable volume management privileges.
        /// User Right: Manage the files on a volume.
        /// </summary>
        ManageVolume,
        /// <summary>
        /// Required to gather profiling information for a single process.
        /// User Right: Profile single process.
        /// </summary>
        ProfileSingleProcess,
        /// <summary>
        /// Required to modify the mandatory integrity level of an object.
        /// User Right: Modify an object label.
        /// </summary>
        Relabel,
        /// <summary>
        /// Required to shut down a system using a network request.
        /// User Right: Force shutdown from a remote system.
        /// </summary>
        RemoteShutdown,
        /// <summary>
        /// Required to perform restore operations.
        /// This privilege causes the system to grant all write access control to any file,
        /// regardless of the ACL specified for the file.
        /// Any access request other than write is still evaluated with the ACL.
        /// Additionally, this privilege enables you to set any valid user or group SID as the owner of a file.
        /// This privilege is required by the RegLoadKey function.
        /// The following access rights are granted if this privilege is held:
        /// <see cref="AccessRights.Standard.WriteDac"/>
        /// <see cref="AccessRights.Standard.WriteOwner"/>
        /// <see cref="AccessRights.AccessSystemSecurity"/>
        /// <see cref="AccessRights.FileGeneric.Write"/>
        /// <see cref="AccessRights.File.AddFile"/>
        /// <see cref="AccessRights.File.AddSubdirectory"/>
        /// <see cref="AccessRights.Standard.Delete"/>
        /// User Right: Restore files and directories.
        /// </summary>
        Restore,
        /// <summary>
        /// Required to perform a number of security-related functions,
        /// such as controlling and viewing audit messages.
        /// This privilege identifies its holder as a security operator.
        /// User Right: Manage auditing and security log.
        /// </summary>
        Security,
        /// <summary>
        /// Required to shut down a local system.
        /// User Right: Shut down the system.
        /// </summary>
        Shutdown,
        /// <summary>
        /// Required for a domain controller to use the Lightweight Directory Access Protocol directory synchronization services.
        /// This privilege enables the holder to read all objects and properties in the directory,
        /// regardless of the protection on the objects and properties.
        /// By default, it is assigned to the Administrator and LocalSystem accounts on domain controllers.
        /// User Right: Synchronize directory service data.
        /// </summary>
        SyncAgent,
        /// <summary>
        /// Required to modify the nonvolatile RAM of systems that use this type of memory to store configuration information.
        /// User Right: Modify firmware environment values.
        /// </summary>
        SystemEnvironment,
        /// <summary>
        /// Required to gather profiling information for the entire system.
        /// User Right: Profile system performance.
        /// </summary>
        SystemProfile,
        /// <summary>
        /// Required to modify the system time.
        /// User Right: Change the system time.
        /// </summary>
        Systemtime,
        /// <summary>
        /// Required to take ownership of an object without being granted discretionary access.
        /// This privilege allows the owner value to be set only to those values that the holder may legitimately assign as the owner of an object.
        /// User Right: Take ownership of files or other objects.
        /// </summary>
        TakeOwnership,
        /// <summary>
        /// This privilege identifies its holder as part of the trusted computer base.
        /// Some trusted protected subsystems are granted this privilege.
        /// User Right: Act as part of the operating system.
        /// </summary>
        Tcb,
        /// <summary>
        /// Required to adjust the time zone associated with the computer's internal clock.
        /// User Right: Change the time zone.
        /// </summary>
        TimeZone,
        /// <summary>
        /// Required to access Credential Manager as a trusted caller.
        /// User Right: Access Credential Manager as a trusted caller.
        /// </summary>
        TrustedCredManAccess,
        /// <summary>
        /// Required to undock a laptop.
        /// User Right: Remove computer from docking station.
        /// </summary>
        Undock,
        /// <summary>
        /// Required to read unsolicited input from a terminal device.
        /// User Right: Not applicable.
        /// </summary>
        UnsolicitedInput
    }
}