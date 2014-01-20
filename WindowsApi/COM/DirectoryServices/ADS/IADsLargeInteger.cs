using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADS
{
    /// <summary>
    /// The IADsLargeInteger interface is used to manipulate 64-bit integers of the LargeInteger type.
    /// </summary>
    [ComImport, Guid("9068270B-0939-11D1-8BE1-00C04FD8D503")]
    internal interface IADsLargeInteger
    {
        /// <summary>
        /// Gets and sets the upper 32 bits of the integer.
        /// </summary>
        int HighPart { get; set; }
        /// <summary>
        /// Gets and sets the lower 32 bits of the integer.
        /// </summary>
        int LowPart { get; set; }
    }
}