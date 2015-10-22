using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Contains code contracts definition for interface <see cref="IProcessExtended" />.
    /// </summary>
    [ContractClassFor(typeof (IProcessExtended))]
    // ReSharper disable once InconsistentNaming
    internal abstract class IProcessExtendedContract : IProcessExtended
    {
        private const string ObjectDisposedExceptionMessage = "IProcessExtended already disposed";

        #region IProcessExtended implementation
        /// <summary>
        /// Extended process.
        /// </summary>
        Process IProcessExtended.Process
        {
            get
            {
                Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

                return default(Process);
            }
        }

        /// <summary>
        /// CPU usage in percent.
        /// This is cumulative value which calculated from the first time when <see cref="IProcessExtended" /> was created.
        /// Can be cleared by <see cref="IProcessExtended.ResetCpuUsage" /> method.
        /// </summary>
        uint IProcessExtended.CpuUsage
        {
            get
            {
                Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

                return default(uint);
            }
        }

        /// <summary>
        /// Indicates that the process was suspended.
        /// </summary>
        public bool IsSuspended => default(bool);

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        public bool IsDisposed => default(bool);

        /// <summary>
        /// Gets the value that the associated process specified when it terminated.
        /// </summary>
        uint IProcessExtended.ExitCode
        {
            get
            {
                Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

                return default(uint);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the associated process has been terminated.
        /// </summary>
        bool IProcessExtended.HasExited
        {
            get
            {
                Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

                return default(bool);
            }
        }

        /// <summary>
        /// Reset CPU usage value and time.
        /// </summary>
        void IProcessExtended.ResetCpuUsage()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);
        }

        /// <summary>
        /// Resume the process.
        /// </summary>
        void IProcessExtended.Resume()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);
            Contract.Ensures(!IsSuspended);
        }

        /// <summary>
        /// Suspend the process.
        /// </summary>
        void IProcessExtended.Suspend()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);
            Contract.Ensures(IsSuspended);
        }

        /// <summary>
        /// Wait indefinitely for the associated process to exit.
        /// </summary>
        void IProcessExtended.WaitForExit()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);
        }

        /// <summary>
        /// wait the specified number of milliseconds for the associated process to exit.
        /// </summary>
        /// <param name="milliseconds">
        /// The amount of time, in milliseconds, to wait for the associated process to exit.
        /// The maximum is the largest possible value of a 32-bit unsigned integer, which represents infinity to the operating system
        /// </param>
        /// <returns>true if the associated process has exited; otherwise, false</returns>
        bool IProcessExtended.WaitForExit(uint milliseconds)
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

            return default(bool);
        }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// <see cref="IDisposable"/> interface implementation.
        /// </summary>
        void IDisposable.Dispose() {}
        #endregion
    }
}