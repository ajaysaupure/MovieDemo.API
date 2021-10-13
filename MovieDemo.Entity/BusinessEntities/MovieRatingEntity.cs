using MovieDemo.Entity.Interfaces;

namespace MovieDemo.Entity.BusinessEntities
{
    public class MovieRatingEntity : IBusinessEntity
    {
        public UserEntity User { get; set; }
        public MovieEntity Movie { get; set; }
        public decimal? Rating { get; set; }
    }
}
