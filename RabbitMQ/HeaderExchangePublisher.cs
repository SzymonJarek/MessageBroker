using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQ
{
    public class HeaderExchangePublisher
    {
        public void Publish(IModel channel)
        {
            //declaring time to live of the message in the queue
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl",30000 }
            };
            channel.ExchangeDeclare("header-exchange", ExchangeType.Headers, arguments: ttl);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hi! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "new" } };

                channel.BasicPublish("header-exchange", string.Empty, properties, body);
                count++;
                Thread.Sleep(1000);
            }

        }
    }
}
