using System.Diagnostics.Contracts;

namespace Wanderer.Library.Finance
{
    /// <summary>
    /// Account interface.
    /// </summary>
    [ContractClass(typeof(AccountContract<>))]
    public interface IAccount<T> where T : ICurrency
    {
        /// <summary>
        /// Account name.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Account type.
        /// </summary>
        IAccountType Type { get; }
        /// <summary>
        /// Account balance.
        /// </summary>
        IMoney<T> Balance { get; }

        /// <summary>
        /// Withdraw money.
        /// </summary>
        /// <param name="money">amount of money</param>
        void Withdraw(IMoney<T> money);

        /// <summary>
        /// Deposit money.
        /// </summary>
        /// <param name="money">amount of money</param>
        void Deposit(IMoney<T> money);
    }
}
