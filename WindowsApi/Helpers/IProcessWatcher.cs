using System;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Helper interface for process CPU clamping.
    /// </summary>
    [ContractClass(typeof (IProcessWatcherContract))]
    public interface IProcessWatcher : IDisposable, IEquatable<IProcessWatcher>
    {
        /// <summary>
        /// Controlled process.
        /// </summary>
        IProcessExtended WatchedProcess { get; }

        /// <summary>
        /// Maximum avaliable CPU usage for the process.
        /// </summary>
        uint MaxCpuUsage { get; }

        /// <summary>
        /// Determines whether object already disposed or not.
        /// </summary>
        bool IsDisposed { get; }
    }
}