using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;

using Wanderer.Library.WindowsApi.COM.DirectoryServices.ADDS;
using Wanderer.Library.WindowsApi.COM.DirectoryServices.ADS;

namespace Wanderer.Library.WindowsApi.Helpers.ObjectPicker
{
    /// <summary>
    /// Represents a common dialog that allows a user to select directory objects.
    /// </summary>
    public class ObjectPickerDialog : CommonDialog
    {
        #region UnmanagedArrayOfStrings class
        private sealed class UnmanagedArrayOfStrings : IDisposable
        {
            private readonly int _length;
            private readonly IntPtr[] _unmanagedStrings;

            private IntPtr _unmanagedArray;
            private bool _disposed;

            public IntPtr ArrayPtr { get { return _unmanagedArray; } }

            #region IDisposable implementation
            public void Dispose()
            {
                Dispose(true);

                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (_disposed) {
                    return;
                }

                if (disposing) {
                    if (_unmanagedArray != IntPtr.Zero) {
                        Marshal.FreeCoTaskMem(_unmanagedArray);

                        _unmanagedArray = IntPtr.Zero;
                    }

                    for (var cx = 0;cx < _length;cx++) {
                        if (_unmanagedStrings[cx] != IntPtr.Zero) {
                            Marshal.FreeCoTaskMem(_unmanagedStrings[cx]);

                            _unmanagedStrings[cx] = IntPtr.Zero;
                        }
                    }
                }

                _disposed = true;
            }
            #endregion

            public UnmanagedArrayOfStrings(IList<string> strings)
            {
                if (strings != null) {
                    _length = strings.Count;
                    _unmanagedStrings = new IntPtr[_length];

                    var neededSize = _length * IntPtr.Size;

                    _unmanagedArray = Marshal.AllocCoTaskMem(neededSize);

                    for (var cx = _length - 1;cx >= 0;cx--) {
                        _unmanagedStrings[cx] = Marshal.StringToCoTaskMemUni(strings[cx]);

                        Marshal.WriteIntPtr(_unmanagedArray, cx * IntPtr.Size, _unmanagedStrings[cx]);
                    }
                }
            }

            ~UnmanagedArrayOfStrings()
            {
                Dispose(false);
            }
        }
        #endregion

        private readonly List<string> _attributesToFetch = new List<string>();

        private DirectoryObject[] _selectedObjects;

        /// <summary>
        /// Gets or sets the scopes the <see cref="ObjectPickerDialog"/> is allowed to search.
        /// </summary>
        public Location AllowedLocations { get; set; }

        /// <summary>
        /// Gets or sets the initially selected scope in the <see cref="ObjectPickerDialog"/>.
        /// </summary>
        public Location DefaultLocations { get; set; }

        /// <summary>
        /// Gets or sets the types of objects the <see cref="ObjectPickerDialog"/> is allowed to search for.
        /// </summary>
        protected ObjectType AllowedTypes { get; set; }

        /// <summary>
        /// Gets or sets the initially seleted types of objects in the <see cref="ObjectPickerDialog"/>.
        /// </summary>
        public ObjectType DefaultTypes { get; set; }

        /// <summary>
        /// Gets or sets the providers affecting the ADPath returned in objects.
        /// </summary>
        public PathProvider Providers { get; set; }

        /// <summary>
        /// Gets or sets whether the user can select multiple objects.
        /// </summary>
        public bool MultiSelect { get; set; }

        /// <summary>
        /// Gets or sets the whether to check whether the target is a Domain Controller and hide the "Local Computer" scope.
        /// </summary>
        public bool SkipDomainControllerCheck { get; set; }

        /// <summary>
        /// List of LDAP attribute names that will be retrieved for picked objects.
        /// </summary>
        public List<string> AttributesToFetch { get { return _attributesToFetch; } }

