using System.Diagnostics.Contracts;

namespace Wanderer.Library.WindowsApi.Helpers
{
    /// <summary>
    /// Contains code contracts definition for interface <see cref="IProcessManagement"/>.
    /// </summary>
    [ContractClassFor(typeof (IProcessManagement))]
    internal abstract class IProcessManagementContract : IProcessManagement
    {
        //
    }
}