using Wanderer.Library.Common;

namespace System.Threading
{
    public static class ReaderWriterLockSlimExtensions
    {
        public static IDisposable GetReadLock(this ReaderWriterLockSlim locker)
        {
            locker.EnterReadLock();

            return new DisposeAction(locker.ExitReadLock);
        }

        public static IDisposable GetUpgradeableReadLock(this ReaderWriterLockSlim locker)
        {
            locker.EnterUpgradeableReadLock();

            return new DisposeAction(locker.ExitUpgradeableReadLock);
        }

        public static IDisposable GetWriteLock(this ReaderWriterLockSlim locker)
        {
            locker.EnterWriteLock();

            return new DisposeAction(locker.ExitWriteLock);
        }
    }
}