        /// <summary>
        /// Gets an array of the directory objects selected in the dialog.
        /// </summary>
        protected DirectoryObject[] SelectedObjects
        {
            get { return (_selectedObjects == null) ? new DirectoryObject[0] : (DirectoryObject[]) _selectedObjects.Clone(); }
        }

        /// <summary>
        /// Gets or sets whether objects flagged as show in advanced view only are displayed (up-level).
        /// </summary>
        public bool ShowAdvancedView { get; set; }

        /// <summary>
        /// Gets or sets the name of the target computer.
        /// </summary>
        public string TargetComputer { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ObjectPickerDialog()
        {
            Reset();
        }

        /// <summary>
        /// Resets the properties of a dialog box to their default values.
        /// </summary>
        public override void Reset()
        {
            AllowedLocations = Location.All;
            DefaultLocations = Location.None;
            AllowedTypes = ObjectType.All;
            DefaultTypes = ObjectType.All;
            Providers = PathProvider.Default;
            MultiSelect = false;
            SkipDomainControllerCheck = false;
            _selectedObjects = null;
            ShowAdvancedView = false;
            TargetComputer = null;

            AttributesToFetch.Clear();
        }

        /// <summary>
        /// Displays the Object Picker (Active Directory) common dialog, when called by ShowDialog.
        /// </summary>
        /// <param name="hwndOwner">handle to the window that owns the dialog</param>
        /// <returns>if the user clicks the OK button of the Object Picker dialog that is displayed, true is returned; otherwise, false.</returns>
        protected override bool RunDialog(IntPtr hwndOwner)
        {
            System.Runtime.InteropServices.ComTypes.IDataObject dataObj;

            var objectPicker = Initialize();

            if (objectPicker == null) {
                _selectedObjects = null;

                return false;
            }

            var hresult = objectPicker.InvokeDialog(hwndOwner, out dataObj);

            if (hresult == HResults.OK) {
                _selectedObjects = ProcessSelections(dataObj);

                Marshal.ReleaseComObject(dataObj);

                return true;
            }

            if (hresult == HResults.False) {
                _selectedObjects = null;

                Marshal.ReleaseComObject(objectPicker);

                return false;
            }

            throw new COMException("IDsObjectPicker.InvokeDialog failed", hresult);
        }

        private IDsObjectPicker Initialize()
        {
            var objectPicker = new DsObjectPicker();
            var result = (IDsObjectPicker) objectPicker;
            var scopeInitInfoList = new List<DSOPScopeInitInfo>();
            var defaultFilter = GetDefaultFilter();
            var upLevelFilter = GetUpLevelFilter();
            var downLevelFilter = GetDownLevelFilter();
            var providerFlags = GetProviderFlags();
            var initialScope = GetScope(DefaultLocations);

            if (((uint) initialScope) > 0u) {
                var initialScopeInfo = new DSOPScopeInitInfo
                                       {
                                           cbSize = (uint) Marshal.SizeOf(typeof (DSOPScopeInitInfo)),
                                           flType = initialScope,
                                           flScope = DSOPScopeFlag.StartingScope | defaultFilter | providerFlags,
                                           pwzADsPath = null,
                                           pwzDcName = null,
                                           hr = 0
                                       };

                initialScopeInfo.FilterFlags.Uplevel.flBothModes = upLevelFilter;
                initialScopeInfo.FilterFlags.flDownlevel = downLevelFilter;

                scopeInitInfoList.Add(initialScopeInfo);
            }

            var otherLocations = AllowedLocations & (~DefaultLocations);
            var otherScope = GetScope(otherLocations);

            if (((uint) otherScope) > 0u) {
                var otherScopeInfo = new DSOPScopeInitInfo
                                     {
                                         cbSize = (uint) Marshal.SizeOf(typeof (DSOPScopeInitInfo)),
                                         flType = otherScope,
                                         flScope = defaultFilter | providerFlags,
                                         pwzADsPath = null,
                                         pwzDcName = null,
                                         hr = 0
                                     };

                otherScopeInfo.FilterFlags.Uplevel.flBothModes = upLevelFilter;
                otherScopeInfo.FilterFlags.flDownlevel = downLevelFilter;

                scopeInitInfoList.Add(otherScopeInfo);
            }

            var scopeInitInfos = scopeInitInfoList.ToArray();
            var scopeInitInfosRef = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (DSOPScopeInitInfo)) * scopeInitInfos.Length);

