namespace MovieProject.Models.Requests
{
    public class AddVoteModel
    {
        public int MovieId { get; set; }

        public string Comment { get; set; }
        public int Point { get; set; }
    }
}
