namespace Wanderer.Library.WindowsApi
{
    /// <summary>
    /// Contains Windows API error codes.
    /// </summary>
    internal static class ErrorCodes
    {
        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        public const int Success = 0x0000;
        /// <summary>
        /// The operation completed successfully.
        /// </summary>
        public const int NoError = 0x0000;
        /// <summary>
        /// Access is denied.
        /// </summary>
        public const int AccessDenied = 0x0005;
        /// <summary>
        /// Not enough storage is available to process this command.
        /// </summary>
        public const int NotEnoughMemory = 0x0008;
        /// <summary>
        /// An attempt was made to reference a token that does not exist.
        /// </summary>
        public const int NoToken = 0x03F0;
        /// <summary>
        /// The service cannot be started,
        /// either because it is disabled or because it has no enabled devices associated with it.
        /// </summary>
        public const int ServiceDisabled = 0x0422;
        /// <summary>
        /// Not all privileges or groups referenced are assigned to the caller.
        /// </summary>
        public const int NotAllAssigned = 0x0514;
        /// <summary>
        /// A specified privilege does not exist.
        /// </summary>
        public const int NoSuchPrivilege = 0x0521;
        /// <summary>
        /// Cannot open an anonymous level security token.
        /// </summary>
        public const int CantOpenAnonymous = 0x0543;
    }
}