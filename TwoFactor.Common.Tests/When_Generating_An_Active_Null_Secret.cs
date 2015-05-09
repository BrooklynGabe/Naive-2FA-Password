using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoFactor.Common;

namespace TwoFactor.Common.Tests
{
    [TestClass]
    public class When_Generating_An_Active_Null_Secret : TwoFactorUnitTestBase
    {
        [TestMethod]
        public void Then_The_Secret_Is_Random_Per_Provider()
        {
            var firstSecretProvider = new ActiveNullSecretProvider();
            var secondSecretProvider = new ActiveNullSecretProvider();

            Assert.IsNotNull(firstSecretProvider.Secret);
            Assert.IsNotNull(secondSecretProvider.Secret);

            Assert.AreNotEqual(firstSecretProvider.Secret, secondSecretProvider.Secret);
        }

        [TestMethod]
        public void Then_The_Secret_Remains_The_Same_For_The_Lifetime_Of_The_Provider()
        {
            var secretProvider = new ActiveNullSecretProvider();
            var expected = secretProvider.Secret;
            var actual = secretProvider.Secret;

            Assert.AreEqual(expected, actual);
        }

        public override void Given() { }

        public override void Done() { }
    }
}
