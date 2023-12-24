using MovieProject.Models.Requests;

namespace MovieProject.Queue.Publisher.Abstract
{
    public interface IRabbitMQPublisher
    {
        void SendMessage(string queueName, SendRecommendMovie model);
    }
}
