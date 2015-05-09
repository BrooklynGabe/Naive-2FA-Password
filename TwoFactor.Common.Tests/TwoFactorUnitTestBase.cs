using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TwoFactor.Common.Tests
{
    [TestClass]
    public abstract class TwoFactorUnitTestBase
    {
        public abstract void Given();

        public abstract void Done();

        [TestInitialize]
        public void InitializeTest()
        {
            try
            {
                Given();
            }
            catch (Exception exception)
            {
                Assert.Fail(string.Format("{0} with Exception {1}: Message {2}", "Failed to initialize test", exception.GetType(), exception.Message));
            }
        }

        [TestCleanup]
        public void CleanupTest()
        {
            try
            {
                Done();
            }
            catch (Exception exception)
            {
                Assert.Fail(string.Format("{0} with Exception {1}: Message {2}", "Failed to cleanup test", exception.GetType(), exception.Message));
            }
        }
    }
}
