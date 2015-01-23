using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Timers;

using Timer = System.Timers.Timer;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Provides feature to control maximum CPU usage for list of processes.
    /// </summary>
    public sealed class CpuLimiter : IDisposable
    {
        private const int RefreshRateInMs = 1;
        private const int CpuUsageResetIntervalInMs = 2000;

        #region Variables
        private readonly IList<IProcessWatcher> _watchList;
        private readonly ReaderWriterLockSlim _watchListLocker;
        private readonly Timer _watchingTimer;

        private int _cpuUsageResetIntervalInMs;
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// <see cref="IDisposable"/> interface implementation.
        /// </summary>
        public void Dispose()
        {
            using (_watchListLocker.GetWriteLock()) {
                _watchingTimer.Elapsed -= OnTimedEvent;
                _watchingTimer.Dispose();

                foreach (var processWatcher in _watchList) {
                    if (processWatcher.WatchedProcess.IsSuspended) {
                        try {
                            processWatcher.WatchedProcess.Resume();
                        }
// ReSharper disable EmptyGeneralCatchClause
                        catch {}
// ReSharper restore EmptyGeneralCatchClause
                    }
                    processWatcher.Dispose();
                }

                _watchList.Clear();
            }

            _watchListLocker.Dispose();
        }
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public CpuLimiter()
        {
            // Initialize the list
            _watchList = new List<IProcessWatcher>();
            _watchListLocker = new ReaderWriterLockSlim();
            // Create the watching timer
            _watchingTimer = new Timer
                {
                    AutoReset = false,
                    // Set the timer refresh rate
                    Interval = RefreshRateInMs
                };
            // Hook up the Elapsed event for the timer.
            _watchingTimer.Elapsed += OnTimedEvent;
        }

        /// <summary>
        /// Add process to list of controlled processes.
        /// </summary>
        /// <param name="processWatcher"><see cref="IProcessWatcher"/></param>
        public void AddProcessToWatchList(IProcessWatcher processWatcher)
        {
            Contract.Requires<ArgumentNullException>(processWatcher != null, "processWatcher cannot be null");

            using (_watchListLocker.GetWriteLock()) {
                if (_watchList.Contains(processWatcher)) {
                    _watchList.Remove(processWatcher);
                }

                _watchList.Add(processWatcher);

                if (_watchList.Count > 0 && !_watchingTimer.Enabled) {
                    _watchingTimer.Enabled = true;
                }
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            using (_watchListLocker.GetUpgradeableReadLock()) {
                _cpuUsageResetIntervalInMs += RefreshRateInMs;

                var resetCpuUsage = _cpuUsageResetIntervalInMs >= CpuUsageResetIntervalInMs;

                for (var i = _watchList.Count - 1;i >= 0;i--) {
                    var processWatcher = _watchList[i];
                    // Check if the process has exited
                    if (processWatcher.WatchedProcess.Process.HasExited) {
                        // Remove this process watcher from the watch list
                        using (_watchListLocker.GetWriteLock()) {
                            _watchList.RemoveAt(i);
                            processWatcher.Dispose();
                        }
                    }
                    else {
                        // If the process is suspended resume it
                        if (processWatcher.WatchedProcess.IsSuspended) {
                            if (processWatcher.WatchedProcess.CpuUsage < processWatcher.MaxCpuUsage) {
                                try {
                                    processWatcher.WatchedProcess.Resume();
                                }
                                catch (ApplicationException) {
                                    // Something bad happended so remove this watcher
                                    using (_watchListLocker.GetWriteLock()) {
                                        _watchList.RemoveAt(i);
                                        processWatcher.Dispose();
                                    }
                                }
                            }
                        }
                        else {
                            if (processWatcher.WatchedProcess.CpuUsage >= processWatcher.MaxCpuUsage) {
                                try {
                                    processWatcher.WatchedProcess.Suspend();
                                }
                                catch (ApplicationException) {
                                    // Something bad happended so remove this watcher
                                    using (_watchListLocker.GetWriteLock()) {
                                        _watchList.RemoveAt(i);
                                        processWatcher.Dispose();
                                    }
                                }
                            }
                            else if (resetCpuUsage) {
                                processWatcher.WatchedProcess.ResetCpuUsage();
                            }
                        }
                    }
                }

                if (_watchList.Count > 0) {
                    _watchingTimer.Enabled = true;
                }

                if (resetCpuUsage) {
                    _cpuUsageResetIntervalInMs = 0;
                }
            }
        }
    }
}