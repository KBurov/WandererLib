using System;

namespace Wanderer.Library.WindowsApi.Authorization
{
    /// <summary>
    /// The TokenType enumeration contains values that differentiate between a primary token and an impersonation token.
    /// </summary>
    [Serializable]
    internal enum TokenType
    {
        /// <summary>
        /// Indicates a primary token.
        /// </summary>
        Primary = 1,
        /// <summary>
        /// Indicates an impersonation token.
        /// </summary>
        Impersonation = 2,
    }
}