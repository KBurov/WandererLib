using System;

using NUnit.Framework;

using Wanderer.Library.TestFramework;

namespace Wanderer.Library.Common.UnitTests.given_DisposeAction.with_not_null_action
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable"), TestFixture]
    [Category(TestCategory.Unit)]
// ReSharper disable once InconsistentNaming
    public class when_call_dispose : ContextBase
    {
        private IDisposable _disposeAction;
        private bool _actionCalled;

        protected override void Act()
        {
            _disposeAction = new DisposeAction(() => { _actionCalled = true; });
            _actionCalled = false;
        }

        [Test]
        public void then_action_called()
        {
            using (_disposeAction) {}

            Assert.IsTrue(_actionCalled);
        }
    }
}