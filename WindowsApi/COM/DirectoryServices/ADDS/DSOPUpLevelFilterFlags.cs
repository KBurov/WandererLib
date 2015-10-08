using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// The DSOPUpLevelFilterFlags structure contains flags that indicate the filters to use for an up-level scope.
    /// An up-level scope is a scope that supports the ADSI LDAP provider.
    /// This structure is contained in the <see cref="DSOPFilterFlags"/> structure when calling <see cref="IDsObjectPicker.Initialize"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    // ReSharper disable once InconsistentNaming
    internal struct DSOPUpLevelFilterFlags
    {
        /// <summary>
        /// Filter flags to use for an up-level scope, regardless of whether it is a mixed or native mode domain.
        /// </summary>
        public DSOPFilter flBothModes;
        /// <summary>
        /// Filter flags to use for an up-level domain in mixed mode.
        /// Mixed mode refers to an up-level domain that may have Windows NT 4.0 Backup Domain Controllers present.
        /// This member can be a combination of the flags listed in the <see cref="flBothModes"/> flags.
        /// The <see cref="DSOPFilter.UniversalGroupsSE"/> flag has no affect in a mixed-mode domain because universal security groups do not exist in mixed mode domains.
        /// </summary>
        public DSOPFilter flMixedModeOnly;
        /// <summary>
        /// Filter flags to use for an up-level domain in native mode.
        /// Native mode refers to an up-level domain in which all domain controllers are running Windows 2000 and an administrator has enabled native mode operation.
        /// This member can be a combination of the flags listed in the <see cref="flBothModes"/> flags.
        /// </summary>
        public DSOPFilter flNativeModeOnly;
    }
}