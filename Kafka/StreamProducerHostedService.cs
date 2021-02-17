using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Kafka
{
    public class StreamProducerHostedService : IHostedService
    {
        private readonly ILogger<StreamProducerHostedService> _logger;
        private readonly IProducer<Null, string> _producer;

        public StreamProducerHostedService(ILogger<StreamProducerHostedService> logger)
        {
            _logger = logger;
            var config = new ProducerConfig()
            {
                BootstrapServers = "localhost:9092"
            };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 100; i++)
            {
                var value = $"Hello Kafka {i}";
                _logger.LogInformation(value);
                await _producer.ProduceAsync("demoTopic", new Message<Null, string>()
                {
                    Value = value
                },cancellationToken);
            }

            _producer.Flush(TimeSpan.FromSeconds(10));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _producer?.Dispose();
            return Task.CompletedTask;
        }
    }
}
