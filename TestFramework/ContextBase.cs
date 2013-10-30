using NUnit.Framework;

namespace Wanderer.Library.TestFramework
{
    /// <summary>
    /// Base class for unit tests context classes.
    /// </summary>
    [TestFixture]
    public abstract class ContextBase
    {
        /// <summary>
        /// Setting up the test fixture (arrange) and run test action (<see cref="Arrange"/> and <see cref="Act"/>).
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            Arrange();

            Act();
        }

        /// <summary>
        /// Tear down the test fixture to return to the original state.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Cleanup();
        }

        /// <summary>
        /// Setting up the test fixture.
        /// </summary>
        public virtual void Arrange() {}

        /// <summary>
        /// Execute test action.
        /// </summary>
        public virtual void Act() {}

        /// <summary>
        /// Tear down the test fixture to return to the original state.
        /// </summary>
        public virtual void Cleanup() {}
    }
}