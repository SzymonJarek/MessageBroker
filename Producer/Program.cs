using System;
using System.Collections;
using RabbitMQ;


namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var producer = new RabbitMQ.ConnectorInit();
            var channel = producer.Init();
            //simple queue
            //var queueProducer = new QueueProducer();
            //queueProducer.SendMessage(channel);
            //direct exchange 
            var directExchange = new DirectExchangePublisher();
            directExchange.Publish(channel);

            Console.ReadLine();
        }
    }
}
