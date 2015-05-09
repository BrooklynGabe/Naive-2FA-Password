using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TwoFactor.Common.Tests
{
    [TestClass]
    public class When_Rounding_Up_Time : TwoFactorUnitTestBase
    {
        [TestMethod]
        public void Then_The_Returned_Time_Is_Rouded_To_The_Provided_Interval()
        {
            var now = DateTime.Now;
            var interval = TimeSpan.FromSeconds(30);
            var future = rounder.RoundTimeUpToNearest(now, interval);

            var actual = future - now;

            Assert.IsTrue(actual <= interval);
        }

        public override void Given()
        {
            rounder = new ValidUntilCalculator();
        }

        public override void Done() { }

        private IRoundTimeUp rounder;
    }
}
