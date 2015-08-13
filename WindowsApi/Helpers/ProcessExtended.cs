using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Security;
using System.Security.Permissions;
using System.Threading;

using Wanderer.Library.WindowsApi.Nt;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Provides additonal functionality for process, for example resume/suspend.
    /// </summary>
    [SecurityCritical]
    public sealed class ProcessExtended : IProcessExtended
    {
        private const string ResumeSuspendErrorMessage = "An error occured during the execution of resume/suspend the process (process id: {0}).";

        #region Variables
        private long _totalProcessorTime;
        private DateTime _totalProcessorTimeUpdate;
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

            Process = Process.GetProcessById(processId);

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

            Process = process;

            ResetCpuUsage();
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
                throw new ApplicationException(string.Format(ResumeSuspendErrorMessage, Process.Id));
            }
        }
    }
}