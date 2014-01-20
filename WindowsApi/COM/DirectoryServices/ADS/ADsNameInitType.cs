namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADS
{
    /// <summary>
    /// Specifies the types of initialization to perform on a NameTranslate object.
    /// It is used in the <see cref="IADsNameTranslate"/> interface.
    /// </summary>
    internal enum ADsNameInitType
    {
        /// <summary>
        /// Initializes a NameTranslate object by setting the domain that the object binds to.
        /// </summary>
        Domain = 1,
        /// <summary>
        /// Initializes a NameTranslate object by setting the server that the object binds to.
        /// </summary>
        Server = 2,
        /// <summary>
        /// Initializes a NameTranslate object by locating the global catalog that the object binds to.
        /// </summary>
        GC = 3
    }
}