using System;
using System.Diagnostics.Contracts;
using System.Security.Permissions;

using Wanderer.Library.WindowsApi.SafeHandles;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Internal implementation for <see cref="IProcess"/> interface.
    /// </summary>
    internal sealed class InternalProcess : IProcess
    {
        private readonly SafeTokenHandle _processHandle;

        private bool _exited;
        private uint _exitCode;

        #region IProcess implementation
        /// <summary>
        /// Gets the value that the associated process specified when it terminated.
        /// </summary>
        public uint ExitCode { get { return HasExited ? _exitCode : NativeConstants.StillActive; } }

        /// <summary>
        /// Gets a value indicating whether the associated process has been terminated.
        /// </summary>
        public bool HasExited
        {
            get
            {
                if (!_exited) {
                    if (Synchronization.NativeMethods.WaitForSingleObject(_processHandle, 0)) {
                        var exitCode = NativeMethods.GetExitCodeProcess(_processHandle);

                        if (exitCode != NativeConstants.StillActive) {
                            _exited = true;
                            _exitCode = exitCode;
                        }
                    }
                }

                return _exited;
            }
        }

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Wait indefinitely for the associated process to exit.
        /// </summary>
        public void WaitForExit()
        {
            WaitForExit(Synchronization.NativeMethods.InfiniteTimeout);
        }

        /// <summary>
        /// wait the specified number of milliseconds for the associated process to exit.
        /// </summary>
        /// <param name="milliseconds">
        /// The amount of time, in milliseconds, to wait for the associated process to exit.
        /// The maximum is the largest possible value of a 32-bit integer, which represents infinity to the operating system
        /// </param>
        /// <returns>true if the associated process has exited; otherwise, false</returns>
        public bool WaitForExit(uint milliseconds)
        {
            return _exited || Synchronization.NativeMethods.WaitForSingleObject(_processHandle, milliseconds);
        }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (IsDisposed) {
                return;
            }

            if (disposing) {
                _processHandle.Dispose();
            }

            IsDisposed = true;
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="processHandle">process handle</param>
        public InternalProcess(SafeTokenHandle processHandle)
        {
            Contract.Requires<ArgumentNullException>(processHandle != null, "processHandle cannot be null");
            Contract.Ensures(_processHandle != null);

            _processHandle = processHandle;
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~InternalProcess()
        {
            Dispose(false);
        }
        #endregion
    }
}