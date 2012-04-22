using System.Diagnostics.Contracts;

using Wanderer.Library.Common;

// ReSharper disable CheckNamespace
namespace System.Threading
// ReSharper restore CheckNamespace
{
    /// <summary>
    /// Extension methods for using <see cref="ReaderWriterLockSlim"/> class with <see cref="DisposeAction"/>.
    /// </summary>
    public static class ReaderWriterLockSlimExtensions
    {
        /// <summary>
        /// Get a <see cref="DisposeAction"/> object for read lock from <see cref="ReaderWriterLockSlim"/> object.
        /// </summary>
        /// <param name="locker"><see cref="ReaderWriterLockSlim"/> object</param>
        /// <returns><see cref="DisposeAction"/> object</returns>
        public static IDisposable GetReadLock(this ReaderWriterLockSlim locker)
        {
            Contract.Requires<ArgumentNullException>(locker != null);

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
            Contract.Requires<ArgumentNullException>(locker != null);

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
            Contract.Requires<ArgumentNullException>(locker != null);

            locker.EnterWriteLock();

            return new DisposeAction(locker.ExitWriteLock);
        }
    }
}
