using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ
{
    public class TopicExchangeConsumer
    {
        public void Consume(IModel channel)
        {
            channel.ExchangeDeclare("topic-exchange", ExchangeType.Topic);
            channel.QueueDeclare("Topic-Queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind("Topic-Queue", "topic-exchange", "account.*");

            //fetch 10 messages at a time
            channel.BasicQos(0, 10, false);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("Topic-Queue", true, consumer);
        }
    }
}
