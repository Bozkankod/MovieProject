namespace MovieProject.Queue.Consumer.Abstract
{
    public interface IRabbitMQConsumer
    {
        void StartConsuming(string queueName);
    }
}
