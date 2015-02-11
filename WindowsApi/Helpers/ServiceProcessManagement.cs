using System;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Contains set of functions for process management for windows services.
    /// </summary>
    public sealed class ServiceProcessManagement// : IProcessManagement
    {
        private const int ProcessCloseTimeoutInMilliseconds = 250;

        //private static readonly Lazy<IProcessManagement> ProcessManagementLazy = new Lazy<IProcessManagement>(() => new ServiceProcessManagement());

        ///// <summary>
        ///// Default implementer for <see cref="IProcessManagement"/> interface.
        ///// </summary>
        //public static IProcessManagement Management { get { return ProcessManagementLazy.Value; } }
    }
}