using System;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// Filter flags to use in <see cref="DSOPFilterFlags"/> structure.
    /// </summary>
    [Flags]
    internal enum DSOPFilter : uint
    {
        /// <summary>
        /// Includes objects that have the showInAdvancedViewOnly attribute set to TRUE.
        /// </summary>
        IncludeAdvancedView = 0x00000001,
        /// <summary>
        /// Includes user objects.
        /// </summary>
        Users = 0x00000002,
        // TODO: Fix reference
        /// <summary>
        /// Includes built-in group objects.
        /// Built-in groups are group objects with a groupType value that contain the GROUP_TYPE_BUILTIN_LOCAL_GROUP (0x00000001),
        /// GROUP_TYPE_RESOURCE_GROUP (0x00000004), and GROUP_TYPE_SECURITY_ENABLED (0x80000000) flags.
        /// </summary>
        BuiltInGroups = 0x00000004,
        /// <summary>
        /// Includes the contents of the Well Known Security Principals container.
        /// </summary>
        WellKnownPrincipals = 0x00000008,
        /// <summary>
        /// Includes distribution group objects with universal scope.
        /// </summary>
        UniversalGroupsDL = 0x00000010,
        /// <summary>
        /// Includes security groups with universal scope.
        /// This flag has no affect in a mixed mode domain because universal security groups do not exist in mixed mode domains.
        /// </summary>
        UniversalGroupsSE = 0x00000020,
        /// <summary>
        /// Includes distribution group objects with global scope.
        /// </summary>
        GlobalGroupsDL = 0x00000040,
        /// <summary>
        /// Includes security group objects with global scope.
        /// </summary>
        GlobalGroupsSE = 0x00000080,
        /// <summary>
        /// Includes distribution group objects with domain local scope.
        /// </summary>
        DomainLocalGroupsDL = 0x00000100,
        /// <summary>
        /// Includes security group objects with domain local scope.
        /// </summary>
        DomainLocalGroupsSE = 0x00000200,
        /// <summary>
        /// Includes contact objects.
        /// </summary>
        Contacts = 0x00000400,
        /// <summary>
        /// Includes computer objects.
        /// </summary>
        Computers = 0x00000800,
        /// <summary>
        /// Includes managed service account and group managed service account objects.
        /// </summary>
        ServiceAccounts = 0x00001000,
        /// <summary>
        /// Includes password settings objects.
        /// </summary>
        PasswordSettingsObjects = 0x00002000
    }
}