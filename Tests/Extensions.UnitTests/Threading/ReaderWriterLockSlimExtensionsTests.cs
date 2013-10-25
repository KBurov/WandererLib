using System;
using System.Threading;

using NUnit.Framework;

namespace Wanderer.Library.Extensions.UnitTests.Threading
{
    [TestFixture]
    public class ReaderWriterLockSlimExtensionsTests
    {
        #region Variables
        private ReaderWriterLockSlim _locker;
        #endregion
// ReSharper disable InconsistentNaming
        #region GetReadLock
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetReadLock_NullLocker_Fail()
        {
            using (((ReaderWriterLockSlim) null).GetReadLock()) {}
        }

        [Test]
        [ExpectedException(typeof (LockRecursionException))]
        public void GetReadLock_TryRead_Fail()
        {
            using (_locker.GetReadLock())
                _locker.TryEnterReadLock(0);
        }

        [Test]
        [ExpectedException(typeof (LockRecursionException))]
        public void GetReadLock_TryEnterUpgradeableRead_Fail()
        {
            using (_locker.GetReadLock())
                _locker.TryEnterUpgradeableReadLock(0);
        }

        [Test]
        [ExpectedException(typeof (LockRecursionException))]
        public void GetReadLock_TryWrite_Fail()
        {
            using (_locker.GetReadLock())
                _locker.TryEnterWriteLock(0);
        }

        [Test]
        public void GetReadLock_TryReadFromOtherThread_Success()
        {
            Func<bool> a = () =>
                               {
                                   var result = _locker.TryEnterReadLock(0);
                                   if (result) _locker.ExitReadLock();
                                   return result;
                               };
            bool tryReadResult;

            using (_locker.GetReadLock())
                tryReadResult = a.EndInvoke(a.BeginInvoke(null, null));

            Assert.IsTrue(tryReadResult);
        }

        [Test]
        public void GetReadLock_TryEnterUpgradeableReadFromOtherThread_Success()
        {
            Func<bool> a = () =>
                               {
                                   var result = _locker.TryEnterUpgradeableReadLock(0);
                                   if (result) _locker.ExitUpgradeableReadLock();
                                   return result;
                               };
            bool tryUpgradeableReadResult;

            using (_locker.GetReadLock())
                tryUpgradeableReadResult = a.EndInvoke(a.BeginInvoke(null, null));

            Assert.IsTrue(tryUpgradeableReadResult);
        }

        [Test]
        public void GetReadLock_TryWriteFromOtherThread_Fail()
        {
            Func<bool> a = () =>
                               {
                                   var result = _locker.TryEnterWriteLock(0);
                                   if (result) _locker.ExitWriteLock();
                                   return result;
                               };
            bool tryWriteResult;

            using (_locker.GetReadLock())
                tryWriteResult = a.EndInvoke(a.BeginInvoke(null, null));

            Assert.IsFalse(tryWriteResult);
        }
        #endregion

        #region GetUpgradeableReadLock
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUpgradeableReadLock_NullLocker_Fail()
        {
            using (((ReaderWriterLockSlim) null).GetUpgradeableReadLock()) {}
        }

        [Test]
        public void GetUpgradeableReadLock_TryRead_Success()
        {
            bool tryReadResult;

            using (_locker.GetUpgradeableReadLock())
            {
                tryReadResult = _locker.TryEnterReadLock(0);

                if (tryReadResult)
                    _locker.ExitReadLock();
            }

            Assert.IsTrue(tryReadResult);
        }

        [Test]
        [ExpectedException(typeof (LockRecursionException))]
        public void GetUpgradeableReadLock_TryEnterUpgradeableRead_Fail()
        {
            using (_locker.GetUpgradeableReadLock())
                _locker.TryEnterUpgradeableReadLock(0);
        }

        [Test]
        public void GetUpgradeableReadLock_TryWrite_Success()
        {
            bool tryWriteResult;

            using (_locker.GetUpgradeableReadLock())
            {
                tryWriteResult = _locker.TryEnterWriteLock(0);

                if (tryWriteResult)
                    _locker.ExitWriteLock();
            }

            Assert.IsTrue(tryWriteResult);
        }

