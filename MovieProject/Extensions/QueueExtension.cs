using MovieProject.Queue.Consumer.Abstract;
using MovieProject.Queue.Publisher.Abstract;
using MovieProject.Queue.Publisher.Concrete;
using MovieProject.Settings;

namespace MovieProject.Extensions
{
    public static class QueueExtension
    {


        public static void AddQueue(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSetting>(configuration.GetSection("RabbitMQ"));

            services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
            services.AddScoped<IRabbitMQConsumer, RabbitMQConsumer>();

            //start consuming
            using var serviceProvider = services.BuildServiceProvider();
            var rabbitMQConsumer = serviceProvider.GetRequiredService<IRabbitMQConsumer>();
            rabbitMQConsumer.StartConsuming("recommendationQueue");
        }
    }
}