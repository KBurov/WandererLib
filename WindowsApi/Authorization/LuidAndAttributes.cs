using System;
using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.Authorization
{
    /// <summary>
    /// The LuidAndAttributes structure represents a locally unique identifier (<see cref="Authorization.Luid"/>) and its attributes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal struct LuidAndAttributes
    {
        /// <summary>
        /// Specifies an <see cref="Authorization.Luid"/> value.
        /// </summary>
        public Luid Luid;
        /// <summary>
        /// Specifies attributes of the <see cref="Luid"/>.
        /// This value contains up to 32 one-bit flags.
        /// Its meaning is dependent on the definition and use of the <see cref="Luid"/>.
        /// </summary>
        public UInt32 Attributes;
    }
}