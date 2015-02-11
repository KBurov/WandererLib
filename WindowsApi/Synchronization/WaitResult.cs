namespace Wanderer.Library.WindowsApi.Synchronization
{
    /// <summary>
    /// Indicates the event that describes wait result.
    /// </summary>
    internal enum WaitResult : uint
    {
        /// <summary>
        /// The state of the specified object is signaled.
        /// </summary>
        Object0 = 0x00000000,
        /// <summary>
        /// The specified object is a mutex object that was not released by the thread that owned the mutex object before the owning thread terminated.
        /// Ownership of the mutex object is granted to the calling thread and the mutex state is set to nonsignaled.
        /// If the mutex was protecting persistent state information, you should check it for consistency.
        /// </summary>
        Abandoned = 0x00000080,
        /// <summary>
        /// The time-out interval elapsed, and the object's state is nonsignaled.
        /// </summary>
        Timeout = 0x00000102,
        /// <summary>
        /// The function has failed.
        /// </summary>
        Failed = 0xFFFFFFFF
    }
}