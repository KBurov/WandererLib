namespace Wanderer.Library.WindowsApi.Authentication
{
    /// <summary>
    /// The logon provider.
    /// </summary>
    internal enum LogonProvider
    {
        /// <summary>
        /// Use the standard logon provider for the system.
        /// The default security provider is negotiate,
        /// unless you pass NULL for the domain name and the user name is not in UPN format.
        /// In this case, the default provider is NTLM.
        /// NOTE: Windows 2000/NT:   The default security provider is NTLM.
        /// </summary>
        Default = 0,
        WinNt35 = 1,
        /// <summary>
        /// Use the NTLM logon provider.
        /// </summary>
        WinNt40 = 2,
        /// <summary>
        /// Use the negotiate logon provider.
        /// </summary>
        WinNt50 = 3,
        Virtual = 4
    }
}