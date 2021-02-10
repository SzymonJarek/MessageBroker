using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ
{
    public class DirectExchangeConsumer
    {
        public void Consume(IModel channel)
        {
            channel.ExchangeDeclare("Exchange_Queue",ExchangeType.Direct);
            channel.QueueDeclare("Direct-Queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind("Direct-Queue","Exchange_Queue","account.init");
            
            //fetch 10 messages at a time
            channel.BasicQos(0, 10, false);


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("Direct-Queue", true, consumer);
        }
    }
}
