using System;

namespace Wanderer.Library.WindowsApi.Authorization
{
    /// <summary>
    /// Contains different access rights.
    /// </summary>
    internal static class AccessRights
    {
        /// <summary>
        /// Controls the ability to get or set the SACL in an object's security descriptor.
        /// The system grants this access right only if the <see cref="Privilege.Security"/> privilege is enabled
        /// in the access token of the requesting thread.
        /// </summary>
        public const uint AccessSystemSecurity = 0x01000000u;
        /// <summary>
        /// Determines all access rights.
        /// </summary>
        public const uint MaximumAllowed = 0x02000000u;
        /// <summary>
        /// Combines <see cref="Standard.Delete"/>, <see cref="Standard.ReadControl"/>,
        /// <see cref="Standard.WriteDac"/> and <see cref="Standard.WriteOwner"/> access.
        /// </summary>
        public const uint StandardRightsRequired = (uint) (Standard.Delete | Standard.ReadControl | Standard.WriteDac | Standard.WriteOwner);
        /// <summary>
        /// Currently defined to equal <see cref="Standard.ReadControl"/>.
        /// </summary>
        public const uint StandardRightsRead = (uint) Standard.ReadControl;
        /// <summary>
        /// Currently defined to equal <see cref="Standard.ReadControl"/>.
        /// </summary>
        public const uint StandardRightsWrite = (uint) Standard.ReadControl;
        /// <summary>
        /// Currently defined to equal <see cref="Standard.ReadControl"/>.
        /// </summary>
        public const uint StandardRightsExecute = (uint) Standard.ReadControl;
        /// <summary>
        /// Combines <see cref="Standard.Delete"/>, <see cref="Standard.ReadControl"/>, <see cref="Standard.WriteDac"/>,
        /// <see cref="Standard.WriteOwner"/> and <see cref="Standard.Synchronize"/> access.
        /// </summary>
        public const uint StandardRightsAll =
            (uint) (Standard.Delete | Standard.ReadControl | Standard.WriteDac | Standard.WriteOwner | Standard.Synchronize);

        /// <summary>
        /// File access rights.
        /// </summary>
        [Flags]
        public enum File : uint
        {
            /// <summary>
            /// For a file object, the right to read the corresponding file data.
            /// For a directory object, the right to read the corresponding directory data.
            /// </summary>
            ReadData = 0x0001,
            /// <summary>
            /// For a directory, the right to list the contents of the directory.
            /// </summary>
            ListDirectory = 0x0001,
            /// <summary>
            /// For a file object, the right to write data to the file.
            /// For a directory object, the right to create a file in the directory (<see cref="AddFile"/>).
            /// </summary>
            WriteData = 0x0002,
            /// <summary>
            /// For a directory, the right to create a file in the directory.
            /// </summary>
            AddFile = 0x0002,
            /// <summary>
            /// For a file object, the right to append data to the file.
            /// (For local files, write operations will not overwrite
            /// existing data if this flag is specified without <see cref="WriteData"/>.)
            /// For a directory object, the right to create a subdirectory (<see cref="AddSubdirectory"/>).
            /// </summary>
            AppendData = 0x0004,
            /// <summary>
            /// For a directory, the right to create a subdirectory.
            /// </summary>
            AddSubdirectory = 0x0004,
            /// <summary>
            /// For a named pipe, the right to create a pipe.
            /// </summary>
            CreatePipeInstance = 0x0004,
            /// <summary>
            /// The right to read extended file attributes.
            /// </summary>
            // ReSharper disable once InconsistentNaming
            ReadEA = 0x0008,
            /// <summary>
            /// The right to write extended file attributes.
            /// </summary>
            // ReSharper disable once InconsistentNaming
            WriteEA = 0x0010,
            /// <summary>
            /// For a native code file, the right to execute the file.
            /// This access right given to scripts may cause the script to be executable,
            /// depending on the script interpreter.
            /// </summary>
            Execute = 0x0020,
            /// <summary>
            /// For a directory, the right to traverse the directory.
            /// By default, users are assigned the BYPASS_TRAVERSE_CHECKING privilege (<see cref="Privilege.ChangeNotify"/>),
            /// which ignores the Traverse access right.
            /// </summary>
            Traverse = 0x0020,
            /// <summary>
            /// For a directory, the right to delete a directory and all the files it contains,
            /// including read-only files.
            /// </summary>
            DeleteChild = 0x0040,
            /// <summary>
            /// The right to read file attributes.
            /// </summary>
            ReadAttributes = 0x0080,
            /// <summary>
            /// The right to write file attributes.
            /// </summary>
            WriteAttributes = 0x0100,
            /// <summary>
            /// All possible access rights for a file.
            /// </summary>
            AllAccess = (StandardRightsRequired | Standard.Synchronize | 0x1FF)
        }

