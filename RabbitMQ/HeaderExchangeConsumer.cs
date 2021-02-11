using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{
    public class HeaderExchangeConsumer
    {
        public void Consume(IModel channel)
        {
            channel.ExchangeDeclare("header-exchange", ExchangeType.Headers);
            channel.QueueDeclare("header-Queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var header = new Dictionary<string, object> { { "account","new" } };

            channel.QueueBind("header-Queue", "header-exchange", string.Empty,header);

            //fetch 10 messages at a time
            channel.BasicQos(0, 10, false);



            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("header-Queue", true, consumer);
        }
    }
}
