using System;
using RabbitMQ;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = new RabbitMQ.ConnectorInit().Init();
            //var consumer = new DirectExchangeConsumer();
            //var consumer = new TopicExchangeConsumer();
            //var consumer = new HeaderExchangeConsumer();
            var consumer = new FanoutExchangeConsumer();
            consumer.Consume(channel);
            
            Console.ReadLine();
        }
    }
}
