using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Security;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Provides additonal functionality for process.
    /// </summary>
    [ContractClass(typeof (IProcessExtendedContract))]
    [SecurityCritical]
    public interface IProcessExtended : IDisposable
    {
        /// <summary>
        /// Extended process.
        /// </summary>
        Process Process { get; }

        /// <summary>
        /// CPU usage in percent.
        /// This is cumulative value which calculated from the first time when <see cref="IProcessExtended" /> was created.
        /// Can be cleared by <see cref="ResetCpuUsage" /> method.
        /// </summary>
        uint CpuUsage { get; }

        /// <summary>
        /// Indicates that the process was suspended.
        /// </summary>
        bool IsSuspended { get; }

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Gets the value that the associated process specified when it terminated.
        /// </summary>
        uint ExitCode { get; }

        /// <summary>
        /// Gets a value indicating whether the associated process has been terminated.
        /// </summary>
        bool HasExited { get; }

        /// <summary>
        /// Reset CPU usage value and time.
        /// </summary>
        void ResetCpuUsage();

        /// <summary>
        /// Resume the process.
        /// </summary>
        void Resume();

        /// <summary>
        /// Suspend the process.
        /// </summary>
        void Suspend();

        /// <summary>
        /// Wait indefinitely for the associated process to exit.
        /// </summary>
        void WaitForExit();

        /// <summary>
        /// wait the specified number of milliseconds for the associated process to exit.
        /// </summary>
        /// <param name="milliseconds">
        /// The amount of time, in milliseconds, to wait for the associated process to exit.
        /// The maximum is the largest possible value of a 32-bit unsigned integer, which represents infinity to the operating system
        /// </param>
        /// <returns>true if the associated process has exited; otherwise, false</returns>
        bool WaitForExit(uint milliseconds);
    }
}