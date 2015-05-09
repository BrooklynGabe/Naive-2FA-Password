using System;
using System.Collections.Generic;
using System.Linq;

namespace TwoFactor.DataAccess
{
    public abstract class QueryReader<TheEntity> : IQueryReader<TheEntity>
    {
        public IQueryable<TheEntity> QueryBy(Specification<TheEntity> specification)
        {
            return QueryableData.Where(specification);
        }

        public IEnumerator<TheEntity> GetEnumerator()
        {
            return QueryableData.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return QueryableData.GetEnumerator();
        }

        public Type ElementType
        {
            get { return QueryableData.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return QueryableData.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return QueryableData.Provider; }
        }

        protected abstract IQueryable<TheEntity> QueryableData { get; }
    }
}
