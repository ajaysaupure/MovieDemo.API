using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDemo.DB.Entities
{
    public partial class MovieUserRating
    {  
        [ForeignKey("Movie")]
        public long MovieId { get; set; }
        
        [ForeignKey("User")]
        public long UserId { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        [Range(1, 5)]
        public decimal? AvgRating { get; set; }
    }
}