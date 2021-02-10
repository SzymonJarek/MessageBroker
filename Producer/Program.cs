using System;
using RabbitMQ;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var producer = new RabbitMQ.Producer();
            var queue = producer.CreateProcuder("Primary-Queue");
            producer.SendMessage(queue,"Hi!");
            Console.ReadLine();
        }
    }
}
