using System;
using System.Linq;

namespace TwoFactor.DataAccess
{
    public interface IQueryReader<TheEntity> : IQueryable<TheEntity>
    {
        IQueryable<TheEntity> QueryBy(Specification<TheEntity> restriction);
    }
}
