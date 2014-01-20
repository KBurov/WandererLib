using System;

using Microsoft.Win32.SafeHandles;

namespace Wanderer.Library.WindowsApi.SafeHandles
{
    /// <summary>
    /// Provides a class for Window Station.
    /// </summary>
    internal sealed class SafeWindowStationHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private SafeWindowStationHandle() : base(true) {}

        internal SafeWindowStationHandle(IntPtr handle, bool ownsHandle = true)
            : base(ownsHandle)
        {
            SetHandle(handle);
        }

        protected override bool ReleaseHandle()
        {
            return Desktop.NativeMethods.CloseWindowStation(handle);
        }
    }
}