using System;
using System.Diagnostics.Contracts;
using System.Security;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Contains common information about an executed process.
    /// </summary>
    [ContractClass(typeof (IProcessContract))]
    [SecurityCritical]
    public interface IProcess : IDisposable
    {
        /// <summary>
        /// Gets the value that the associated process specified when it terminated.
        /// </summary>
        uint ExitCode { get; }

        /// <summary>
        /// Gets a value indicating whether the associated process has been terminated.
        /// </summary>
        bool HasExited { get; }

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        bool IsDisposed { get; }

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