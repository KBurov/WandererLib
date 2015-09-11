using System.Diagnostics.Contracts;

using Wanderer.Library.Common;
// ReSharper disable once CheckNamespace
namespace System.Threading
{
    /// <summary>
    /// Extension methods for using <see cref="ReaderWriterLockSlim"/> class with <see cref="DisposeAction"/>.
    /// </summary>
    public static class ReaderWriterLockSlimExtensions
    {
        private const string LockerNullErrorMessage = "locker cannot be null";
        /// <summary>
        /// Get a <see cref="DisposeAction"/> object for read lock from <see cref="ReaderWriterLockSlim"/> object.
        /// </summary>
        /// <param name="locker"><see cref="ReaderWriterLockSlim"/> object</param>
        /// <returns><see cref="DisposeAction"/> object</returns>
        public static IDisposable GetReadLock(this ReaderWriterLockSlim locker)
        {
            Contract.Requires<ArgumentNullException>(locker != null, LockerNullErrorMessage);

            locker.EnterReadLock();

            return new DisposeAction(locker.ExitReadLock);
        }

        /// <summary>
        /// Get a <see cref="DisposeAction"/> object for upgradeable read lock from <see cref="ReaderWriterLockSlim"/> object.
        /// </summary>
        /// <param name="locker"><see cref="ReaderWriterLockSlim"/> object</param>
        /// <returns><see cref="DisposeAction"/> object</returns>
        public static IDisposable GetUpgradeableReadLock(this ReaderWriterLockSlim locker)
        {
            Contract.Requires<ArgumentNullException>(locker != null, LockerNullErrorMessage);

            locker.EnterUpgradeableReadLock();

            return new DisposeAction(locker.ExitUpgradeableReadLock);
        }

        /// <summary>
        /// Get a <see cref="DisposeAction"/> object for write lock from <see cref="ReaderWriterLockSlim"/> object.
        /// </summary>
        /// <param name="locker"><see cref="ReaderWriterLockSlim"/> object</param>
        /// <returns><see cref="DisposeAction"/> object</returns>
        public static IDisposable GetWriteLock(this ReaderWriterLockSlim locker)
        {
            Contract.Requires<ArgumentNullException>(locker != null, LockerNullErrorMessage);

            locker.EnterWriteLock();

            return new DisposeAction(locker.ExitWriteLock);
        }
    }
}