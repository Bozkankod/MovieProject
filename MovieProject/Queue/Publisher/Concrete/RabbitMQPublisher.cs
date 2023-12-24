using Microsoft.Extensions.Options;
using MovieProject.Models.Requests;
using MovieProject.Queue.Publisher.Abstract;
using MovieProject.Settings;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;


namespace MovieProject.Queue.Publisher.Concrete
{
    public class RabbitMQPublisher : IRabbitMQPublisher
    {

        private readonly RabbitMQSetting _rabbitMQSetting;

        public RabbitMQPublisher(IOptions<RabbitMQSetting> rabbitMQSetting)
        {
            _rabbitMQSetting = rabbitMQSetting.Value;
        }

        public void SendMessage(string queueName, SendRecommendMovie model)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMQSetting.ConnectionString)
            };

            var message = JsonConvert.SerializeObject(model);

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
    }
}
