using Kafka.Public;
using Kafka.Public.Loggers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kafka
{
    public class StreamConsumerHostedService : IHostedService
    {
        private readonly ILogger<StreamConsumerHostedService> _logger;
        private readonly ClusterClient _cluster;

        public StreamConsumerHostedService(ILogger<StreamConsumerHostedService> logger)
        {
            _logger = logger;
            _cluster = new ClusterClient(new Configuration
            {
                Seeds = "localhost:9092"
            },new ConsoleLogger());
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cluster.ConsumeFromLatest("demoTopic");
            _cluster.MessageReceived += _cluster_MessageReceived;

            return Task.CompletedTask;
        }

        private void _cluster_MessageReceived(RawKafkaRecord obj)
        {
            _logger.LogInformation($"Received: {Encoding.UTF8.GetString(obj.Value as byte[])}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cluster?.Dispose();
            return Task.CompletedTask;
        }
    }
}
