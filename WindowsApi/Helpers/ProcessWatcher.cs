using System;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Helper class for process CPU clamping.
    /// </summary>
    public class ProcessWatcher : IProcessWatcher
    {
        #region Variables
        private readonly IProcessExtended _processExtended;
        private readonly uint _maxCpuUsage;
        #endregion

        #region IProcessWatcher implementation
        /// <summary>
        /// Controlled process.
        /// </summary>
        public IProcessExtended WatchedProcess { get { return _processExtended; } }

        /// <summary>
        /// Maximum avaliable CPU usage for the process.
        /// </summary>
        public uint MaxCpuUsage { get { return _maxCpuUsage; } }
        #endregion

        #region IDisposable implementation
        /// <summary>
        /// <see cref="IDisposable"/> interface implementation.
        /// </summary>
        public void Dispose()
        {
            _processExtended.Dispose();
        }
        #endregion

        #region IEquatable<IProcessWatcher> implemetation
        /// <summary>
        /// Indicates whether the current <see cref="IProcessWatcher"/> object is equal to another <see cref="IProcessWatcher"/> object.
        /// </summary>
        /// <param name="other">an <see cref="IProcessWatcher"/> to compare</param>
        /// <returns><b>true</b> if current object is equal to <i>other</i> parameter; otherwise, <b>false</b></returns>
        public bool Equals(IProcessWatcher other)
        {
            if (other == null)
                return false;

            return WatchedProcess.Process.Id == other.WatchedProcess.Process.Id;
        }
        #endregion

        /// <summary>
        /// Initialize constructor.
        /// </summary>
        /// <param name="processExtended">controlled process</param>
        /// <param name="maxCpuUsage">maximum avaliable CPU usage for the process</param>
        public ProcessWatcher(IProcessExtended processExtended, uint maxCpuUsage)
        {
            if (processExtended == null)
                throw new ArgumentNullException("processExtended");

            _processExtended = processExtended;
            _maxCpuUsage = maxCpuUsage;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">the object to compare with the current object</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

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
