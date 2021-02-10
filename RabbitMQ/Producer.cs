using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class Producer
    {
        private string _queueName;
        public IModel CreateProcuder(string queueName)
        {
            _queueName = queueName;
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            return channel;
            
        }

        public void SendMessage(IModel channel, string messageText)
        {
            var message = new { Name = "Producer", Message = messageText };
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", _queueName, null, body);
        }

    }
}
