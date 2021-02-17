using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //RabbitMQ();
            Kafka(args);
            Console.ReadLine();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
           .ConfigureServices((context, collection) =>
           {
               collection.AddHostedService<Kafka.StreamConsumerHostedService>();
           });

        private static void Kafka(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }


        private static void RabbitMQ()
        {
            var channel = new RabbitMQ.ConnectorInit().Init();
            //var consumer = new DirectExchangeConsumer();
            //var consumer = new TopicExchangeConsumer();
            //var consumer = new HeaderExchangeConsumer();
            var consumer = new FanoutExchangeConsumer();
            consumer.Consume(channel);
        }
    }
}
