using System;

using NUnit.Framework;

using Wanderer.Library.TestFramework;

namespace Wanderer.Library.Common.UnitTests.given_DisposeAction.with_null_action
{
    [TestFixture]
    [Category(TestCategory.Unit)]
// ReSharper disable once InconsistentNaming
    public class when_call_constructor
    {
        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void then_exception_occurs()
        {
            using (new DisposeAction(null)) {}
        }
    }
}