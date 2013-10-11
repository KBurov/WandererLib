using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Wanderer.Library.Common.UnitTests
{
    [TestClass]
// ReSharper disable InconsistentNaming
    public class DisposeActionTests
    {
        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Constructor_NullAction_ThrowsException()
        {
            using (new DisposeAction(null)) {}
        }

        [TestMethod]
        public void Constructor_NotNullAction_Ok()
        {
            using (new DisposeAction(() => { })) {}
        }

        [TestMethod]
        public void Dispose_CallDisposeAction()
        {
            var actionCalled = false;

            using (new DisposeAction(() => { actionCalled = true; })) {}

            Assert.IsTrue(actionCalled);
        }
    }
// ReSharper restore InconsistentNaming
}