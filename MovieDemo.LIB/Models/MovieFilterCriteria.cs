using MovieDemo.Entity.BusinessEntities;
using MovieDemo.Entity.Interfaces;
using MovieDemo.LIB.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MovieDemo.LIB.Models
{
    public class MovieFilterCriteria : IFilterCriteria<MovieEntity>, IValidable
    {
        public string Title { get; set; }
        public int? YearOfRelease { get; set; }
        public string Genres { get; set; }
        public bool IsValid()
        {

            return !(String.IsNullOrEmpty(Title) &&
                (YearOfRelease == null || YearOfRelease == 0) &&
                (Genres == null || Genres.Length == 0));

        }
        public Expression<Func<MovieEntity, bool>> GetWherePredicate()
        {
            List<Expression<Func<MovieEntity, bool>>> lstPredicates = new List<Expression<Func<MovieEntity, bool>>>();

            if (!string.IsNullOrEmpty(Title))
                lstPredicates.Add(t => t.Title.StartsWith(Title));

            if (YearOfRelease.GetValueOrDefault(0) > 0)
                lstPredicates.Add(y => y.YearOfRelease == YearOfRelease);

            if (!string.IsNullOrEmpty(Genres))
                lstPredicates.Add(g => g.Genres != null && g.Genres.Contains(Genres));

            Expression<Func<MovieEntity, bool>> moviePredicate = null;
            foreach (var predicate in lstPredicates)
                moviePredicate = null == moviePredicate ? predicate : moviePredicate.And(predicate);
            return moviePredicate;
        }
    }
}
