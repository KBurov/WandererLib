using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS
{
    /// <summary>
    /// The IDsObjectPicker interface is used by an application to initialize and display an object picker dialog box.
    /// </summary>
    [ComImport, Guid("0C87E64E-3B7A-11D2-B9E0-00C04FD8DBF7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IDsObjectPicker
    {
        /// <summary>
        /// Initializes the interface with data about the scopes, filters, and options used by the dialog box.
        /// </summary>
        /// <param name="pInitInfo">pointer to a <see cref="DSOPInitInfo"/> structure that contains the initialization data</param>
        /// <returns>
        /// a standard error code or one of the following values:
        /// <see cref="HResults.OK"/> - the method succeeded,
        /// <see cref="HResults.InvalidArguments"/> - the contents of one or more of the members of <paramref name="pInitInfo"/> are invalid,
        /// <see cref="HResults.OutOfMemory"/> - a memory allocation error occurred.
        /// </returns>
        [PreserveSig]
        int Initialize(ref DSOPInitInfo pInitInfo);

        // TODO: Fix reference
        /// <summary>
        /// Displays the dialog box and returns the user's selections.
        /// </summary>
        /// <param name="hwndParent">
        /// handle to the owner window of the dialog box;
        /// this parameter cannot be NULL (<see cref="IntPtr.Zero"/>) or the result of the GetDesktopWindow function.
        /// </param>
        /// <param name="ppdoSelections">
        /// pointer to an <see cref="IDataObject"/> interface pointer that receives a data object that contains data about the user selections;
        /// this data is supplied in the CFSTR_DSOP_DS_SELECTION_LIST data format;
        /// this parameter receives null if the user cancels the dialog box
        /// </param>
        /// <returns>
        /// a standard error code or one of the following values:
        /// <see cref="HResults.OK"/> - the method succeeded,
        /// <see cref="HResults.False"/> - the user canceled the dialog box, <paramref name="ppdoSelections"/> receives null,
        /// <see cref="HResults.Unexpected"/> - the <see cref="IDsObjectPicker"/> object has not been initialized.
        /// </returns>
        [PreserveSig]
        int InvokeDialog(IntPtr hwndParent, out IDataObject ppdoSelections);
    }

    /// <summary>
    /// <see cref="IDsObjectPicker"/> implementor.
    /// </summary>
    [ComImport, Guid("17D6CCD8-3B7B-11D2-B9E0-00C04FD8DBF7")]
    internal class DsObjectPicker {}
}