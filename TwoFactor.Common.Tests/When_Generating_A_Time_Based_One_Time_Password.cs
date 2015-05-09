using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoFactor.Common;
using Moq;
using System.Threading;

namespace TwoFactor.Common.Tests
{
    [TestClass]
    public class When_Generating_A_Time_Based_One_Time_Password : TwoFactorUnitTestBase
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void And_Secret_Provider_Is_Null_Then_An_Exception_Is_Thrown()
        {
            var actual = new TimeBasedOneTimeGenerator(null, passwordOptions, nowTimeCalculator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void And_Password_Options_Is_Null_Then_An_Exception_Is_Thrown()
        {
            var actual = new TimeBasedOneTimeGenerator(secretProvider, null, nowTimeCalculator);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void And_Time_Rounder_Is_Null_Then_An_Exception_Is_Thrown()
        {
            var actual = new TimeBasedOneTimeGenerator(secretProvider, passwordOptions, null);
        }

        [TestMethod]
        public void And_All_Minimums_Are_Met_Then_A_One_Time_Password_Is_Returned()
        {
            var generator = new TimeBasedOneTimeGenerator(secretProvider, passwordOptions, nowTimeCalculator);

            var actual = generator.GeneratePassword();

            Assert.IsNotNull(actual);
            Assert.AreEqual(passwordOptions.PasswordLength, actual.Length);
        }

        [TestMethod]
        public void And_Multiple_Requests_To_Generate_Are_Within_Time_Interval_Then_They_Are_The_Same()
        {
            var first_generator = new TimeBasedOneTimeGenerator(secretProvider, passwordOptions, nowTimeCalculator);
            var second_generator = new TimeBasedOneTimeGenerator(secretProvider, passwordOptions, nowTimeCalculator);

            var first_actual = first_generator.GeneratePassword();
            var second_actual = second_generator.GeneratePassword();

            Assert.AreEqual(first_actual, second_actual);
        }

        [TestMethod]
        public void And_Multiple_Requests_To_Generate_Are_Outside_Time_Interval_Then_They_Are_The_Different()
        {
            var first_generator = new TimeBasedOneTimeGenerator(secretProvider, passwordOptions, nowTimeCalculator);
            var second_generator = new TimeBasedOneTimeGenerator(secretProvider, passwordOptions, laterTimeCalculator);

            var first_actual = first_generator.GeneratePassword();
            var second_actual = second_generator.GeneratePassword();

            Assert.AreNotEqual(first_actual, second_actual);
        }

        public override void Given()
        {
            var now = DateTime.Now;
            var interval = TimeSpan.FromSeconds(30);
            var later = now + interval;
            
            var mockUniqueSecretProvider = new Mock<IProvideUniqueSecrets>();
            mockUniqueSecretProvider
                .Setup(m => m.Secret)
                .Returns(Guid.NewGuid().ToByteArray);


            var mockOneTimePasswordOptions = new Mock<IOneTimePasswordOptions>();
            mockOneTimePasswordOptions
                .Setup(m => m.PasswordLength)
                .Returns(6);
            mockOneTimePasswordOptions
                .Setup(m => m.TimeInterval)
                .Returns(30);


            var mockCurrentTimeRoundUp = new Mock<IRoundTimeUp>();
            mockCurrentTimeRoundUp
                .Setup(m => m.RoundTimeUpToNearest(
                     It.IsAny<DateTime>()
                    , It.IsAny<TimeSpan>()))
                .Returns(() =>
                {
                    return new DateTime(((now.Ticks + interval.Ticks - 1) / interval.Ticks) * interval.Ticks);
                });

            var mockLaterTimeRoundUp = new Mock<IRoundTimeUp>();
            mockLaterTimeRoundUp
                .Setup(m => m.RoundTimeUpToNearest(
                     It.IsAny<DateTime>()
                    , It.IsAny<TimeSpan>()))
                .Returns(() =>
                {
                    return new DateTime(((later.Ticks + interval.Ticks - 1) / interval.Ticks) * interval.Ticks);
                });

            secretProvider = mockUniqueSecretProvider.Object;
            passwordOptions = mockOneTimePasswordOptions.Object;
            nowTimeCalculator = mockCurrentTimeRoundUp.Object;
            laterTimeCalculator = mockLaterTimeRoundUp.Object;
        }

        public override void Done() { }

        private IProvideUniqueSecrets secretProvider;
        private IOneTimePasswordOptions passwordOptions;
        private IRoundTimeUp nowTimeCalculator, laterTimeCalculator;
    }
}
