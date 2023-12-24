namespace MovieProject.Models.Requests
{
    public class AddMovieModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double VoteAverage { get; set; }

        public int VoteCount { get; set; }
    }
}
