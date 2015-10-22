using System;

namespace Wanderer.Library.WindowsApi
{
    [Flags]
    // TODO: Fix functions references
    internal enum ProcessAccess : uint
    {
        /// <summary>All possible access rights for a process object.</summary>
        AllAccess = CreateThread | DuplicateHandle | QueryInformation | SetInformation | Terminate | VmOperation | VmRead | VmWrite | Synchronize,
        /// <summary>Required to create a thread.</summary>
        CreateThread = 0x002,
        /// <summary>Required to duplicate a handle using <see cref="NativeMethods.DuplicateHandle"/>.</summary>
        DuplicateHandle = 0x040,
        /// <summary>Required to retrieve certain information about a process, such as its token, exit code, and priority class (<see cref="NativeMethods.OpenProcessToken"/>).</summary>
        QueryInformation = 0x400,
        /// <summary>Required to set certain information about a process, such as its priority class (<see cref="NativeMethods.SetPriorityClass"/>).</summary>
        SetInformation = 0x200,
        /// <summary>Required to suspend or resume a process.</summary>
        SuspendResume = 0x800,
        /// <summary>Required to terminate a process using <see cref="NativeMethods.TerminateProcess"/>.</summary>
        Terminate = 0x001,
        /// <summary>Required to perform an operation on the address space of a process (<see cref="NativeMethods.VirtualProtectEx"/> and <see cref="NativeMethods.WriteProcessMemory"/>).</summary>
        VmOperation = 0x008,
        /// <summary>Required to read memory in a process using <see cref="NativeMethods.ReadProcessMemory"/>.</summary>
        VmRead = 0x010,
        /// <summary>Required to write to memory in a process using <see cref="NativeMethods.WriteProcessMemory"/>.</summary>
        VmWrite = 0x020,
        /// <summary>Required to wait for the process to terminate using the wait functions.</summary>
        Synchronize = 0x100000
    }
}