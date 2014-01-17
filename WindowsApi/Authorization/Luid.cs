using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.Authorization
{
    /// <summary>
    /// An LUID is a 64-bit value guaranteed to be unique only on the system on which it was generated.
    /// The uniqueness of a locally unique identifier (LUID) is guaranteed only until the system is restarted.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Luid
    {
        /// <summary>
        /// Low-order bits.
        /// </summary>
        public uint LowPart;
        /// <summary>
        /// High-order bits.
        /// </summary>
        public uint HighPart;
    }
}