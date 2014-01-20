using System;

namespace Wanderer.Library.WindowsApi.Helpers.ObjectPicker
{
    /// <summary>
    /// Indicates the ADsPath provider type of the <see cref="ObjectPickerDialog"/>.
    /// This provider affects the contents of the ADPath returned.
    /// </summary>
    [Flags]
    public enum PathProvider
    {
        /// <summary>
        /// Default path provider.
        /// </summary>
        Default = 0,
        /// <summary>
        /// The ADsPath are converted to use the WinNT provider.
        /// </summary>
        WinNT = 0x00000002,
        /// <summary>
        /// The ADsPaths are converted to use the LDAP provider.
        /// </summary>
        LDAP = 0x00000004,
        /// <summary>
        /// The ADsPaths for objects selected from this scope are converted to use the GC provider.
        /// </summary>
        GC = 0x00000008,
        /// <summary>
        /// The ADsPaths having an objectSid attribute are converted to the form 
        /// </summary>
        SIDPath = 0x00000010,
        /// <summary>
        /// The ADsPaths for down-level, well-known SID objects are an empty string unless this flag is specified (For example; DSOP_DOWNLEVEL_FILTER_INTERACTIVE).
        /// If this flag is specified, the paths have the form: 
        /// </summary>
        DownlevelBuildinPath = 0x00000020
    }
}