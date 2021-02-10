using System;
using RabbitMQ;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = new RabbitMQ.ConnectorInit().Init();
            var consumer = new DirectExchangeConsumer();
            consumer.Consume(channel);
            
            Console.ReadLine();
        }
    }
}
