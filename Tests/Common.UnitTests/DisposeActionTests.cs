using System;

using NUnit.Framework;

namespace Wanderer.Library.Common.UnitTests
{
    [TestFixture]
    public class DisposeActionTests
    {
// ReSharper disable InconsistentNaming
        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Constructor_NullAction_ThrowsException()
        {
            using (new DisposeAction(null)) {}
        }

        [Test]
        public void Constructor_NotNullAction_Ok()
        {
            using (new DisposeAction(() => { })) {}
        }

        [Test]
        public void Dispose_CallDisposeAction()
        {
            var actionCalled = false;

            using (new DisposeAction(() => { actionCalled = true; })) {}

            Assert.IsTrue(actionCalled);
        }
// ReSharper restore InconsistentNaming
    }
}