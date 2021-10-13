using MovieDemo.Entity.Interfaces;

namespace MovieDemo.LIB.Models
{
    public class UpdateMovieUserRating : IValidable
    {
        public MovieFilterCriteria Movie { get; set; }
        public UserFilterCriteria User { get; set; }
        public decimal UserRating { get; set; }
        public bool IsValid()
        {
            return (null != Movie && Movie.IsValid() &&
                (null != User && User.IsValid()) &&
                (UserRating != 0));
        }
    }
}
