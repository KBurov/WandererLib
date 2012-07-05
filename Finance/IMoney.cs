namespace Wanderer.Library.Finance
{
    /// <summary>
    /// Money interface.
    /// </summary>
    /// <typeparam name="T">currency type</typeparam>
    public interface IMoney<T> where T : ICurrency
    {
        /// <summary>
        /// Amount of money.
        /// </summary>
        decimal Amount { get; }
        /// <summary>
        /// Money currency.
        /// </summary>
        T Currency { get; }

        /// <summary>
        /// Summarize the money.
        /// </summary>
        /// <param name="money">amount of money</param>
        /// <returns>sum of <see cref="Amount"/> and money</returns>
        IMoney<T> Add(IMoney<T> money);

        /// <summary>
        /// Subtract the money.
        /// </summary>
        /// <param name="money">amount of money</param>
        /// <returns>subtract of <see cref="Amount"/> and money</returns>
        IMoney<T> Subtract(IMoney<T> money);
    }
}
