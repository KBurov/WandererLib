using System;
using System.Diagnostics;
using System.Security;
using System.Threading;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Provides additonal functionality for process, for example resume/suspend.
    /// </summary>
    [SecurityCritical]
    public class ProcessExtended : IProcessExtended
    {
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
        public Process Process { get { return _process; } }

        /// <summary>
        /// CPU usage in percent.
        /// This is cumulative value which calculated from the first time when <see cref="IProcessExtended"/> was created.
        /// Can be cleared by <see cref="ResetCpuUsage"/> method.
        /// </summary>
        public uint CpuUsage
        {
            get
            {
                if (DateTime.Now.Ticks == _totalProcessorTimeUpdate.Ticks)
                    Thread.Sleep(10);

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
        public void ResetCpuUsage()
        {
            _totalProcessorTime = _process.TotalProcessorTime.Ticks;
            _totalProcessorTimeUpdate = DateTime.Now;
        }

        /// <summary>
        /// Resume the process.
        /// </summary>
        public void Resume()
        {
            ExecuteResumeSuspend(Nt.Functions.NtResumeProcess);

            IsSuspended = false;
        }

        /// <summary>
        /// Suspend the process.
        /// </summary>
        public void Suspend()
        {
            ExecuteResumeSuspend(Nt.Functions.NtSuspendProcess);

            IsSuspended = true;
        }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// <see cref="IDisposable"/> interface implementation.
        /// </summary>
        public void Dispose()
        {
            _process.Dispose();
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="processId">id of extended process</param>
        public ProcessExtended(int processId)
        {
            _process = Process.GetProcessById(processId);

            ResetCpuUsage();
        }

        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="process">extended process</param>
        public ProcessExtended(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            _process = process;

            _totalProcessorTime = _process.TotalProcessorTime.Ticks;
            _totalProcessorTimeUpdate = DateTime.Now;
        }
        #endregion

        private void ExecuteResumeSuspend(Func<IntPtr, Nt.NtStatus> func)
        {
            var result = func(_process.Handle);

            if (result != Nt.NtStatus.Success)
                throw new ApplicationException(string.Format(ResumeSuspendErrorMessage, _process.Id));
        }
    }
}
