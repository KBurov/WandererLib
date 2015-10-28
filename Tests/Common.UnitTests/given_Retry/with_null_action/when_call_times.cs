using System;

using NUnit.Framework;

using Wanderer.Library.TestFramework;

namespace Wanderer.Library.Common.UnitTests.given_Retry.with_null_action
{
    [TestFixture]
    [Category(TestCategory.Unit)]
    // ReSharper disable InconsistentNaming
    public class when_call_times : ContextBase
    {
        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void then_exception_occurs()
        {
            Retry.Times(null, 1u);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void then_exception_occurs_generic()
        {
            Retry.Times((Func<int>) null, 1u);
        }
    }
}