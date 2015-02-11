namespace Wanderer.Library.WindowsApi
{
    /// <summary>
    /// Contains Windows API constants.
    /// </summary>
    internal static class NativeConstants
    {
        public const int AnySizeArray = 1;

        /// <summary>
        /// Exit code value which determine that process still was not terminated.
        /// </summary>
        public const uint StillActive = 259u;
    }
}