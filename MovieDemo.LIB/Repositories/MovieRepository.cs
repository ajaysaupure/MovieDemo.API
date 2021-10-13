using Microsoft.EntityFrameworkCore;
using MovieDemo.DB.Context;
using MovieDemo.DB.Entities;
using MovieDemo.Entity.BusinessEntities;
using MovieDemo.LIB.Exceptions;
using MovieDemo.LIB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDemo.LIB.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private MovieDemoContext _movieDemoContext;
        public MovieRepository(MovieDemoContext freewheelDbContext)
        {
            _movieDemoContext = freewheelDbContext;
        }
        public virtual async Task<IEnumerable<MovieEntity>> GetMoviesByFilterCriteriaAsync(MovieFilterCriteria movieFilterCritera)
        {
            return await _movieDemoContext.Movies
                 .GroupJoin(_movieDemoContext.MovieRatings, mo => mo.MovieId, mr => mr.MovieId, (mo, mr) => new { mo, mr })
                 .SelectMany(x => x.mr.DefaultIfEmpty(), (mov, mrv) => new MovieEntity()
                 {
                     MovieId = mov.mo.MovieId,
                     AverageRating = mrv.AvgRating,
                     Genres = mov.mo.Genres,
                     Title = mov.mo.Title,
                     RunningTime = mov.mo.RunningTime,
                     YearOfRelease = mov.mo.YearOfRelease
                 })
                 .Where(movieFilterCritera.GetWherePredicate())
                 .ToListAsync();
        }
        public virtual async Task<IEnumerable<MovieEntity>> GetTopMoviesByRatingAsync(int top = 5)
        {
            return await _movieDemoContext.Movies
                 .Join(_movieDemoContext.MovieRatings, mo => mo.MovieId, mr => mr.MovieId, (mo, mr) => new { mo, mr })
                 .Where(m => m.mr.AvgRating != null)
                 .OrderByDescending(m => m.mr.AvgRating)
                 .Take(top)
                 .Select(x => new MovieEntity()
                 {
                     MovieId = x.mo.MovieId,
                     AverageRating = x.mr.AvgRating,
                     Genres = x.mo.Genres,
                     Title = x.mo.Title,
                     RunningTime = x.mo.RunningTime,
                     YearOfRelease = x.mo.YearOfRelease
                 })
                 .ToListAsync();
        }
        public virtual async Task<IEnumerable<MovieEntity>> GetTopMoviesByUserAsync(long userId, int top = 5)
        {
            return await _movieDemoContext.Movies
                .Join(_movieDemoContext.MovieRatings, mo => mo.MovieId, mr => mr.MovieId, (mo, mr) => new { mo, mr })
                 .Join(_movieDemoContext.MovieUserRatings, mo => mo.mo.MovieId, mu => mu.MovieId, (mo, mu) => new { mo, mu })
                 .Where(m => m.mu.UserId ==userId && m.mu.AvgRating != null)
                 .GroupBy(x => new { x.mo.mo.MovieId, x.mo.mo.Title, x.mo.mo.RunningTime, x.mo.mo.YearOfRelease, x.mo.mr.AvgRating, x.mo.mo.Genres, x.mu.UserId })
                 .OrderByDescending(x => x.Count())
                 .Take(top)
                 .Select(x => new MovieEntity()
                 {
                     MovieId = x.Key.MovieId,
                     AverageRating = x.Key.AvgRating,
                     Genres = x.Key.Genres,
                     Title = x.Key.Title,
                     RunningTime = x.Key.RunningTime,
                     YearOfRelease = x.Key.YearOfRelease
                 })
                 .ToListAsync();
        }
        public virtual async Task<int> UpdateUserRatingAsync(UpdateMovieUserRating updateMovieUserRating)
        {
            var movie = (await GetMoviesByFilterCriteriaAsync(updateMovieUserRating.Movie)).FirstOrDefault();
            if (null == movie)
                throw new MovieNotFoundExceptions();

            var user = (await GetUsersByFilterCriteria(updateMovieUserRating.User)).FirstOrDefault();
            if (null == user)
                throw new UserNotFoundExceptions();

            long noOfNewUsersToBeAdded;
            decimal previousUserRatingsIfExists;

            var movieUserRating = await _movieDemoContext.MovieUserRatings.FirstOrDefaultAsync(ur => ur.UserId == user.UserId && ur.MovieId == movie.MovieId);
            if (null == movieUserRating)
            {
                noOfNewUsersToBeAdded = 1L;
                previousUserRatingsIfExists = 0m;
                movieUserRating = new MovieUserRating()
                {
                    MovieId = movie.MovieId,
                    UserId = user.UserId,
                    AvgRating = Math.Round(updateMovieUserRating.UserRating * 2, MidpointRounding.AwayFromZero) / 2m
                };
                await _movieDemoContext.MovieUserRatings.AddAsync(movieUserRating);
            }
            else
            {
                noOfNewUsersToBeAdded = 0L;
                previousUserRatingsIfExists = movieUserRating.AvgRating.GetValueOrDefault();
                movieUserRating.AvgRating = Math.Round(updateMovieUserRating.UserRating * 2, MidpointRounding.AwayFromZero) / 2m;
            }

            var movieRating = await _movieDemoContext.MovieRatings.FirstOrDefaultAsync(mr => mr.MovieId == movie.MovieId);
            if (null == movieRating)
            {
                movieRating = new MovieRating() { MovieId = movie.MovieId };
                await _movieDemoContext.AddAsync(movieRating);                
            }            

            var previousRating = movieRating.AvgRating.GetValueOrDefault();
            var previousNoOfVotes = movieRating.NumVotes.GetValueOrDefault();

            movieRating.AvgRating =
                decimal.Divide
                (
                    decimal.Add
                    (
                        decimal.Add
                        (
                            decimal.Multiply(previousRating, previousNoOfVotes),
                            decimal.Multiply(-1m, previousUserRatingsIfExists)
                        )
                    ,
                    updateMovieUserRating.UserRating
                    )
                    ,
                    decimal.Add(previousNoOfVotes, noOfNewUsersToBeAdded)
                );

            movieRating.AvgRating = Math.Round(movieRating.AvgRating.GetValueOrDefault() * 2, MidpointRounding.AwayFromZero) / 2m;
            movieRating.NumVotes = previousNoOfVotes + noOfNewUsersToBeAdded;

            return await _movieDemoContext.SaveChangesAsync();
        }
        private async Task<IEnumerable<UserEntity>> GetUsersByFilterCriteria(UserFilterCriteria userFilterCriteria)
        {
            return await _movieDemoContext.Users
                .Select(u => new UserEntity() { Name = u.Name, UserId = u.UserId })
                .Where(userFilterCriteria.GetWherePredicate())
                .ToListAsync();
        }

        public virtual async Task<string> TestEFAsync()
        {
            string title = "Test EF Migrations!";
            var movie = await _movieDemoContext.Movies.FirstOrDefaultAsync(m => m.Title.StartsWith(title));
            if (null != movie) movie.Title = $"{title}::{DateTime.Now.ToString()}";
            else
            {
                movie = new Movie() { Title = $"{title}::{DateTime.Now.ToString()}" };
                await _movieDemoContext.Movies.AddAsync(movie);
            }

            await _movieDemoContext.SaveChangesAsync();
            return movie.Title;
        }
    }
}