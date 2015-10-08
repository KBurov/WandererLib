using System;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// Flags that indicate the format used to return ADsPath for objects selected from this scope.
    /// </summary>
    [Flags]
    // ReSharper disable InconsistentNaming
    internal enum DSOPScopeFlag : uint
    {
        /// <summary>
        /// The scope described by this structure is initially selected in the Look in drop-down list.
        /// Only one scope can specify this flag. If no scope specifies this flag,
        /// the initial scope is the first successfully created scope in the array of scopes passed to the <see cref="IDsObjectPicker.Initialize"/> method.
        /// </summary>
        StartingScope = 0x00000001,
        /// <summary>
        /// The ADsPaths are converted to use the WinNT provider.
        /// </summary>
        WantProviderWinNT = 0x00000002,
        /// <summary>
        /// The ADsPaths are converted to use the LDAP provider.
        /// </summary>
        WantProviderLDAP = 0x00000004,
        /// <summary>
        /// The ADsPaths for objects selected from this scope are converted to use the GC provider.
        /// </summary>
        WantProviderGC = 0x00000008,
#pragma warning disable 1570
        /// <summary>
        /// The ADsPaths having an objectSid attribute are converted to the form LDAP://<SID=x> where x represents the hexadecimal digits of the objectSid attribute value.
        /// </summary>
        WantSIDPath = 0x00000010,
#pragma warning restore 1570
        /// <summary>
        /// The ADsPaths for down-level, well-known SID objects are an empty string unless this flag is specified (For example; <see cref="DSOPDownLevelFilter.Interactive"/>).
        /// If this flag is specified, the paths have the form WinNT://NT AUTHORITY/Interactive or WinNT://Creator owner.
        /// </summary>
        WantDownLevelBuiltInPath = 0x00000020,
        /// <summary>
        /// If the scope filter contains users, select the Users check box in the dialog.
        /// </summary>
        DefaultFilterUsers = 0x00000040,
        /// <summary>
        /// If the scope filter contains groups, select the Groups check box in the dialog.
        /// </summary>
        DefaultFilterGroups = 0x00000080,
        /// <summary>
        /// If the scope filter contains computers, select the Computers check box in the dialog.
        /// </summary>
        DefaultFilterComputers = 0x00000100,
        /// <summary>
        /// If the scope filter contains contacts, select the Contacts check box in the dialog.
        /// </summary>
        DefaultFilterContacts = 0x00000200,
        /// <summary>
        /// If the scope filter contains service accounts, select the Service Accounts and Group Managed Service Accounts check boxes in the dialog.
        /// </summary>
        DefaultFilterServiceAccounts = 0x00000400,
        /// <summary>
        /// If the scope filter contains password setting objects, select the Password Setting Objects check box in the dialog.
        /// </summary>
        DefaultFilterPasswordSettingsObjects = 0x00000800
    }
}