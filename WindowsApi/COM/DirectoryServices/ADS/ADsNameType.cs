namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADS
{
    /// <summary>
    /// The NameType enumeration specifies the formats used for representing distinguished names.
    /// It is used by the <see cref="IADsNameTranslate"/> interface to convert the format of a distinguished name.
    /// </summary>
    internal enum ADsNameType
    {
        /// <summary>
        /// Name format as specified in RFC 1779.
        /// For example, "CN=Jeff Smith,CN=users,DC=Fabrikam,DC=com".
        /// </summary>
        _1779 = 1,
        /// <summary>
        /// Canonical name format.
        /// For example, "Fabrikam.com/Users/Jeff Smith".
        /// </summary>
        Canonical = 2,
        /// <summary>
        /// Account name format used in Windows NT 4.0.
        /// For example, "Fabrikam\JeffSmith".
        /// </summary>
        NT4 = 3,
        /// <summary>
        /// Display name format.
        /// For example, "Jeff Smith".
        /// </summary>
        Display = 4,
        /// <summary>
        /// Simple domain name format.
        /// For example, "JeffSmith@Fabrikam.com".
        /// </summary>
        DomainSimple = 5,
        /// <summary>
        /// Simple enterprise name format.
        /// For example, "JeffSmith@Fabrikam.com".
        /// </summary>
        EnterpriseSimple = 6,
        /// <summary>
        /// Global Unique Identifier format.
        /// For example, "{95ee9fff-3436-11d1-b2b0-d15ae3ac8436}".
        /// </summary>
        Guid = 7,
        /// <summary>
        /// Unknown name type. The system will estimate the format.
        /// This element is a meaningful option only with the <see cref="IADsNameTranslate.Set"/> or the <see cref="IADsNameTranslate.SetEx"/> method,
        /// but not with the <see cref="IADsNameTranslate.Get"/> or <see cref="IADsNameTranslate.GetEx"/> method.
        /// </summary>
        Unknown = 8,
        /// <summary>
        /// User principal name format.
        /// For example, "JeffSmith@Fabrikam.com".
        /// </summary>
        UserPrincipalName = 9,
        /// <summary>
        /// Extended canonical name format.
        /// For example, "Fabrikam.com/Users Jeff Smith".
        /// </summary>
        CanonicalEx = 10,
        /// <summary>
        /// Service principal name format.
        /// For example, "www/www.fabrikam.com@fabrikam.com".
        /// </summary>
        ServicePrincipalName = 11,
        /// <summary>
        /// A SID string, as defined in the Security Descriptor Definition Language (SDDL), for either the SID of the current object or one from the object SID history.
        /// For example, "O:AOG:DAD:(A;;RPWPCCDCLCSWRCWDWOGA;;;S-1-0-0)".
        /// </summary>
        SIDorSIDHistoryName = 12
    }
}