            try {
                for (var i = 0;i < scopeInitInfos.Length;i++) {
                    Marshal.StructureToPtr(scopeInitInfos[i], (IntPtr) ((int) scopeInitInfosRef + i * Marshal.SizeOf(typeof (DSOPScopeInitInfo))),
                        false);
                }

                var initInfo = new DSOPInitInfo
                               {
                                   cbSize = (uint) Marshal.SizeOf(typeof (DSOPInitInfo)),
                                   pwzTargetComputer = TargetComputer,
                                   cDsScopeInfos = (uint) scopeInitInfos.Length,
                                   aDsScopeInfos = scopeInitInfosRef
                               };
                var flOptions = (DSOPFlag) 0;

                if (MultiSelect) {
                    flOptions |= DSOPFlag.MultiSelect;
                }
                if (SkipDomainControllerCheck) {
                    flOptions |= DSOPFlag.SkipTargetComputerDCCheck;
                }

                initInfo.flOptions = flOptions;

                var goingToFetch = new List<string>(AttributesToFetch);

                for (var i = 0;i < goingToFetch.Count;i++) {
                    if (goingToFetch[i].Equals("objectClass", StringComparison.OrdinalIgnoreCase)) {
                        goingToFetch[i] = "__objectClass__";
                    }
                }

                initInfo.cAttributesToFetch = (uint) goingToFetch.Count;

                using (var unmanagedAttributesToFetch = new UnmanagedArrayOfStrings(goingToFetch)) {
                    initInfo.apwzAttributeNames = unmanagedAttributesToFetch.ArrayPtr;

                    var hResult = result.Initialize(ref initInfo);

                    if (hResult != HResults.OK) {
                        throw new COMException("IDsObjectPicker.Initialize failed", hResult);
                    }

                    return result;
                }
            }
            finally {
                Marshal.FreeHGlobal(scopeInitInfosRef);
            }
        }

        private DSOPScopeFlag GetDefaultFilter()
        {
            var result = (DSOPScopeFlag) 0;

            if (((DefaultTypes & ObjectType.Users) == ObjectType.Users) ||
                ((DefaultTypes & ObjectType.WellKnownPrincipals) == ObjectType.WellKnownPrincipals)) {
                result |= DSOPScopeFlag.DefaultFilterUsers;
            }
            if (((DefaultTypes & ObjectType.Groups) == ObjectType.Groups) || ((DefaultTypes & ObjectType.BuiltInGroups) == ObjectType.BuiltInGroups)) {
                result |= DSOPScopeFlag.DefaultFilterGroups;
            }
            if ((DefaultTypes & ObjectType.Computers) == ObjectType.Computers) {
                result |= DSOPScopeFlag.DefaultFilterComputers;
            }
            if ((DefaultTypes & ObjectType.Contacts) == ObjectType.Contacts) {
                result |= DSOPScopeFlag.DefaultFilterContacts;
            }

            return result;
        }

