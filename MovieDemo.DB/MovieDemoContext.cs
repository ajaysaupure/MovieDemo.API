using Microsoft.EntityFrameworkCore;
using MovieDemo.DB.Entities;
using System;
using System.Collections.Generic;

namespace MovieDemo.DB.Context
{
    public class MovieDemoContext : DbContext
    {
        public MovieDemoContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieRating> MovieRatings { get; set; }
        public DbSet<MovieUserRating> MovieUserRatings { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieUserRating>().HasKey(p => new { p.MovieId, p.UserId });
            //modelBuilder.Entity<MovieRating>().HasKey(p => p.MovieId);

            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 1, Title = "Creative Lite", Genres = "Short,Talk -Show", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 2, Title = "A Little Late with Lilly Singh", Genres = "Comedy,Talk-Show", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 3, Title = "The Queen's Gambit", Genres = "Documentary,Sport", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 4, Title = "Followers", Genres = "Action,Adventure,Crime", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 5, Title = "The Substitute", Genres = "Comedy,Drama,Romance", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 6, Title = "Harry's Heroes: The Full English", Genres = "Biography,Documentary,Drama", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 7, Title = "Stolen Away", Genres = "Comedy,Drama,Romance", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 8, Title = "Home for Christmas", Genres = "Crime,Drama,Music", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 9, Title = "The Gift", Genres = "Animation,Comedy,Fantasy", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 10, Title = "Feel Good", Genres = "Drama", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 11, Title = "Lost Treasures of Egypt", Genres = "Drama", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 12, Title = "Stu, My Name is Stu", Genres = "Animation,Comedy,Fantasy", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 13, Title = "Thirumanam", Genres = "Drama", RunningTime = 120, YearOfRelease = 2019 });
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 14, Title = "Washington", Genres = "Animation,Comedy,Fantasy", RunningTime = 120, YearOfRelease = 2019 }); 
            modelBuilder.Entity<Movie>().HasData(new Movie() { MovieId = 15, Title = "Lazor Wulf", Genres = "Drama", RunningTime = 120, YearOfRelease = 2019 });

            decimal d = 0.5m;
            List<MovieRating> lstMovieRatings = new List<MovieRating>();
            for (int i = 1; i <= 10; i++)
            {
                lstMovieRatings.Add(new MovieRating() { MovieId = i, AvgRating = Math.Round(d * 2, MidpointRounding.AwayFromZero) / 2m, NumVotes = 1 });
                d = decimal.Add(d, 0.25m);
            }
            modelBuilder.Entity<MovieRating>().HasData(lstMovieRatings.ToArray());

            List<User> lstUsers = new List<User>();
            for (int i = 1; i <= 20; i++)
                lstUsers.Add(new User() { UserId = i, Name = $"UserName{i}" });
            modelBuilder.Entity<User>().HasData(lstUsers.ToArray());

            d = 0.5m;
            List<MovieUserRating> lstMovieUserRatings = new List<MovieUserRating>();
            for (int i = 1; i <= 10; i++)
            {
                lstMovieUserRatings.Add(new MovieUserRating() { UserId = i, MovieId = i, AvgRating = Math.Round(d * 2, MidpointRounding.AwayFromZero) / 2m });
                d = decimal.Add(d, 0.25m);
            }
            modelBuilder.Entity<MovieUserRating>().HasData(lstMovieUserRatings.ToArray());
        }
    }
}