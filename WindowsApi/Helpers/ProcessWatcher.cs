﻿using System;
using System.Diagnostics.Contracts;
using System.Security.Permissions;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Helper class for process CPU clamping.
    /// </summary>
    public class ProcessWatcher : IProcessWatcher
    {
        #region IProcessWatcher implementation
        /// <summary>
        /// Controlled process.
        /// </summary>
        public IProcessExtended WatchedProcess { get; }

        /// <summary>
        /// Maximum avaliable CPU usage for the process.
        /// </summary>
        public uint MaxCpuUsage { get; }

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// <see cref="IDisposable"/> interface implementation.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Internal implementation of <see cref="IDisposable"/> interface.
        /// </summary>
        /// <param name="disposing">indicates that method called from public Dispose method</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) {
                return;
            }

            if (disposing) {
                WatchedProcess.Dispose();
            }

            IsDisposed = true;
        }
        #endregion

        #region IEquatable<IProcessWatcher> implemetation
        /// <summary>
        /// Indicates whether the current <see cref="IProcessWatcher"/> object is equal to another <see cref="IProcessWatcher"/> object.
        /// </summary>
        /// <param name="other">an <see cref="IProcessWatcher"/> to compare</param>
        /// <returns><b>true</b> if current object is equal to <i>other</i> parameter; otherwise, <b>false</b></returns>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public bool Equals(IProcessWatcher other)
        {
            return WatchedProcess.Process.Id == other?.WatchedProcess.Process.Id;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="processExtended">controlled process</param>
        /// <param name="maxCpuUsage">maximum avaliable CPU usage for the process</param>
        public ProcessWatcher(IProcessExtended processExtended, uint maxCpuUsage)
        {
            Contract.Requires<ArgumentNullException>(processExtended != null, "processExtended cannot be null");
            Contract.Ensures(WatchedProcess != null);

            WatchedProcess = processExtended;
            MaxCpuUsage = maxCpuUsage;
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~ProcessWatcher()
        {
            Dispose(false);
        }
        #endregion

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">the object to compare with the current object</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false</returns>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public override bool Equals(object obj)
        {
            if (obj == null) {
                return false;
            }

            var processWatcher = obj as IProcessWatcher;

            return processWatcher != null && Equals(processWatcher);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="ProcessWatcher"/>.
        /// </summary>
        /// <returns>a hash code for the current <see cref="ProcessWatcher"/></returns>
        public override int GetHashCode()
        {
            return WatchedProcess.GetHashCode();
        }
    }
}