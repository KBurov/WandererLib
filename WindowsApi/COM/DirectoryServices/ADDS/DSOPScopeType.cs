using System;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// Flags that indicate the scope types described by <see cref="DSOPScopeInitInfo"/> structure.
    /// </summary>
    [Flags]
    internal enum DSOPScopeType : uint
    {
        /// <summary>
        /// Computer specified by the <see cref="DSOPInitInfo.pwzTargetComputer"/>.
        /// If the target computer is an up-level or down-level domain controller,
        /// this flag is ignored unless the <see cref="DSOPFlag.SkipTargetComputerDCCheck"/> flag is set in the <see cref="DSOPInitInfo.flOptions"/>.
        /// </summary>
        TargetComputer = 0x00000001,
        /// <summary>
        /// An up-level domain to which the target computer is joined.
        /// If this flag is set, use the <see cref="DSOPScopeInitInfo.pwzDcName"/> member to specify the name of a domain controller in the joined domain.
        /// </summary>
        UpLevelJoinedDomain = 0x00000002,
        /// <summary>
        /// A down-level domain to which the target computer is joined.
        /// </summary>
        DownLevelJoinedDomain = 0x00000004,
        /// <summary>
        /// All Windows 2000 domains in the enterprise to which the target computer belongs.
        /// If the <see cref="UpLevelJoinedDomain"/> scope is specified,
        /// then the <see cref="EnterpriseDomain"/> scope represents all Windows 2000 domains in the enterprise except the joined domain.
        /// </summary>
        EnterpriseDomain = 0x00000008,
        /// <summary>
        /// A scope that contains objects from all domains in the enterprise.
        /// An enterprise can contain only up-level domains.
        /// </summary>
        GlobalCatalog = 0x00000010,
        /// <summary>
        /// All up-level domains external to the enterprise but trusted by the domain to which the target computer is joined.
        /// </summary>
        ExternalUpLevelDomain = 0x00000020,
        /// <summary>
        /// All down-level domains external to the enterprise, but trusted by the domain to which the target computer is joined.
        /// </summary>
        ExternalDownLevelDomain = 0x00000040,
        /// <summary>
        /// The workgroup to which the target computer is joined.
        /// Applies only if the target computer is not joined to a domain.
        /// The only type of object that can be selected from a workgroup is a computer.
        /// </summary>
        Workgroup = 0x00000080,
        /// <summary>
        /// Enables the user to enter an up-level scope.
        /// If neither of the UserEntered* types is specified,
        /// the dialog box restricts the user to the scopes in the Look in drop-down list.
        /// </summary>
        UserEnteredUpLevelScope = 0x00000100,
        /// <summary>
        /// Enables the user to enter a down-level scope.
        /// </summary>
        UserEnteredDownLevelScope = 0x00000200
    }
}