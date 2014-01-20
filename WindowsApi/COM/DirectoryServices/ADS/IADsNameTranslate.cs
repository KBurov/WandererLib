using System.Runtime.InteropServices;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADS
{
    /// <summary>
    /// The IADsNameTranslate interface translates distinguished names (DNs) among various formats as defined in the <see cref="ADsNameType"/> enumeration.
    /// </summary>
    [Guid("B1B272A3-3625-11D1-A3A4-00C04FB950DC"), InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    internal interface IADsNameTranslate
    {
        /// <summary>
        /// Toggles referral chasing ON or OFF.
        /// </summary>
        [DispId(1)]
        int ChaseReferral { set; }

        /// <summary>
        /// Gets the object name, set by <see cref="Set"/>, in a specified format.
        /// </summary>
        /// <param name="lnFormatType">
        /// the format type of the output name
        /// (for more information, see <see cref="ADsNameType"/>; this method does not support the <see cref="ADsNameType.SIDorSIDHistoryName"/> element)
        /// </param>
        /// <returns>the name of the returned object</returns>
        [DispId(5)]
        string Get(int lnFormatType);

        /// <summary>
        /// Gets the names of the objects, set by <see cref="SetEx"/>, in a specified format.
        /// </summary>
        /// <param name="lnFormatType">
        /// the format type used for the output names
        /// (for more information about the various types of formats you can use, see <see cref="ADsNameType"/>;
        /// this method does not support the <see cref="ADsNameType.SIDorSIDHistoryName"/> element)
        /// </param>
        /// <returns>a variant array of strings that hold names of the objects returned</returns>
        [DispId(7)]
        object GetEx(int lnFormatType);

        /// <summary>
        /// Initializes the IADsNameTranslate object with default credentialsю
        /// </summary>
        /// <param name="lnInitType">a type of initialization to be performed, possible values are defined in <see cref="ADsNameInitType"/></param>
        /// <param name="bstrADsPath">
        /// the name of the server or domain, depending on the value of <paramref name="lnInitType"/>: when <see cref="ADsNameInitType.GC"/> is issued, this parameter is ignored;
        /// the global catalog server of the domain of the current computer will perform the name translate operations;
        /// this method will fail if the computer is not part of a domain as no global catalog will be found in this scenario
        /// (for more information, see <see cref="ADsNameInitType"/>)
        /// </param>
        [DispId(2)]
        void Init(ADsNameInitType lnInitType, string bstrADsPath);

        /// <summary>
        /// Initializes the IADsNameTranslate object with specified credentials.
        /// </summary>
        /// <param name="lnInitType">a type of initialization to be performed, possible values are defined in <see cref="ADsNameInitType"/></param>
        /// <param name="bstrADsPath">
        /// the name of the server or domain, depending on the value of <paramref name="lnInitType"/>: when <see cref="ADsNameInitType.GC"/> is issued, this parameter is ignored;
        /// the global catalog server of the domain of the current computer will perform the name translate operations;
        /// this method will fail if the computer is not part of a domain as no global catalog will be found in this scenario
        /// (for more information, see <see cref="ADsNameInitType"/>)
        /// </param>
        /// <param name="bstrUserId">user name</param>
        /// <param name="bstrDomain">user domain name</param>
        /// <param name="bstrPassword">user password</param>
        [DispId(3)]
        void InitEx(ADsNameInitType lnInitType, string bstrADsPath, string bstrUserId, string bstrDomain, string bstrPassword);

        /// <summary>
        /// Specifies the object name to translate.
        /// </summary>
        /// <param name="lnSetType">the format of the name of a directory object (for more information, see <see cref="ADsNameType"/>)</param>
        /// <param name="bstrADsPath">the object name (for example, "CN=Administrator, CN=users, DC=Fabrikam, DC=com")</param>
        [DispId(4)]
        void Set(ADsNameType lnSetType, string bstrADsPath);

        /// <summary>
        /// Sets the names of multiple objects at the same time.
        /// </summary>
        /// <param name="lnFormatType">the format type of the input names (for more information, see <see cref="ADsNameType"/>)</param>
        /// <param name="pVar">a variant array of strings that hold object names</param>
        [DispId(6)]
        void SetEx(ADsNameType lnFormatType, object pVar);
    }

    /// <summary>
    /// <see cref="IADsNameTranslate"/> implementor.
    /// </summary>
    [ComImport, Guid("274FAE1F-3626-11D1-A3A4-00C04FB950DC")]
    internal class ADsNameTranslate {}
}