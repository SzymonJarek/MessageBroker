using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{
    public class FanoutExchangeConsumer
    {
        public void Consume(IModel channel)
        {
            channel.ExchangeDeclare("fanout-exchange", ExchangeType.Fanout);
            channel.QueueDeclare("fanout-Queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            channel.QueueBind("fanout-Queue", "fanout-exchange", string.Empty);

            //fetch 10 messages at a time
            channel.BasicQos(0, 10, false);



            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("fanout-Queue", true, consumer);
        }
    }
}
