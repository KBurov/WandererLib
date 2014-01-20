using System;

namespace Wanderer.Library.WindowsApi.Helpers.ObjectPicker
{
    /// <summary>
    /// Indicates the scope the <see cref="ObjectPickerDialog"/> searches for objects.
    /// </summary>
    [Flags]
    public enum Location : uint
    {
        /// <summary>
        /// No locations.
        /// </summary>
        None = 0x0,
        /// <summary>
        /// The target computer (down-level).
        /// </summary>
        LocalComputer = 0x0001,
        /// <summary>
        /// A domain to which the target computer is joined (down-level and up-level).
        /// </summary>
        JoinedDomain = 0x0002,
        /// <summary>
        /// All Windows 2000 domains in the enterprise to which the target computer belongs (up-level).
        /// </summary>
        EnterpriseDomain = 0x0004,
        /// <summary>
        /// A scope containing objects from all domains in the enterprise (up-level). 
        /// </summary>
        GlobalCatalog = 0x0008,
        /// <summary>
        /// All domains external to the enterprise, but trusted by the domain to which the target computer is joined (down-level and up-level).
        /// </summary>
        ExternalDomain = 0x0010,
        /// <summary>
        /// The workgroup to which the target computer is joined (down-level). 
        /// </summary>
        Workgroup = 0x0020,
        /// <summary>
        /// Enables the user to enter a scope (down-level and up-level). 
        /// </summary>
        UserEntered = 0x0040,
        /// <summary>
        /// All locations.
        /// </summary>
        All = 0x007F
    }
}