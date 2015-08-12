using System;
using System.Management;
using System.Text;

namespace Wanderer.Library.Common
{
    /// <summary>
    /// Provides additional information and methods to <see cref="Environment" /> class.
    /// </summary>
    public static class EnvironmentEx
    {
        private const string InternalServicePackNumberVariableName = "ServicePackNumber";
        private const string DefaultServicePackNumber = "0";

        /// <summary>
        /// Variable name for operating system service pack version number.
        /// </summary>
        public const string ServicePackNumberVariableName = "%{InternalServicePackNumberVariableName}%";

        /// <summary>
        /// Replaces the name of each environment variable embedded in the specified string with the string equivalent of the value of the variable,
        /// then returns the resulting string.
        /// </summary>
        /// <param name="name">
        /// a string containing the names of zero or more environment variables;
        /// each environment variable is quoted with the percent sign character (%)
        /// </param>
        /// <returns>a string with each environment variable replaced by its value</returns>
        public static string ExpandEnvironmentVariables(string name)
        {
            var nameParts = name.Split('%');
            var builder = new StringBuilder(nameParts[0]);

            for (var i = 1;i < nameParts.Length - 1;++i) {
                if ((i % 2 == 0) || string.IsNullOrWhiteSpace(nameParts[i])) {
                    continue;
                }

                builder.Append(InternalExpandEnvironmentVariable(nameParts[i]));
            }

            if (nameParts.Length > 2) {
                builder.Append(nameParts[nameParts.Length - 1]);
            }

            return Environment.ExpandEnvironmentVariables(builder.ToString());
        }

        private static string InternalExpandEnvironmentVariable(string name)
        {
            if (name.Equals(InternalServicePackNumberVariableName, StringComparison.InvariantCultureIgnoreCase)) {
                var query = new SelectQuery("Win32_OperatingSystem");

                using (var searcher = new ManagementObjectSearcher(query)) {
                    foreach (var mo in searcher.Get()) {
                        var managementObject = mo as ManagementObject;

                        if (managementObject != null) {
                            return mo["ServicePackMajorVersion"].ToString();
                        }
                    }
                }

                return DefaultServicePackNumber;
            }

            return "%{name}%";
        }
    }
}