        [Test]
        public void GetUpgradeableReadLock_TryReadFromOtherThread_Success()
        {
            Func<bool> a = () =>
                               {
                                   var result = _locker.TryEnterReadLock(0);
                                   if (result) _locker.ExitReadLock();
                                   return result;
                               };
            bool tryReadResult;

            using (_locker.GetUpgradeableReadLock())
                tryReadResult = a.EndInvoke(a.BeginInvoke(null, null));

            Assert.IsTrue(tryReadResult);
        }

        [Test]
        public void GetUpgradeableReadLock_TryEnterUpgradeableReadFromOtherThread_Fail()
        {
            Func<bool> a = () =>
                               {
                                   var result = _locker.TryEnterUpgradeableReadLock(0);
                                   if (result) _locker.ExitUpgradeableReadLock();
                                   return result;
                               };
            bool tryUpgradeableReadResult;

            using (_locker.GetUpgradeableReadLock())
                tryUpgradeableReadResult = a.EndInvoke(a.BeginInvoke(null, null));

            Assert.IsFalse(tryUpgradeableReadResult);
        }

        [Test]
        public void GetUpgradeableReadLock_TryWriteFromOtherThread_Fail()
        {
            Func<bool> a = () =>
                               {
                                   var result = _locker.TryEnterWriteLock(0);
                                   if (result) _locker.ExitWriteLock();
                                   return result;
                               };
            bool tryWriteResult;

            using (_locker.GetUpgradeableReadLock())
                tryWriteResult = a.EndInvoke(a.BeginInvoke(null, null));

            Assert.IsFalse(tryWriteResult);
        }
        #endregion

        #region GetWriteLock
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetWriteLock_NullLocker_Fail()
        {
            using (((ReaderWriterLockSlim) null).GetWriteLock()) {}
        }

        [Test]
        [ExpectedException(typeof(LockRecursionException))]
        public void GetWriteLock_TryRead_Fail()
        {
            using (_locker.GetWriteLock())
                _locker.TryEnterReadLock(0);
        }

        [Test]
        [ExpectedException(typeof (LockRecursionException))]
        public void GetWriteLock_TryEnterUpgradeableRead_Fail()
        {
            using (_locker.GetWriteLock())
                _locker.TryEnterUpgradeableReadLock(0);
        }

        [Test]
        [ExpectedException(typeof(LockRecursionException))]
        public void GetWriteLock_TryWrite_Fail()
        {
            using (_locker.GetWriteLock())
                _locker.TryEnterWriteLock(0);
        }

        [Test]
        public void GetWriteLock_TryReadFromOtherThread_Fail()
        {
            Func<bool> a = () =>
                               {
                                   var result = _locker.TryEnterReadLock(0);
                                   if (result) _locker.ExitReadLock();
                                   return result;
                               };
            bool tryReadResult;

            using (_locker.GetWriteLock())
                tryReadResult = a.EndInvoke(a.BeginInvoke(null, null));

            Assert.IsFalse(tryReadResult);
        }

        [Test]
        public void GetWriteLock_TryEnterUpgradeableReadFromOtherThread_Fail()
        {
            Func<bool> a = () =>
                               {
                                   var result = _locker.TryEnterUpgradeableReadLock(0);
                                   if (result) _locker.ExitUpgradeableReadLock();
                                   return result;
                               };
            bool tryUpgradeableReadResult;

            using (_locker.GetWriteLock())
                tryUpgradeableReadResult = a.EndInvoke(a.BeginInvoke(null, null));

            Assert.IsFalse(tryUpgradeableReadResult);
        }

        [Test]
        public void GetWriteLock_TryWriteFromOtherThread_Fail()
        {
            Func<bool> a = () =>
                               {
                                   var result = _locker.TryEnterWriteLock(0);
                                   if (result) _locker.ExitWriteLock();
                                   return result;
                               };
            bool tryWriteResult;

            using (_locker.GetWriteLock())
                tryWriteResult = a.EndInvoke(a.BeginInvoke(null, null));

            Assert.IsFalse(tryWriteResult);
        }
        #endregion

        #region Additional test attributes
        [TestFixtureSetUp]
        public void TestInitialize()
        {
            _locker = new ReaderWriterLockSlim();
        }
        #endregion
// ReSharper restore InconsistentNaming
    }
}