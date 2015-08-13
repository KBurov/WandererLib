using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Management;

namespace Wanderer.Library.WindowsApi.Helpers.ObjectPicker
{
    /// <summary>
    /// Represents a dialog that allows a user to select users.
    /// </summary>
    public sealed class EntityPickerDialog : ObjectPickerDialog
    {
        private const string EntityNameFormat = "{0}\\{1}";

        private IList<string> _selectedEntities;

        /// <summary>
        /// List of selected users.
        /// </summary>
        public IList<string> SelectedEntities => _selectedEntities ?? (_selectedEntities = GetSelectedEntities());

        /// <summary>
        /// Default/Initialize constructor.
        /// </summary>
        /// <param name="dialogType">determines the behaviour of dialog (select user or user groups)</param>
        public EntityPickerDialog(EntityPickerDialogType dialogType = EntityPickerDialogType.Users)
        {
            AllowedTypes = dialogType == EntityPickerDialogType.Users ? ObjectType.Users : ObjectType.Groups;
            DefaultLocations = Environment.UserDomainName == Environment.MachineName ? Location.LocalComputer : Location.JoinedDomain;
        }

        /// <summary>
        /// Displays the Object Picker (Active Directory) common dialog, when called by ShowDialog.
        /// </summary>
        /// <param name="hwndOwner">handle to the window that owns the dialog</param>
        /// <returns>if the user clicks the OK button of the Object Picker dialog that is displayed, true is returned; otherwise, false.</returns>
        protected override bool RunDialog(IntPtr hwndOwner)
        {
            _selectedEntities = null;

            return base.RunDialog(hwndOwner);
        }

        private IList<string> GetSelectedEntities()
        {
            var result = new List<string>();

            result.AddRange(Environment.UserDomainName.Equals(Environment.MachineName)
                ? SelectedObjects.Select(o => string.Format(EntityNameFormat, Environment.MachineName, o.Name))
                : SelectedObjects.Select(o => GetEntityNameFromPath(o.Path)));

            return result;
        }

        private static string GetEntityNameFromPath(string path)
        {
            var tempPath = string.Empty;
            var isAD = false;
            var domainName = string.Empty;
            string accountName;

            if (path.IndexOf("LDAP://", StringComparison.InvariantCultureIgnoreCase) == 0) {
                tempPath = path.Substring(7);
                isAD = true;
            }
            else if (path.IndexOf("WinNT://", StringComparison.InvariantCultureIgnoreCase) == 0) {
                tempPath = path.Substring(8);
            }

            var parts = tempPath.Split('/');

            if (isAD) {
                using (var ldapUser = new DirectoryEntry(path)) {
                    var count = ldapUser.Properties["samAccountName"].Count;

                    accountName = ldapUser.Properties["samAccountName"][count - 1].ToString();

                    if (parts[0] != null) {
                        if (parts[0].IndexOf(Environment.MachineName, StringComparison.InvariantCultureIgnoreCase) >= 0) {
                            domainName = GetDomainName(parts[0]);
                        }
                        else {
                            if (IsMachineInDomain()) {
                                domainName = GetMachineWorkgroupDomainName();
                            }
                        }
                    }
                }
            }
            else {
                if (tempPath.IndexOf(Environment.MachineName, StringComparison.InvariantCultureIgnoreCase) >= 0) {
                    domainName = parts[1];
                    accountName = parts[2];
                }
                else {
                    domainName = parts[0];
                    accountName = parts[1];
                }
            }

            return string.Format(EntityNameFormat, domainName, accountName);
        }

        private static string GetDomainName(string dnsName)
        {
            string defaultNamingContext;
            string rootDomainNamingContext;

            using (var rootDSE = new DirectoryEntry("LDAP://RootDSE")) {
                defaultNamingContext = rootDSE.Properties["defaultNamingContext"].Value.ToString();
                rootDomainNamingContext = rootDSE.Properties["rootDomainNamingContext"].Value.ToString();
            }

            using (
                var domainRoot = defaultNamingContext.Equals(rootDomainNamingContext, StringComparison.InvariantCultureIgnoreCase)
                    ? new DirectoryEntry($"LDAP://CN=Partitions,CN=Configuration,{defaultNamingContext}")
                    : new DirectoryEntry($"LDAP://CN=Partitions,CN=Configuration,{rootDomainNamingContext}")) {
                try {
                    foreach (DirectoryEntry c in domainRoot.Children) {
                        try {
                            if (c.Properties["dnsRoot"].Value.ToString().Equals(dnsName, StringComparison.InvariantCultureIgnoreCase)) {
                                return c.Properties["NetBIOSName"].Value.ToString();
                            }
                        }
// ReSharper disable EmptyGeneralCatchClause
                        catch {}
// ReSharper restore EmptyGeneralCatchClause
                    }
                }
// ReSharper disable EmptyGeneralCatchClause
                catch {}
// ReSharper restore EmptyGeneralCatchClause
            }

            return string.Empty;
        }

        private static bool IsMachineInDomain()
        {
            try {
                var query = new SelectQuery("Win32_ComputerSystem");

                using (var searcher = new ManagementObjectSearcher(query)) {
                    var results = searcher.Get();

                    return results.Cast<ManagementBaseObject>().Any(o => (bool) o["partofdomain"]);
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch {}
// ReSharper restore EmptyGeneralCatchClause
            return false;
        }

        private static string GetMachineWorkgroupDomainName()
        {
            var result = string.Empty;

            try {
                var query = new SelectQuery("Win32_ComputerSystem");

                using (var searcher = new ManagementObjectSearcher(query)) {
                    var results = searcher.Get();

                    foreach (var mo in results) {
                        var managementObject = mo as ManagementObject;

                        if (managementObject != null) {
                            result = (bool) managementObject["partofdomain"] ? managementObject["domain"].ToString() : managementObject["workgroup"].ToString();
                        }
                    }
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch {}
// ReSharper restore EmptyGeneralCatchClause
            try {
                if (result.Contains(".")) {
                    result = result.Split('.')[0];
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch {}
// ReSharper restore EmptyGeneralCatchClause
            return result;
        }
    }
}