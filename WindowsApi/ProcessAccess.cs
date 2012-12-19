using System;

namespace Wanderer.Library.WindowsApi
{
    [Flags]
    internal enum ProcessAccess : int
    {
        /// <summary>All possible access rights for a process object.</summary>
        AllAccess = CreateThread | DuplicateHandle | QueryInformation | SetInformation | Terminate | VMOperation | VMRead | VMWrite | Synchronize,
        /// <summary>Required to create a thread.</summary>
        CreateThread = 0x002,
        /// <summary>Required to duplicate a handle using <see cref="Functions.DuplicateHandle"/>.</summary>
        DuplicateHandle = 0x040,
        /// <summary>Required to retrieve certain information about a process, such as its token, exit code, and priority class (<see cref="Functions.OpenProcessToken"/>).</summary>
        QueryInformation = 0x400,
        /// <summary>Required to set certain information about a process, such as its priority class (<see cref="Functions.SetPriorityClass"/>).</summary>
        SetInformation = 0x200,
        /// <summary>Required to suspend or resume a process.</summary>
        SuspendResume = 0x800,
        /// <summary>Required to terminate a process using <see cref="Functions.TerminateProcess"/>.</summary>
        Terminate = 0x001,
        /// <summary>Required to perform an operation on the address space of a process (<see cref="Functions.VirtualProtectEx"/> and <see cref="Functions.WriteProcessMemory"/>).</summary>
        VMOperation = 0x008,
        /// <summary>Required to read memory in a process using <see cref="Functions.ReadProcessMemory"/>.</summary>
        VMRead = 0x010,
        /// <summary>Required to write to memory in a process using <see cref="Functions.WriteProcessMemory"/>.</summary>
        VMWrite = 0x020,
        /// <summary>Required to wait for the process to terminate using the wait functions.</summary>
        Synchronize = 0x100000
    }
}