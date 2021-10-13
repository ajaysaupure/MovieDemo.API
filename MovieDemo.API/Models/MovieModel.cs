using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDemo.API.Models
{
    public class MovieModel
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public int? YearOfRelease { get; set; }
        public decimal? RunningTime { get; set; }
        public string Genres { get; set; }
        public decimal? AverageRating { get; set; }
    }
}
