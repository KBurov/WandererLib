using System;

using NUnit.Framework;

using Wanderer.Library.TestFramework;

namespace Wanderer.Library.Common.UnitTests.given_Retry.with_not_null_action.with_numberOfRetries_greater_than_zero
{
    [Category(TestCategory.Unit)]
// ReSharper disable once InconsistentNaming
    public class when_call_times : ContextBase
    {
        private const uint NumberOfRetries = 3u;

        private bool _actionCalled;
        private Action _action;
        private Action _otherAction;
        private uint _actionRetries = NumberOfRetries;
        private Func<int> _func;
        private Func<int> _otherFunc;
        private uint _funcRetries = NumberOfRetries;

        protected override void Act()
        {
            _action = () => _actionCalled = true;
            _otherAction = () =>
                {
                    if (--_actionRetries > 0u) {
                        throw new Exception();
                    }

                    _actionCalled = true;
                };
            _func = () =>
                {
                    _actionCalled = true;

                    return 1;
                };
            _otherFunc = () =>
                {
                    if (--_funcRetries > 0u) {
                        throw new Exception();
                    }

                    _actionCalled = true;

                    return 1;
                };
            _actionCalled = false;
        }

        [Test]
        public void then_action_called_at_least_one_time()
        {
            Retry.Times(_action, 1u);

            Assert.IsTrue(_actionCalled);
        }

        [Test]
        public void then_action_called_at_least_one_time_generic()
        {
            Retry.Times(_func, 1u);

            Assert.IsTrue(_actionCalled);
        }

        [Test]
        public void then_action_called_exact_times()
        {
            Retry.Times(_otherAction, NumberOfRetries);

            Assert.IsTrue(_actionCalled);
            Assert.AreEqual(_actionRetries, 0u);
        }

        [Test]
        public void then_action_called_exact_times_generic()
        {
            Retry.Times(_otherFunc, NumberOfRetries);

            Assert.IsTrue(_actionCalled);
            Assert.AreEqual(_funcRetries, 0u);
        }
    }
}