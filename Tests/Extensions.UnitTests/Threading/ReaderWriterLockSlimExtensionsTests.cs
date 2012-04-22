using System;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Wanderer.Library.Extensions.UnitTests.Threading
{
    [TestClass]
    // ReSharper disable InconsistentNaming
    public class ReaderWriterLockSlimExtensionsTests
    {
        #region Variables
        private ReaderWriterLockSlim _locker;
        #endregion

        #region GetReadLock
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetReadLock_NullLocker_Fail()
        {
            using (((ReaderWriterLockSlim) null).GetReadLock()) {}
        }

        [TestMethod]
        [ExpectedException(typeof (LockRecursionException))]
        public void GetReadLock_TryRead_Fail()
        {
            using (_locker.GetReadLock())
                _locker.TryEnterReadLock(0);
        }

        [TestMethod]
        [ExpectedException(typeof (LockRecursionException))]
        public void GetReadLock_TryEnterUpgradeableRead_Fail()
        {
            using (_locker.GetReadLock())
                _locker.TryEnterUpgradeableReadLock(0);
        }

        [TestMethod]
        [ExpectedException(typeof (LockRecursionException))]
        public void GetReadLock_TryWrite_Fail()
        {
            using (_locker.GetReadLock())
                _locker.TryEnterWriteLock(0);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetUpgradeableReadLock_NullLocker_Fail()
        {
            using (((ReaderWriterLockSlim) null).GetUpgradeableReadLock()) {}
        }

        [TestMethod]
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

        [TestMethod]
        [ExpectedException(typeof (LockRecursionException))]
        public void GetUpgradeableReadLock_TryEnterUpgradeableRead_Fail()
        {
            using (_locker.GetUpgradeableReadLock())
                _locker.TryEnterUpgradeableReadLock(0);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetWriteLock_NullLocker_Fail()
        {
            using (((ReaderWriterLockSlim) null).GetWriteLock()) {}
        }

        [TestMethod]
        [ExpectedException(typeof(LockRecursionException))]
        public void GetWriteLock_TryRead_Fail()
        {
            using (_locker.GetWriteLock())
                _locker.TryEnterReadLock(0);
        }

        [TestMethod]
        [ExpectedException(typeof (LockRecursionException))]
        public void GetWriteLock_TryEnterUpgradeableRead_Fail()
        {
            using (_locker.GetWriteLock())
                _locker.TryEnterUpgradeableReadLock(0);
        }

        [TestMethod]
        [ExpectedException(typeof(LockRecursionException))]
        public void GetWriteLock_TryWrite_Fail()
        {
            using (_locker.GetWriteLock())
                _locker.TryEnterWriteLock(0);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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
        [TestInitialize]
        public void TestInitialize()
        {
            _locker = new ReaderWriterLockSlim();
        }
        #endregion
    }
    // ReSharper restore InconsistentNaming
}
