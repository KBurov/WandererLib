using System;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// Avaliable option flags for <see cref="DSOPInitInfo.flOptions"/>.
    /// </summary>
    [Flags]
    internal enum DSOPFlag : uint
    {
        /// <summary>
        /// If this flag is set, the user can select multiple objects.
        /// If this flag is not set, the user can select only one object.
        /// </summary>
        MultiSelect = 0x00000001,
        /// <summary>
        /// If this flag is set and the <see cref="DSOPScopeType.TargetComputer"/> flag is set in the <see cref="DSOPInitInfo.aDsScopeInfos"/> array,
        /// the target computer is always included in the Look in drop-down list.
        /// If this flag is not set and the target computer is an up-level or down-level domain controller,
        /// the <see cref="DSOPScopeType.TargetComputer"/> flag is ignored and the target computer is not included in the Look in drop-down list.
        /// To save time during initialization, this flag should be set if it is known that the target computer is not a domain controller.
        /// However, if the target computer is a domain controller,
        /// this flag should not be set because it is better for the user to select domain objects from the domain scope rather than from the domain controller itself.
        /// </summary>
        SkipTargetComputerDCCheck = 0x00000002
    }
}