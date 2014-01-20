using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Wanderer.Library.WindowsApi.COM
{
    /// <summary>
    /// COM Windows API functions.
    /// </summary>
    internal static class NativeMethods
    {
        private const string Ole32 = "ole32.dll";

        /// <summary>
        /// Frees the specified storage medium.
        /// </summary>
        /// <param name="pMedium">pointer to the storage medium that is to be freed</param>
        [DllImport(Ole32)]
        public static extern void ReleaseStgMedium([In] ref STGMEDIUM pMedium);
    }
}