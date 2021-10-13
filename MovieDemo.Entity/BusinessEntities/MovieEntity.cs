using MovieDemo.Entity.Interfaces;
using System;

namespace MovieDemo.Entity.BusinessEntities
{
    public class MovieEntity : IBusinessEntity
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public int? YearOfRelease { get; set; }
        public decimal? RunningTime { get; set; }
        public string Genres { get; set; }
        public decimal? AverageRating { get { return _avgRating; } set { _avgRating = Math.Round(value.GetValueOrDefault() * 2, MidpointRounding.AwayFromZero) / 2m; } }
        private decimal? _avgRating;
    }
}
