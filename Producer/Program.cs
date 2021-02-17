using System;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ;
using Serilog;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            //RabbitMQ();
            Kafka(args);

            Console.ReadLine();
        }

        private static void Kafka(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog((context, configuration) =>
            {
                configuration.Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .WriteTo.Console()
                        .WriteTo.Elasticsearch(new Serilog.Sinks.Elasticsearch.ElasticsearchSinkOptions(new Uri("localhost:9200"))
                        {
                            IndexFormat = $"Thats whats in LOG",
                            AutoRegisterTemplate = true,
                            NumberOfShards = 2,
                            NumberOfReplicas = 1
                        }).ReadFrom.Configuration(context.Configuration);
            })
            .ConfigureServices((context, collection) =>
            {
                collection.AddHostedService<Kafka.StreamProducerHostedService>();
            });

        private static void RabbitMQ()
        {
            var producer = new RabbitMQ.ConnectorInit();
            var channel = producer.Init();
            //simple queue
            //var queueProducer = new QueueProducer();
            //queueProducer.SendMessage(channel);
            //direct exchange 
            //var exchange = new DirectExchangePublisher();
            //var exchange = new TopicExchangePublisher();
            //var exchange = new HeaderExchangePublisher();
            var exchange = new FanoutExchangePublisher();
            exchange.Publish(channel);
        }
    }
}
