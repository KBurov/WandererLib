using System.Diagnostics.Contracts;

namespace Wanderer.Library.Finance
{
    /// <summary>
    /// Account type interface.
    /// </summary>
    [ContractClass(typeof(AccountTypeContract))]
    public interface IAccountType
    {
        /// <summary>
        /// Type name.
        /// </summary>
        string Name { get; }
    }
}
