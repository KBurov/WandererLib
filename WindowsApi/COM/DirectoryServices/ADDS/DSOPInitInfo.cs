using System;
using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// The DSOPInitInfo structure contains data required to initialize an object picker dialog box.
    /// This structure is used with the <see cref="IDsObjectPicker.Initialize"/> method.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DSOPInitInfo
    {
        /// <summary>
        /// Contains the size, in bytes, of the structure.
        /// </summary>
        public uint cbSize;
        /// <summary>
        /// Pointer to a null-terminated Unicode string that contains the name of the target computer.
        /// The dialog box operates as if it is running on the target computer,
        /// using the target computer to determine the joined domain and enterprise.
        /// If this value is null, the target computer is the local computer.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwzTargetComputer;
        /// <summary>
        /// Specifies the number of elements in the <see cref="aDsScopeInfos"/> array.
        /// </summary>
        public uint cDsScopeInfos;
        /// <summary>
        /// Pointer to an array of <see cref="DSOPScopeInitInfo"/> structures that describe the scopes from which the user can select objects.
        /// This member cannot be NULL (<see cref="IntPtr.Zero"/>) and the array must contain at least one element because the object picker cannot operate without at least one scope.
        /// </summary>
        public IntPtr aDsScopeInfos;
        /// <summary>
        /// Flags that determine the object picker options.
        /// </summary>
        public DSOPFlag flOptions;
        /// <summary>
        /// Contains the number of elements in the <see cref="apwzAttributeNames"/> array.
        /// This member can be zero.
        /// </summary>
        public uint cAttributesToFetch;
        /// <summary>
        /// Pointer to an array of null-terminated Unicode strings that contain the names of the attributes to retrieve for each selected object.
        /// If <see cref="cAttributesToFetch"/> is zero, this member is ignored.
        /// </summary>
        public IntPtr apwzAttributeNames;
    }
}