using Microsoft.Extensions.Options;
using MovieProject.Models.Requests;
using MovieProject.Queue.Consumer.Abstract;
using MovieProject.Settings;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class RabbitMQConsumer : IRabbitMQConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly RabbitMQSetting _rabbitMQSetting;

    public RabbitMQConsumer(IOptions<RabbitMQSetting> rabbitMQSetting)
    {
        _rabbitMQSetting = rabbitMQSetting.Value;

        var factory = new ConnectionFactory
        {
            Uri = new Uri(_rabbitMQSetting.ConnectionString)
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }


    public void StartConsuming(string queueName)
    {
        _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var recommendMovie = JsonConvert.DeserializeObject<SendRecommendMovie>(message);

            var sender = recommendMovie.SenderUsername;
            var receiver = recommendMovie.ReceiverMail;
            var movie = recommendMovie.MovieName;

            var emailService = new EmailService();
            emailService.SendEmail(receiver, "Movie Recommendation", $"Hello my name is {sender}, {movie} i recommend you to watch the movie.");

        };

        _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

    }

    public void StopConsuming()
    {
        _channel.Close();
        _connection.Close();
    }
}
