﻿using System;
using System.Diagnostics.Contracts;

namespace Wanderer.Library.Common
{
    /// <summary>
    /// Represents information about an operating system, such as the version and platform identifier.
    /// </summary>
    [Serializable]
    public sealed class OperatingSystemEx
    {
        #region Variables
        private readonly PlatformID _platform;
        private readonly Version _version;
        private readonly string _servicePack;
        private readonly bool _is64BitPlatform;

        private volatile string _versionString;
        private readonly object _versionStringLock = new object();
        #endregion

        /// <summary>
        /// Gets a <see cref="PlatformID"/> enumeration value that identifies the operating system platform.
        /// </summary>
        /// <returns>one of the <see cref="PlatformID"/> values</returns>
        public PlatformID Platform { get { return _platform; } }

        /// <summary>
        /// Gets a <see cref="Version"/> object that identifies the operating system.
        /// </summary>
        /// <returns>a <see cref="Version"/> object that describes the major version, minor version, build, and revision numbers for the operating system</returns>
        public Version Version { get { return _version; } }

        /// <summary>
        /// Gets the service pack version represented by this <see cref="OperatingSystemEx"/> object.
        /// </summary>
        /// <returns>the service pack version, if service packs are supported and at least one is installed; otherwise, an empty string ("")</returns>
        public string ServicePack { get { return _servicePack ?? string.Empty; } }

        /// <summary>
        /// Determines whether the operating system is a 64-bit operating system.
        /// </summary>
        public bool Is64BitPlatform { get { return _is64BitPlatform; } }

        /// <summary>
        /// Gets the concatenated string representation of the platform identifier, version, and service pack that are currently installed on the operating system.
        /// </summary>
        /// <returns>the string representation of the values returned by the <see cref="OperatingSystemEx.Platform"/>, <see cref="OperatingSystemEx.Version"/>
        /// and <see cref="OperatingSystemEx.ServicePack"/> properties</returns>
        public string VersionString
        {
            get
            {
                if (_versionString == null) {
                    lock (_versionStringLock) {
                        if (_versionString == null) {
                            string str;

                            switch (_platform) {
                                case PlatformID.Win32S:
                                    str = "Microsoft Win32S ";
                                    break;
                                case PlatformID.Win32Windows:
                                    str = _version.Major > 4 || _version.Major == 4 && _version.Minor > 0
                                        ? "Microsoft Windows 98 "
                                        : "Microsoft Windows 95 ";
                                    break;
                                case PlatformID.Win32NT:
                                    str = "Microsoft Windows NT ";
                                    break;
                                case PlatformID.WinCE:
                                    str = "Microsoft Windows CE ";
                                    break;
                                case PlatformID.MacOSX:
                                    str = "Mac OS X ";
                                    break;
                                default:
                                    str = "<unknown> ";
                                    break;
                            }

                            _versionString = !string.IsNullOrEmpty(_servicePack) ? str + _version.ToString(3) + " " + _servicePack : str + _version;
                        }
                    }
                }

                return _versionString;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperatingSystemEx"/> class, using the specified platform identifier value, version object and service pack string.
        /// </summary>
        /// <param name="platform">one of the <see cref="PlatformID"/> values that indicates the operating system platform</param>
        /// <param name="version">a <see cref="Version"/> object that indicates the version of the operating system</param>
        /// <param name="servicePack">a service pack string description</param>
        /// <param name="is64BitPlatform">a value which determines whether the operating system is a 64-bit operating system.</param>
        /// <exception cref="ArgumentException"><paramref name="platform"/> is not a <see cref="PlatformID"/> enumeration value</exception>
        /// <exception cref="ArgumentNullException"><paramref name="version"/> is null</exception>
        public OperatingSystemEx(PlatformID platform, Version version, string servicePack, bool is64BitPlatform)
        {
            Contract.Requires<ArgumentException>(platform >= PlatformID.Win32S && platform <= PlatformID.MacOSX);
            Contract.Requires<ArgumentNullException>(version != null);

            _platform = platform;
            _version = version;
            _servicePack = servicePack;
            _is64BitPlatform = is64BitPlatform;
        }

        /// <summary>
        /// Converts the value of this <see cref="OperatingSystemEx"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>the string representation of the values returned by the <see cref="OperatingSystemEx.Platform"/>, <see cref="OperatingSystemEx.Version"/>
        /// and <see cref="OperatingSystemEx.ServicePack"/> properties</returns>
        public override string ToString()
        {
            return VersionString;
        }
    }
}