using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ
{
    public class QueueConsumer
    {
        public void CreateConsumer(IModel channel)
        {
            channel.QueueDeclare("Primary-Queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("Primary-Queue", true, consumer);

        }
    }
}
