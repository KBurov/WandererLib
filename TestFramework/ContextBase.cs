﻿using NUnit.Framework;

namespace Wanderer.Library.TestFramework
{
    /// <summary>
    /// Base class for unit or integration tests context classes.
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