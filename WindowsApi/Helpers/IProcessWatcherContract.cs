﻿using System;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.WindowsApi.Helpers
{
    [ContractClassFor(typeof (IProcessWatcher))]
// ReSharper disable InconsistentNaming
    internal abstract class IProcessWatcherContract : IProcessWatcher
// ReSharper restore InconsistentNaming
    {
        private const string ObjectDisposedExceptionMessage = "IProcessWatcher already disposed";

        #region IProcessWatcher implementation
        /// <summary>
        /// Controlled process.
        /// </summary>
        IProcessExtended IProcessWatcher.WatchedProcess
        {
            get
            {
                Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

                return default(IProcessExtended);
            }
        }

        /// <summary>
        /// Maximum avaliable CPU usage for the process.
        /// </summary>
        uint IProcessWatcher.MaxCpuUsage { get { return default(uint); } }

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        public bool IsDisposed { get { return default(bool); } }
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

        #region IEquatable<IProcessWatcher> implemetation
        bool IEquatable<IProcessWatcher>.Equals(IProcessWatcher other)
        {
            return default(bool);
        }
        #endregion
    }
}