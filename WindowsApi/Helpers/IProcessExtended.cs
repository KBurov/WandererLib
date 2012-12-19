using System;
using System.Diagnostics;
using System.Security;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Provides additonal functionality for process, for example resume/suspend.
    /// </summary>
    [SecurityCritical]
    public interface IProcessExtended : IDisposable
    {
        /// <summary>
        /// Extended process.
        /// </summary>
        Process Process { get; }

        /// <summary>
        /// CPU usage in percent.
        /// This is cumulative value which calculated from the first time when <see cref="IProcessExtended"/> was created.
        /// Can be cleared by <see cref="ResetCpuUsage"/> method.
        /// </summary>
        uint CpuUsage { get; }

        /// <summary>
        /// Indicates that the process was suspended.
        /// </summary>
        bool IsSuspended { get; }

        /// <summary>
        /// Reset CPU usage value and time.
        /// </summary>
        void ResetCpuUsage();

        /// <summary>
        /// Resume the process.
        /// </summary>
        void Resume();

        /// <summary>
        /// Suspend the process.
        /// </summary>
        void Suspend();
    }
}
