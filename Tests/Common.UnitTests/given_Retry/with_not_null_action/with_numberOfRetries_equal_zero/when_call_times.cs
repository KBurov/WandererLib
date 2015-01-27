using System;

using NUnit.Framework;

using Wanderer.Library.TestFramework;

namespace Wanderer.Library.Common.UnitTests.given_Retry.with_not_null_action.with_numberOfRetries_equal_zero
{
    [Category(TestCategory.Unit)]
// ReSharper disable once InconsistentNaming
    public class when_call_times : ContextBase
    {
        private bool _actionCalled;
        private Action _action;
        private Func<int> _func;

        protected override void Act()
        {
            _action = () => _actionCalled = true;
            _func = () =>
                {
                    _actionCalled = true;

                    return 1;
                };
            _actionCalled = false;
        }

        [Test]
        public void then_action_does_not_called()
        {
            Retry.Times(_action, 0u);

            Assert.IsFalse(_actionCalled);
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void then_exception_occurs_generic()
        {
            Retry.Times(_func, 0u);
        }
    }
}