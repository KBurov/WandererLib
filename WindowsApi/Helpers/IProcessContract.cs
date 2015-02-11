using System;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Contains code contracts definition for interface <see cref="IProcess"/>.
    /// </summary>
    [ContractClassFor(typeof (IProcess))]
// ReSharper disable InconsistentNaming
    internal abstract class IProcessContract : IProcess
// ReSharper restore InconsistentNaming
    {
        private const string ObjectDisposedExceptionMessage = "IProcess already disposed";

        #region IProcess implementation
        /// <summary>
        /// Gets the value that the associated process specified when it terminated.
        /// </summary>
        uint IProcess.ExitCode
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
        bool IProcess.HasExited
        {
            get
            {
                Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

                return default(bool);
            }
        }

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        public bool IsDisposed { get { return default(bool); } }

        /// <summary>
        /// Wait indefinitely for the associated process to exit.
        /// </summary>
        void IProcess.WaitForExit()
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
        bool IProcess.WaitForExit(uint milliseconds)
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

            return default(bool);
        }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// <see cref="IDisposable"/> interface implementation.
        /// </summary>
        void IDisposable.Dispose()
        {
            Contract.Ensures(IsDisposed);
        }
        #endregion
    }
}