namespace MovieDemo.DB.Entities
{
    public partial class Movie
    {
        public long MovieId { get; set; }
        public string Title { get; set; }
        public int? YearOfRelease { get; set; }
        public int? RunningTime { get; set; }
        public string Genres { get; set; }        
    }
}