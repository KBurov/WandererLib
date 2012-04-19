using System.Diagnostics.Contracts;

using Wanderer.Library.Common;

// ReSharper disable CheckNamespace
namespace System.Threading
// ReSharper restore CheckNamespace
{
    public static class ReaderWriterLockSlimExtensions
    {
        public static IDisposable GetReadLock(this ReaderWriterLockSlim locker)
        {
            Contract.Requires<ArgumentNullException>(locker != null);

            locker.EnterReadLock();

            return new DisposeAction(locker.ExitReadLock);
        }

        public static IDisposable GetUpgradeableReadLock(this ReaderWriterLockSlim locker)
        {
            Contract.Requires<ArgumentNullException>(locker != null);

            locker.EnterUpgradeableReadLock();

            return new DisposeAction(locker.ExitUpgradeableReadLock);
        }

        public static IDisposable GetWriteLock(this ReaderWriterLockSlim locker)
        {
            Contract.Requires<ArgumentNullException>(locker != null);

            locker.EnterWriteLock();

            return new DisposeAction(locker.ExitWriteLock);
        }
    }
}
