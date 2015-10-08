using System;
using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// Contains data about an object the user selected from an object picker dialog box.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    // ReSharper disable once InconsistentNaming
    internal struct DSSelection
    {
        /// <summary>
        /// Pointer to a null-terminated Unicode string that contains the object's relative distinguished name (RDN).
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwzName;
        /// <summary>
        /// Pointer to a null-terminated Unicode string that contains the object's ADsPath.
        /// The format of this string depends on the flags specified in the <see cref="DSOPScopeInitInfo.flScope"/> for the scope from which this object was selected.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwzADsPath;
        /// <summary>
        /// Pointer to a null-terminated Unicode string that contains the value of the object's objectClass attribute.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwzClass;
        /// <summary>
        /// Pointer to a null-terminated Unicode string that contains the object's userPrincipalName attribute value.
        /// If the object does not have a userPrincipalName value, pwzUPN points to an empty string (L"").
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwzUPN;
        /// <summary>
        /// Pointer to an array of <see cref="System.Runtime.InteropServices.Variant"/> structures.
        /// Each Variant contains the value of an attribute of the selected object.
        /// The attributes retrieved are determined by the attribute names specified in the <see cref="DSOPInitInfo.apwzAttributeNames"/>
        /// passed to the <see cref="IDsObjectPicker.Initialize"/> method.
        /// The order of attributes in the pvarFetchedAttributes array corresponds to the order of attribute names specified in the <see cref="DSOPInitInfo.apwzAttributeNames"/> array.
        /// </summary>
        public IntPtr pvarFetchedAttributes;
        /// <summary>
        /// Contains one, or more, of the <see cref="DSOPScopeType"/> that indicate the type of scope from which this object was selected.
        /// </summary>
        public uint flScopeType;
    }
}