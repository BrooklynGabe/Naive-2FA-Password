using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoFactor.Common.Tests;

namespace TwoFactor.DataAccess.Tests
{
    [TestClass]
    public class When_Opening_A_Data_Reader_For_One_Time_Secrets : TwoFactorUnitTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Then_An_Exception_Is_Thrown_When_Context_Is_Null()
        {
            var actual = new OneTimeSecretReader(null);
        }

        public override void Given() { }

        public override void Done() { }
    }
}