        /// <summary>
        /// Generic file access rights.
        /// </summary>
        [Flags]
        public enum FileGeneric : uint
        {
            Read = (StandardRightsRead | File.ReadData | File.ReadAttributes | File.ReadEA | Standard.Synchronize),
            Write = (StandardRightsWrite | File.WriteData | File.WriteAttributes | File.WriteEA | File.AppendData | Standard.Synchronize),
            Execute = (StandardRightsExecute | File.ReadAttributes | File.Execute | Standard.Synchronize)
        }

        /// <summary>
        /// Generic access rights.
        /// </summary>
        [Flags]
        public enum Generic : uint
        {
            /// <summary>
            /// Read access.
            /// </summary>
            Read = 0x80000000,
            /// <summary>
            /// Write access.
            /// </summary>
            Write = 0x40000000,
            /// <summary>
            /// Execute access.
            /// </summary>
            Execute = 0x20000000,
            /// <summary>
            /// All possible access rights.
            /// </summary>
            All = 0x10000000
        }

        /// <summary>
        /// Standard access rights.
        /// </summary>
        [Flags]
        public enum Standard : uint
        {
            /// <summary>
            /// The right to delete the object.
            /// </summary>
            Delete = 0x00010000,
            /// <summary>
            /// The right to read the information in the object's security descriptor,
            /// not including the information in the system access control list (SACL).
            /// </summary>
            ReadControl = 0x00020000,
            /// <summary>
            /// The right to modify the discretionary access control list (DACL)
            /// in the object's security descriptor.
            /// </summary>
            WriteDac = 0x00040000,
            /// <summary>
            /// The right to change the owner in the object's security descriptor.
            /// </summary>
            WriteOwner = 0x00080000,
            /// <summary>
            /// The right to use the object for synchronization.
            /// This enables a thread to wait until the object is in the signaled state.
            /// Some object types do not support this access right.
            /// </summary>
            Synchronize = 0x00100000
        }

        /// <summary>
        /// Service Control Manager access rights.
        /// </summary>
        [Flags]
        public enum ServiceControlManager : uint
        {
            /// <summary>
            /// Required to connect to the service control manager.
            /// </summary>
            Connect = 0x0001,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the CreateService function to create a service object and add it to the database.
            /// </summary>
            CreateService = 0x0002,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the EnumServicesStatus or EnumServicesStatusEx function to list the services that are in the database.
            /// Required to call the NotifyServiceStatusChange function to receive notification when any service is created or deleted.
            /// </summary>
            EnumerateService = 0x0004,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the LockServiceDatabase function to acquire a lock on the database.
            /// </summary>
            Lock = 0x0008,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the QueryServiceLockStatus function to retrieve the lock status information for the database.
            /// </summary>
            QueryLockStatus = 0x0010,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the NotifyBootConfigStatus function.
            /// </summary>
            ModifyBootConfig = 0x0020,
            /// <summary>
            /// Includes <see cref="StandardRightsRequired"/>, in addition to all access rights in this table.
            /// </summary>
            AllAccess = (StandardRightsRequired | Connect | CreateService | EnumerateService | Lock | QueryLockStatus | ModifyBootConfig)
        }

        /// <summary>
        /// Service access rights.
        /// </summary>
        [Flags]
        public enum Service : uint
        {
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the QueryServiceConfig and QueryServiceConfig2 functions to query the service configuration.
            /// </summary>
            QueryConfig = 0x0001,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the <see cref="Services.NativeMethods.ChangeServiceConfig(SafeServiceHandle,ServiceTypeFull,ServiceStartModeFull,ServiceErrorControlType,string,string,string,string,string,string)"/>
            /// or ChangeServiceConfig2 function to change the service configuration.
            /// Because this grants the caller the right to change the executable file that the system runs,
            /// it should be granted only to administrators.
            /// </summary>
            ChangeConfig = 0x0002,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the QueryServiceStatus or QueryServiceStatusEx function to ask the service control manager about the status of the service.
            /// Required to call the NotifyServiceStatusChange function to receive notification when a service changes status.
            /// </summary>
            QueryStatus = 0x0004,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the EnumDependentServices function to enumerate all the services dependent on the service.
            /// </summary>
            EnumerateDependents = 0x0008,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the StartService function to start the service.
            /// </summary>
            Start = 0x0010,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the ControlService function to stop the service.
            /// </summary>
            Stop = 0x0020,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the ControlService function to pause or continue the service.
            /// </summary>
            PauseContinue = 0x0040,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the ControlService function to ask the service to report its status immediately.
            /// </summary>
            Interrogate = 0x0080,
            // TODO: Fix function reference
            /// <summary>
            /// Required to call the ControlService function to specify a user-defined control code.
            /// </summary>
            UserDefinedControl = 0x0100,
            /// <summary>
            /// Includes <see cref="StandardRightsRequired"/> in addition to all access rights in this table.
            /// </summary>
            AllAccess =
                (StandardRightsRequired | QueryConfig | ChangeConfig | QueryStatus | EnumerateDependents | Start | Stop | PauseContinue |
                 Interrogate | UserDefinedControl)
        }
    }
}