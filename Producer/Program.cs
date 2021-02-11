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
            //var exchange = new DirectExchangePublisher();
            //var exchange = new TopicExchangePublisher();
            //var exchange = new HeaderExchangePublisher();
            var exchange = new FanoutExchangePublisher();
            exchange.Publish(channel);

            Console.ReadLine();
        }
    }
}
