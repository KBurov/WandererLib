using System;
using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.Memory
{
    /// <summary>
    /// Memory menagement Windows API functions.
    /// </summary>
    internal static class NativeMethods
    {
        private const string Kernel32 = "kernel32.dll";

        /// <summary>
        /// Locks a global memory object and returns a pointer to the first byte of the object's memory block.
        /// </summary>
        /// <param name="hMem">a handle to the global memory object</param>
        /// <returns>
        /// if the function succeeds, the return value is a pointer to the first byte of the memory block;
        /// if the function fails, the return value is NULL (<see cref="IntPtr.Zero"/>)
        /// </returns>
        [DllImport(Kernel32, SetLastError = true)]
        public static extern IntPtr GlobalLock([In] IntPtr hMem);

        /// <summary>
        /// Decrements the lock count associated with a memory object.
        /// </summary>
        /// <param name="hMem">a handle to the global memory object</param>
        /// <returns>
        /// if the memory object is still locked after decrementing the lock count, the return value is a nonzero value;
        /// if the memory object is unlocked after decrementing the lock count, the function returns zero and <see cref="Marshal.GetLastWin32Error"/> returns <see cref="ErrorCodes.NoError"/>.
        /// </returns>
        [DllImport(Kernel32, SetLastError = true)]
        public static extern bool GlobalUnlock([In] IntPtr hMem);
    }
}