using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class QueueProducer
    {
        public void SendMessage(IModel channel)
        {
            channel.QueueDeclare("Primary-Queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hi! Count {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("", "Primary-Queue", null, body);
                count++;
                Thread.Sleep(1000);
            }

        }
    }
}