        private DSOPFilter GetUpLevelFilter()
        {
            var result = (DSOPFilter) 0;

            if ((AllowedTypes & ObjectType.Users) == ObjectType.Users) {
                result |= DSOPFilter.Users;
            }
            if ((AllowedTypes & ObjectType.Groups) == ObjectType.Groups) {
                result |= DSOPFilter.UniversalGroupsDL | DSOPFilter.UniversalGroupsSE | DSOPFilter.GlobalGroupsDL | DSOPFilter.GlobalGroupsSE
                          | DSOPFilter.DomainLocalGroupsDL | DSOPFilter.DomainLocalGroupsSE;
            }
            if ((AllowedTypes & ObjectType.Computers) == ObjectType.Computers) {
                result |= DSOPFilter.Computers;
            }
            if ((AllowedTypes & ObjectType.Contacts) == ObjectType.Contacts) {
                result |= DSOPFilter.Contacts;
            }
            if ((AllowedTypes & ObjectType.BuiltInGroups) == ObjectType.BuiltInGroups) {
                result |= DSOPFilter.BuiltInGroups;
            }
            if ((AllowedTypes & ObjectType.WellKnownPrincipals) == ObjectType.WellKnownPrincipals) {
                result |= DSOPFilter.WellKnownPrincipals;
            }
            if (ShowAdvancedView) {
                result |= DSOPFilter.IncludeAdvancedView;
            }

            return result;
        }

        private DSOPDownLevelFilter GetDownLevelFilter()
        {
            var result = (DSOPDownLevelFilter) 0;

            if ((AllowedTypes & ObjectType.Users) == ObjectType.Users) {
                result |= DSOPDownLevelFilter.Users;
            }
            if ((AllowedTypes & ObjectType.Groups) == ObjectType.Groups) {
                result |= DSOPDownLevelFilter.LocalGroups | DSOPDownLevelFilter.GlobalGroups;
            }
            if ((AllowedTypes & ObjectType.Computers) == ObjectType.Computers) {
                result |= DSOPDownLevelFilter.Computers;
            }
            // Contacts not available in downlevel scopes
            //if ((allowedTypes & ObjectTypes.Contacts) == ObjectTypes.Contacts)
            // Exclude build in groups if not selected
            if ((AllowedTypes & ObjectType.BuiltInGroups) == 0) {
                result |= DSOPDownLevelFilter.ExcludeBuiltInGroups;
            }
            if ((AllowedTypes & ObjectType.WellKnownPrincipals) == ObjectType.WellKnownPrincipals) {
                result |= DSOPDownLevelFilter.AllWellKnownSIDs;
                // This includes all the following:
                //DSOPDownLevelFilter.World |
                //DSOPDownLevelFilter.AuthenticatedUser |
                //DSOPDownLevelFilter.Anonymous |
                //DSOPDownLevelFilter.Batch |
                //DSOPDownLevelFilter.CreatorOwner |
                //DSOPDownLevelFilter.CreatorGroup |
                //DSOPDownLevelFilter.Dialup |
                //DSOPDownLevelFilter.Interactive |
                //DSOPDownLevelFilter.Network |
                //DSOPDownLevelFilter.Service |
                //DSOPDownLevelFilter.System |
                //DSOPDownLevelFilter.TerminalServer |
                //DSOPDownLevelFilter.LocalService |
                //DSOPDownLevelFilter.NetworkService |
                //DSOPDownLevelFilter.RemoteLogon;
            }

            return result;
        }

        private DSOPScopeFlag GetProviderFlags()
        {
            var result = (DSOPScopeFlag) 0;

            if ((Providers & PathProvider.WinNT) == PathProvider.WinNT) {
                result |= DSOPScopeFlag.WantProviderWinNT;
            }

            if ((Providers & PathProvider.LDAP) == PathProvider.LDAP) {
                result |= DSOPScopeFlag.WantProviderLDAP;
            }

            if ((Providers & PathProvider.GC) == PathProvider.GC) {
                result |= DSOPScopeFlag.WantProviderGC;
            }

            if ((Providers & PathProvider.SIDPath) == PathProvider.SIDPath) {
                result |= DSOPScopeFlag.WantSIDPath;
            }

            if ((Providers & PathProvider.DownlevelBuildinPath) == PathProvider.DownlevelBuildinPath) {
                result |= DSOPScopeFlag.WantDownLevelBuiltInPath;
            }

            return result;
        }

