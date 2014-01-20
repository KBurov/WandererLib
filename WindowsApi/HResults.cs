namespace Wanderer.Library.WindowsApi
{
    /// <summary>
    /// Contains Windows COM API error codes.
    /// </summary>
    internal static class HResults
    {
        /// <summary>
        /// Operation successful.
        /// </summary>
        public const uint OK = 0x00000000;
        /// <summary>
        /// Operation successful.
        /// </summary>
        public const uint False = 0x00000001;
        /// <summary>
        /// Not implemented.
        /// </summary>
        public const uint NotImplemented = 0x80004001;
        /// <summary>
        /// No such interface supported.
        /// </summary>
        public const uint NoInterface = 0x80004002;
        /// <summary>
        /// Pointer that is not valid.
        /// </summary>
        public const uint Pointer = 0x80004003;
        /// <summary>
        /// Operation aborted.
        /// </summary>
        public const uint Abort = 0x80004004;
        /// <summary>
        /// Unspecified failure.
        /// </summary>
        public const uint Fail = 0x80004005;
        /// <summary>
        /// Unexpected failure.
        /// </summary>
        public const uint Unexpected = 0x8000FFFF;
        /// <summary>
        /// General access denied error.
        /// </summary>
        public const uint AccessDenied = 0x80070005;
        /// <summary>
        /// Handle that is not valid.
        /// </summary>
        public const uint Handle = 0x80070006;
        /// <summary>
        /// Failed to allocate necessary memory.
        /// </summary>
        public const uint OutOfMemory = 0x8007000E;
        /// <summary>
        /// One or more arguments are not valid.
        /// </summary>
        public const uint InvalidArguments = 0x80070057;
    }
}