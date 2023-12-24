using MovieProject.Models.Requests;
using MovieProject.Queue.Publisher.Abstract;
using MovieProject.Services.Abstract;

namespace MovieProject.Services.Concrete
{
    public class RecommendMovieService : IRecommendMovieService
    {

        private readonly IRabbitMQPublisher _rabbitMQPublisher;

        public RecommendMovieService(IRabbitMQPublisher rabbitMQPublisher)
        {
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public void Send(SendRecommendMovie model)
        {
            _rabbitMQPublisher.SendMessage("recommendationQueue", model);
        }

    }
}
