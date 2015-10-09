using NUnit.Framework;

namespace Wanderer.Library.TestFramework
{
    /// <summary>
    /// Base class for unit or integration tests context classes with complex setup.
    /// </summary>
    [TestFixture]
    public abstract class StaticContextBase
    {
        /// <summary>
        /// Run test action (<see cref="Act"/>).
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            Act();
        }

        /// <summary>
        /// Setting up the test fixture (<see cref="Arrange"/>).
        /// </summary>
        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Arrange();
        }

        /// <summary>
        /// Tear down the test fixture to return to the original state (<see cref="Cleanup"/>).
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Cleanup();
        }

        /// <summary>
        /// Setting up the test fixture.
        /// </summary>
        protected virtual void Arrange() {}

        /// <summary>
        /// Execute test action.
        /// </summary>
        protected virtual void Act() {}

        /// <summary>
        /// Tear down the test fixture to return to the original state.
        /// </summary>
        protected virtual void Cleanup() {}
    }
}