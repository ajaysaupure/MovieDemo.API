using MovieDemo.Entity.BusinessEntities;
using MovieDemo.LIB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieDemo.LIB.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieEntity>> GetMoviesByFilterCriteriaAsync(MovieFilterCriteria movieFilterCritera);
        Task<IEnumerable<MovieEntity>> GetTopMoviesByRatingAsync(int top = 5);
        Task<IEnumerable<MovieEntity>> GetTopMoviesByUserAsync(long userId, int top = 5);
        Task<int> UpdateUserRatingAsync(UpdateMovieUserRating updateMovieUserRating);
        Task<string> TestEFAsync();
    }
}