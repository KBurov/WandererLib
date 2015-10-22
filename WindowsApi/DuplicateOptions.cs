using System;

namespace Wanderer.Library.WindowsApi
{
    [Flags]
    internal enum DuplicateOptions : uint
    {
        None = 0x00000000,
        /// <summary>Closes the source handle. This occurs regardless of any error status returned.</summary>
        CloseSource = 0x00000001,
        /// <summary>
        /// Ignores the dwDesiredAccess parameter (<see cref="NativeMethods.DuplicateHandle"/>).
        /// The duplicate handle has the same access as the source handle.</summary>
        SameAccess = 0x00000002,
        All = CloseSource | SameAccess
    }
}