using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoFactor.Common.Tests;

namespace TwoFactor.DataAccess.Tests
{
    [TestClass]
    public class When_Creating_A_Specification : TwoFactorUnitTestBase 
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void And_The_Restriction_Is_Null_Then_An_Exception_Is_Thrown()
        {
            var actual = new Specification<object>(null);
        }

        public override void Given() { }

        public override void Done() { }
    }
}
