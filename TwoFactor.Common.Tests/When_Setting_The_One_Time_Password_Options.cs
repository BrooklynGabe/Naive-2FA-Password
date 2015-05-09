using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TwoFactor.Common.Tests
{
    [TestClass]
    public class When_Setting_The_One_Time_Password_Options : TwoFactorUnitTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void And_The_Time_Interval_Is_Too_Short_Then_Exception_Is_Thrown()
        {
            options.TimeInterval = TimeBasedOneTimeOptions.MinimumTimeInterval - 1;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void And_The_Password_Length_Is_Too_Short_Then_Exception_Is_Thrown()
        {
            options.PasswordLength = TimeBasedOneTimeOptions.MinimumPasswordLength - 1;
        }

        [TestMethod]
        public void And_The_Values_Are_Allowed_Then_The_Values_Are_Accepted()
        {
            options.TimeInterval = TimeBasedOneTimeOptions.MinimumTimeInterval;
            options.PasswordLength = TimeBasedOneTimeOptions.MinimumPasswordLength;

            Assert.AreEqual(TimeBasedOneTimeOptions.MinimumTimeInterval, options.TimeInterval);
            Assert.AreEqual(TimeBasedOneTimeOptions.MinimumPasswordLength, options.PasswordLength);
        }

        public override void Given()
        {
            options = new TimeBasedOneTimeOptions();
        }

        public override void Done() { }

        private IOneTimePasswordOptions options;
    }
}
