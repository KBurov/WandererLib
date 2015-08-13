using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Contains code contracts definition for interface <see cref="IProcessExtended" />.
    /// </summary>
    [ContractClassFor(typeof (IProcessExtended))]
// ReSharper disable InconsistentNaming
    internal abstract class IProcessExtendedContract : IProcessExtended
// ReSharper restore InconsistentNaming
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