using System;
using System.Linq.Expressions;

namespace TwoFactor.DataAccess
{
    public sealed class Specification<TheEntity> : ISatisfiable<TheEntity>
    {
        public Specification(Expression<Func<TheEntity, bool>> Restriction)
        {
            if (Restriction == null)
                throw new ArgumentNullException("Restriction");

            _restriction = Restriction;
        }

        public bool SatisfiedBy(TheEntity entity)
        {
            return _restriction.Compile()(entity);
        }

        public static implicit operator Expression<Func<TheEntity, bool>>(Specification<TheEntity> ThisSpecification)
        {
            return ThisSpecification._restriction;
        }

        public static Specification<TheEntity> And(Specification<TheEntity> ThisSpecification, Specification<TheEntity> OtherSpecification)
        {
            var vRightInvocationExpression = Expression.Invoke(OtherSpecification._restriction, ThisSpecification._restriction.Parameters);
            var vCompositeExpression = Expression.MakeBinary(ExpressionType.AndAlso, ThisSpecification._restriction.Body, vRightInvocationExpression);
            return new Specification<TheEntity>(Expression.Lambda<Func<TheEntity, bool>>(vCompositeExpression, ThisSpecification._restriction.Parameters));
        }

        public static Specification<TheEntity> operator &(Specification<TheEntity> ThisSpecification, Specification<TheEntity> OtherSpecification)
        {
            return And(ThisSpecification, OtherSpecification);
        }

        public static Specification<TheEntity> Or(Specification<TheEntity> ThisSpecification, Specification<TheEntity> OtherSpecification)
        {
            var vRightInvocationExpression = Expression.Invoke(OtherSpecification._restriction, ThisSpecification._restriction.Parameters);
            var vCompositeExpression = Expression.MakeBinary(ExpressionType.OrElse, ThisSpecification._restriction.Body, vRightInvocationExpression);
            return new Specification<TheEntity>(Expression.Lambda<Func<TheEntity, bool>>(vCompositeExpression, ThisSpecification._restriction.Parameters));
        }

        public static Specification<TheEntity> operator |(Specification<TheEntity> ThisSpecification, Specification<TheEntity> OtherSpecification)
        {
            return Or(ThisSpecification, OtherSpecification);
        }

        public static Specification<TheEntity> Not(Specification<TheEntity> ThisSpecification)
        {
            return new Specification<TheEntity>(Expression.Lambda<Func<TheEntity, bool>>(Expression.Not(ThisSpecification._restriction.Body),
                                                   ThisSpecification._restriction.Parameters));
        }

        public static Specification<TheEntity> operator !(Specification<TheEntity> ThisSpecification)
        {
            return Not(ThisSpecification);
        }

        #region Private
        private readonly Expression<Func<TheEntity, bool>> _restriction;
        #endregion
    }
}
