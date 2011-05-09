using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Wanderer.Library.Common;

namespace Common.UnitTests
{
    [TestClass]
    public class DisposeActionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_NullAction_ThrowsException()
        {
            new DisposeAction(null);
        }

        [TestMethod]
        public void Constructor_NotNullAction_Ok()
        {
            new DisposeAction(() => { });
        }

        [TestMethod]
        public void Dispose_CallDisposeAction()
        {
            var actionCalled = false;

            using (new DisposeAction(() => { actionCalled = true; }))
            {
            }

            Assert.IsTrue(actionCalled);
        }
    }
}
