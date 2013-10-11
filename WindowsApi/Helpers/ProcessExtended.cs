using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Provides additonal functionality for process, for example resume/suspend.
    /// </summary>
    [SecurityCritical]
    public class ProcessExtended : IProcessExtended
    {
        private const string ObjectDisposedExceptionMessage = "IProcessExtended already disposed";
        private const string ResumeSuspendErrorMessage = "An error occured during the execution of resume/suspend the process (process id: {0}).";

        #region Variables
        private readonly Process _process;

        private long _totalProcessorTime;
        private DateTime _totalProcessorTimeUpdate;
        #endregion

        #region IProcessExtended implementation
        /// <summary>
        /// Extended process.
        /// </summary>
        public Process Process
        {
            get
            {
                Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

                return _process;
            }
        }

        /// <summary>
        /// CPU usage in percent.
        /// This is cumulative value which calculated from the first time when <see cref="IProcessExtended"/> was created.
        /// Can be cleared by <see cref="ResetCpuUsage"/> method.
        /// </summary>
        public uint CpuUsage
        {
            [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
            get
            {
                Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

                if (DateTime.Now.Ticks == _totalProcessorTimeUpdate.Ticks) {
                    Thread.Sleep(10);
                }

                var currentTotalProcessTime = _process.TotalProcessorTime.Ticks;
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
        /// Reset CPU usage value and time.
        /// </summary>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public void ResetCpuUsage()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);

            _totalProcessorTime = _process.TotalProcessorTime.Ticks;
            _totalProcessorTimeUpdate = DateTime.Now;
        }

        /// <summary>
        /// Resume the process.
        /// </summary>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public void Resume()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);
            Contract.Ensures(!IsSuspended);

            ExecuteResumeSuspend(Nt.NativeMethods.NtResumeProcess);

            IsSuspended = false;
        }

        /// <summary>
        /// Suspend the process.
        /// </summary>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public void Suspend()
        {
            Contract.Requires<ObjectDisposedException>(!IsDisposed, ObjectDisposedExceptionMessage);
            Contract.Ensures(IsSuspended);

            ExecuteResumeSuspend(Nt.NativeMethods.NtSuspendProcess);

            IsSuspended = true;
        }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// <see cref="IDisposable"/> interface implementation.
        /// </summary>
        public void Dispose()
        {
            Contract.Ensures(IsDisposed);

            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        public bool IsDisposed { get; private set; }

        #region Constructors
        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="processId">id of extended process</param>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public ProcessExtended(int processId)
        {
            Contract.Ensures(_process != null);

            _process = Process.GetProcessById(processId);

            ResetCpuUsage();
        }

        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="process">extended process</param>
        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        public ProcessExtended(Process process)
        {
            Contract.Requires<ArgumentNullException>(process != null);
            Contract.Ensures(_process != null);

            _process = process;

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

        /// <summary>
        /// Internal implementation of <see cref="IDisposable"/> interface.
        /// </summary>
        /// <param name="disposing">indicates that method called from public Dispose method</param>
        protected virtual void Dispose(bool disposing)
        {
            Contract.Ensures(IsDisposed);

            if (IsDisposed) {
                return;
            }

            if (disposing) {
                _process.Dispose();
            }

            IsDisposed = true;
        }

        [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust"), PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
        private void ExecuteResumeSuspend(Func<IntPtr, Nt.NtStatus> func)
        {
            var result = func(_process.Handle);

            if (result != Nt.NtStatus.Success) {
                throw new ApplicationException(string.Format(ResumeSuspendErrorMessage, _process.Id));
            }
        }
    }
}