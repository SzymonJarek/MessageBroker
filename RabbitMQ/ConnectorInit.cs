using System;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQ
{
    public class ConnectorInit
    {
        public IModel Init()
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            
            return channel;
            
        }


    }
}
