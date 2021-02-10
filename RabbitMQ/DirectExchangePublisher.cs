using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class DirectExchangePublisher
    {
        public void Publish(IModel channel)
        {
            //declaring time to live of the message in the queue
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl",30000 }
            };
            channel.ExchangeDeclare("Exchange_Queue", ExchangeType.Direct, arguments: ttl);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hi! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("Exchange_Queue", "account.init", null, body);
                count++;
                Thread.Sleep(1000);
            }

        }
    }
}
