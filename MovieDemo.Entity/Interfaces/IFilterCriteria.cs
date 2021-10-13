using System;
using System.Linq.Expressions;

namespace MovieDemo.Entity.Interfaces
{
    public interface IFilterCriteria<T> where T : IBusinessEntity
    {
        Expression<Func<T, bool>> GetWherePredicate();
    }
}
