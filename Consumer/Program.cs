using System;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var consumer = new RabbitMQ.Consumer();
            consumer.CreateConsumer("Primary-Queue");
            Console.ReadLine();
        }
    }
}
