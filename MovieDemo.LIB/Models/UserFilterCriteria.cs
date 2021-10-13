using MovieDemo.Entity.BusinessEntities;
using MovieDemo.Entity.Interfaces;
using MovieDemo.LIB.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MovieDemo.LIB.Models
{
    public class UserFilterCriteria : IFilterCriteria<UserEntity>, IValidable
    {
        public long? UserId { get; set; }
        public string Name { get; set; }
        public bool IsValid()
        {
            return !(String.IsNullOrEmpty(Name) && (UserId == null && UserId == 0));
        }

        public Expression<Func<UserEntity, bool>> GetWherePredicate()
        {
            List<Expression<Func<UserEntity, bool>>> lstPredicates = new List<Expression<Func<UserEntity, bool>>>();

            if (!string.IsNullOrEmpty(Name))
                lstPredicates.Add(t => t.Name.StartsWith(Name));

            if (UserId.GetValueOrDefault(0) > 0)
                lstPredicates.Add(y => y.UserId == UserId.GetValueOrDefault());

            Expression<Func<UserEntity, bool>> userPredicate = null;
            foreach (var predicate in lstPredicates)
                userPredicate = null == userPredicate ? predicate : userPredicate.And(predicate);
            return userPredicate;
        }
    }
}
