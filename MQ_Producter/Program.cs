using RabbitMQ.Client;
using RabbitMQ_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ_Producter
{
    class Program
    {
        static void Main(string[] args)
        {
            string exchangeName = "07281616_exchange_topic";
            string routingkeya = "0728.a.c.routingkey";
            string routingkeyb = "0728.b.routingkey";
            MyRabbitMQ myMQ = new MyRabbitMQ(exchangeName, ExchangeType.Topic);
            for (int i = 0; i < 3600; i++)
            {
                System.Threading.Thread.Sleep(1000);
                if (i % 2 == 0)
                {
                    var message = $"{routingkeyb} -- {DateTime.Now.ToLongTimeString()}";
                    Console.WriteLine($"auto send: {message}  for {routingkeyb}");
                    myMQ.SendMessage(message, exchangeName, routingkeyb);
                }
                else
                {
                    var message = $"{routingkeya} -- {DateTime.Now.ToLongTimeString()}";
                    Console.WriteLine($"auto send: {message}  for {routingkeya}");
                    myMQ.SendMessage(message, exchangeName, routingkeya);
                }

            }
        }
    }
}
