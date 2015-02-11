﻿using System;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Helper interface for process CPU clamping.
    /// </summary>
    // TODO: Create separate code contract class
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
    }
}