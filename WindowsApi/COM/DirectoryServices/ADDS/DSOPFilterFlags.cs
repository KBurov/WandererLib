using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// The DSOPFilterFlags structure contains flags that indicate the types of objects presented to the user for a specified scope or scopes.
    /// This structure is contained in the <see cref="DSOPScopeInitInfo"/> structure when calling <see cref="IDsObjectPicker.Initialize"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct DSOPFilterFlags
    {
        /// <summary>
        /// Contains a <see cref="DSOPUpLevelFilterFlags"/> structure that contains the filter flags to use for up-level scopes.
        /// An up-level scope is a scope that supports the ADSI LDAP provider.
        /// </summary>
        public DSOPUpLevelFilterFlags Uplevel;
        /// <summary>
        /// Contains the filter flags to use for down-level scopes.
        /// </summary>
        public DSOPDownLevelFilter flDownlevel;
    }
}