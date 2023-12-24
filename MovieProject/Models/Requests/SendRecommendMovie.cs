namespace MovieProject.Models.Requests
{
    public class SendRecommendMovie
    {
        public string SenderUsername { get; set; }
        public string ReceiverMail { get; set; }
        public string MovieName { get; set; }
    }
}
