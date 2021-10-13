using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieDemo.DB.Entities
{
    public partial class MovieRating
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [ForeignKey("Movie")]
        public long MovieId { get; set; }

        [Column(TypeName = "decimal(3,1)")]
        [Range(1,5)]
        public decimal? AvgRating { get; set; }
        public long? NumVotes { get; set; }        
    }
}