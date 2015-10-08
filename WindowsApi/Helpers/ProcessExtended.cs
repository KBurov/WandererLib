using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading;

using Wanderer.Library.WindowsApi.Nt;
using Wanderer.Library.WindowsApi.SafeHandles;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Provides additonal functionality for process, for example resume/suspend.
    /// </summary>
    [SecurityCritical]
    public sealed class ProcessExtended : IProcessExtended
    {
        #region Variables
        private readonly SafeTokenHandle _processHandle;
        private readonly bool _initializedByProcessHandle;

        private long _totalProcessorTime;
        private DateTime _totalProcessorTimeUpdate;
        private bool _exited;
        private uint _exitCode;
        #endregion

        #region IProcessExtended implementation
        /// <summary>
        /// Extended process.
        /// </summary>
        public Process Process { get; }

        /// <summary>
        /// CPU usage in percent.
        /// This is cumulative value which calculated from the first time when <see cref="IProcessExtended" /> was created.
        /// Can be cleared by <see cref="ResetCpuUsage" /> method.
        /// </summary>
        public uint CpuUsage
        {
            [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
            get
            {
                if (DateTime.Now.Ticks == _totalProcessorTimeUpdate.Ticks) {
                    Thread.Sleep(10);
                }

                var currentTotalProcessTime = Process.TotalProcessorTime.Ticks;
                var updateTime = DateTime.Now;
                var usedTotalProcessTime = currentTotalProcessTime - _totalProcessorTime;
                var updateDelay = updateTime.Ticks - _totalProcessorTimeUpdate.Ticks;

                return (uint) (usedTotalProcessTime * 100 / updateDelay / Environment.ProcessorCount);
            }
        }

        /// <summary>
        /// Indicates that the process was suspended.
        /// </summary>
        public bool IsSuspended { get; private set; }

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Gets the value that the associated process specified when it terminated.
        /// </summary>
        public uint ExitCode => HasExited ? (_initializedByProcessHandle ? _exitCode : (uint) Process.ExitCode) : NativeConstants.StillActive;

        /// <summary>
        /// Gets a value indicating whether the associated process has been terminated.
        /// </summary>
        public bool HasExited
        {
            get
            {
                if (_initializedByProcessHandle) {
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
                else {
                    return Process.HasExited;
                }
            }
        }

        /// <summary>
        /// Reset CPU usage value and time.
        /// </summary>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public void ResetCpuUsage()
        {
            _totalProcessorTime = Process.TotalProcessorTime.Ticks;
            _totalProcessorTimeUpdate = DateTime.Now;
        }

        /// <summary>
        /// Resume the process.
        /// </summary>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public void Resume()
        {
            ExecuteResumeSuspend(Nt.NativeMethods.NtResumeProcess);

            IsSuspended = false;
        }

        /// <summary>
        /// Suspend the process.
        /// </summary>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public void Suspend()
        {
            ExecuteResumeSuspend(Nt.NativeMethods.NtSuspendProcess);

            IsSuspended = true;
        }

        /// <summary>
        /// Wait indefinitely for the associated process to exit.
        /// </summary>
        public void WaitForExit()
        {
            if (_initializedByProcessHandle) {
                WaitForExit(Synchronization.NativeMethods.InfiniteTimeout);
            }
            else {
                Process.WaitForExit();
            }
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
            return _initializedByProcessHandle
                ? (_exited || Synchronization.NativeMethods.WaitForSingleObject(_processHandle, milliseconds))
                : Process.WaitForExit((int) milliseconds);
        }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// <see cref="IDisposable" /> interface implementation.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Internal implementation of <see cref="IDisposable" /> interface.
        /// </summary>
        /// <param name="disposing">indicates that method called from public Dispose method</param>
        private void Dispose(bool disposing)
        {
            if (IsDisposed) {
                return;
            }

            if (disposing) {
                _processHandle.Dispose();

                Process.Dispose();
            }

            IsDisposed = true;
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="processId">id of extended process</param>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public ProcessExtended(int processId)
        {
            Contract.Ensures(Process != null);
            Contract.Ensures(_processHandle != null);

            Process = Process.GetProcessById(processId);
            _processHandle = new SafeTokenHandle(Process.Handle, false);

            _initializedByProcessHandle = false;

            ResetCpuUsage();
        }

        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="process">extended process</param>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public ProcessExtended(Process process)
        {
            Contract.Requires<ArgumentNullException>(process != null, "process cannot be null");
            Contract.Ensures(Process != null);
            Contract.Ensures(_processHandle != null);

            Process = process;
            _processHandle = new SafeTokenHandle(Process.Handle, false);

            _initializedByProcessHandle = false;

            ResetCpuUsage();
        }

        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="processHandle">process handle</param>
        public ProcessExtended(SafeTokenHandle processHandle)
        {
            Contract.Requires<ArgumentNullException>(processHandle != null, "processHandle cannot be null");
            Contract.Ensures(Process != null);
            Contract.Ensures(_processHandle != null);

            var handle = processHandle.DangerousGetHandle();

            Process = Process.GetProcesses().Single(p => p.Id != 0 && p.Handle == handle);
            _processHandle = processHandle;

            _initializedByProcessHandle = true;
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~ProcessExtended()
        {
            Dispose(false);
        }
        #endregion

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        private void ExecuteResumeSuspend(Func<IntPtr, NtStatus> func)
        {
            var result = func(Process.Handle);

            if (result != NtStatus.Success) {
                throw new ApplicationException($"An error occured during the execution of resume/suspend the process (process id: {Process.Id}).");
            }
        }
    }
}