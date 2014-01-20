using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// The DSOPScopeInitInfo structure describes one or more scope types that have the same attributes.
    /// A scope type is a type of location, for example a domain, computer, or Global Catalog, from which the user can select objects.
    /// This structure is used with <see cref="DSOPInitInfo"/> when calling <see cref="IDsObjectPicker.Initialize"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DSOPScopeInitInfo
    {
        /// <summary>
        /// Contains the size, in bytes, of the structure.
        /// </summary>
        public uint cbSize;
        /// <summary>
        /// Flags that indicate the scope types described by this structure.
        /// You can combine multiple scope types if all specified scopes use the same settings.
        /// </summary>
        public DSOPScopeType flType;
        /// <summary>
        /// Flags that indicate the format used to return ADsPath for objects selected from this scope.
        /// The flScope member can also indicate the initial scope displayed in the Look in drop-down list.
        /// </summary>
        public DSOPScopeFlag flScope;
        /// <summary>
        /// Contains a <see cref="DSOPFilterFlags"/> structure that indicates the types of objects presented to the user for this scope or scopes.
        /// </summary>
        public DSOPFilterFlags FilterFlags;
        /// <summary>
        /// Pointer to a null-terminated Unicode string that contains the name of a domain controller of the domain to which the target computer is joined.
        /// This member is used only if the <see cref="flType"/> member contains the <see cref="DSOPScopeType.UpLevelJoinedDomain"/> flag.
        /// If that flag is not set, pwzDcName must be null.
        /// This member can be null even if the <see cref="DSOPScopeType.UpLevelJoinedDomain"/> flag is specified, in which case, the dialog box looks up the domain controller.
        /// This member enables you to name a specific domain controller in a multimaster domain.
        /// For example, an administrative application might make changes on a domain controller in a multimaster domain,
        /// and then open the object picker dialog box before the changes have been replicated on the other domain controllers.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwzDcName;
        /// <summary>
        /// Reserved; must be null.
        /// </summary>
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pwzADsPath;
        /// <summary>
        /// Contains an <see cref="HResults"/> value that indicates the status of the specific scope.
        /// If the <see cref="IDsObjectPicker.Initialize"/> method successfully creates the scope, or scopes, specified by this structure, hr contains <see cref="HResults.OK"/>.
        /// Otherwise, hr contains an error code.
        /// If <see cref="IDsObjectPicker.Initialize"/> returns <see cref="HResults.OK"/>,
        /// the hr members of all the specified <see cref="DSOPScopeInitInfo"/> structures also contain <see cref="HResults.OK"/>.
        /// </summary>
        public uint hr;
    }
}