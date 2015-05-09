using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwoFactor.Common.Tests;
using Moq;

namespace TwoFactor.DataAccess.Tests
{
    [TestClass]
    public class When_Using_Specification_Operators : TwoFactorUnitTestBase 
    {
        [TestMethod]
        public void Then_True_Is_Returned_For_AND_When_Both_Operands_Were_True()
        {
            var and_True_True = alwaysTrue & alwaysTrue;

            Assert.IsTrue(and_True_True.SatisfiedBy(new Object()));
        }

        [TestMethod]
        public void Then_False_Is_Returned_For_AND_When_Either_Operand_Was_False()
        {
            var and_True_False = alwaysTrue & alwaysFalse;
            var and_False_True = alwaysFalse & alwaysTrue;
            var and_False_False = alwaysFalse & alwaysFalse;

            Assert.IsFalse(and_True_False.SatisfiedBy(new Object()));
            Assert.IsFalse(and_False_True.SatisfiedBy(new Object()));
            Assert.IsFalse(and_False_False.SatisfiedBy(new Object()));
        }

        [TestMethod]
        public void Then_True_Is_Returned_For_OR_When_Either_Operand_Was_True()
        {
            var or_True_True = alwaysTrue | alwaysTrue;
            var or_True_False = alwaysTrue | alwaysFalse;
            var or_False_True = alwaysFalse | alwaysTrue;

            Assert.IsTrue(or_True_True.SatisfiedBy(new Object()));
            Assert.IsTrue(or_True_False.SatisfiedBy(new Object()));
            Assert.IsTrue(or_False_True.SatisfiedBy(new Object()));
        }

        [TestMethod]
        public void Then_False_Is_Returned_For_OR_When_Both_Operands_Were_False()
        {
            var or_False_False = alwaysFalse | alwaysFalse;

            Assert.IsFalse(or_False_False.SatisfiedBy(new Object()));
        }

        [TestMethod]
        public void Then_Opposite_Is_Returned_For_NOT_When_Applied()
        {
            var not_False = !alwaysFalse;
            var not_True = !alwaysTrue;

            Assert.IsTrue(not_False.SatisfiedBy(new Object()));
            Assert.IsFalse(not_True.SatisfiedBy(new Object()));
        }

        public override void Given()
        {
            alwaysTrue = new Specification<object>(x => true);
            alwaysFalse = new Specification<object>(x => false);
        }

        public override void Done() { }

        private Specification<object> alwaysTrue, alwaysFalse;
    }
}
