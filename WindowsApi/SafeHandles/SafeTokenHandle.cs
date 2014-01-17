using System;

using Microsoft.Win32.SafeHandles;

namespace Wanderer.Library.WindowsApi.SafeHandles
{
    /// <summary>
    /// Provides a class for Win32 safe token handle implementations in which the value of either 0 or -1 indicates an invalid handle.
    /// </summary>
    public sealed class SafeTokenHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeTokenHandle() : base(true) {}

        internal SafeTokenHandle(IntPtr handle, bool ownsHandle = true)
            : base(ownsHandle)
        {
            SetHandle(handle);
        }

        /// <summary>
        ///  Executes the code required to free the handle.
        /// </summary>
        /// <returns>true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false</returns>
        protected override bool ReleaseHandle()
        {
            return NativeMethods.CloseHandle(handle);
        }
    }
}