        private static DSOPScopeType GetScope(Location locations)
        {
            var result = (DSOPScopeType) 0;

            if ((locations & Location.LocalComputer) == Location.LocalComputer) {
                result |= DSOPScopeType.TargetComputer;
            }
            if ((locations & Location.JoinedDomain) == Location.JoinedDomain) {
                result |= DSOPScopeType.DownLevelJoinedDomain | DSOPScopeType.UpLevelJoinedDomain;
            }
            if ((locations & Location.EnterpriseDomain) == Location.EnterpriseDomain) {
                result |= DSOPScopeType.EnterpriseDomain;
            }
            if ((locations & Location.GlobalCatalog) == Location.GlobalCatalog) {
                result |= DSOPScopeType.GlobalCatalog;
            }
            if ((locations & Location.ExternalDomain) == Location.ExternalDomain) {
                result |= DSOPScopeType.ExternalDownLevelDomain | DSOPScopeType.ExternalUpLevelDomain;
            }
            if ((locations & Location.Workgroup) == Location.Workgroup) {
                result |= DSOPScopeType.Workgroup;
            }
            if ((locations & Location.UserEntered) == Location.UserEntered) {
                result |= DSOPScopeType.UserEnteredDownLevelScope | DSOPScopeType.UserEnteredUpLevelScope;
            }

            return result;
        }

        private DirectoryObject[] ProcessSelections(System.Runtime.InteropServices.ComTypes.IDataObject dataObj)
        {
            const string ClipboardFormat = "CFSTR_DSOP_DS_SELECTION_LIST";

            if (dataObj == null) {
                return null;
            }

            DirectoryObject[] result = null;
            STGMEDIUM stg;
            var fe = new FORMATETC
                     {
                         cfFormat = (short) DataFormats.GetFormat(ClipboardFormat).Id,
                         ptd = IntPtr.Zero,
                         dwAspect = DVASPECT.DVASPECT_CONTENT,
                         lindex = -1,
                         tymed = TYMED.TYMED_HGLOBAL
                     };

            dataObj.GetData(ref fe, out stg);

            var dsSelectionList = Memory.NativeMethods.GlobalLock(stg.unionmember);

            if (dsSelectionList == IntPtr.Zero) {
                NativeMethods.ReportWin32Exception();
            }

            try {
                var current = dsSelectionList;
                var count = Marshal.ReadInt32(current);
                var fetchedAttributesCount = Marshal.ReadInt32(current, Marshal.SizeOf(typeof (uint)));

                if (count > 0) {
                    result = new DirectoryObject[count];
                    current += Marshal.SizeOf(typeof (uint)) * 2;

                    for (var i = 0;i < count;i++) {
                        var selection = (DSSelection) Marshal.PtrToStructure(current, typeof (DSSelection));
                        result[i] = new DirectoryObject(selection.pwzName, selection.pwzADsPath, selection.pwzClass, selection.pwzUPN,
                            GetFetchedAttributes(selection.pvarFetchedAttributes, fetchedAttributesCount, selection.pwzClass));

                        current += Marshal.SizeOf(typeof (DSSelection));
                    }
                }
            }
            finally {
                Memory.NativeMethods.GlobalUnlock(dsSelectionList);
                COM.NativeMethods.ReleaseStgMedium(ref stg);
            }

            return result;
        }

        private object[] GetFetchedAttributes(IntPtr fetchedAttributes, int fetchedAttributesCount, string schemaClassName)
        {
            var result = fetchedAttributesCount > 0 ? Marshal.GetObjectsForNativeVariants(fetchedAttributes, fetchedAttributesCount) : new object[0];

            for (var i = 0;i < result.Length;i++) {
                var largeInteger = result[i] as IADsLargeInteger;

                if (largeInteger != null) {
                    result[i] = (largeInteger.HighPart * 0x100000000L) + ((uint) largeInteger.LowPart);
                }

                if (_attributesToFetch[i].Equals("objectClass", StringComparison.OrdinalIgnoreCase)) {
                    result[i] = schemaClassName;
                }
            }

            return result;
        }
    }
}