using RabbitMQ_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQ_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            string queueName = "07281616_queue";
            string exchangeName = "07281616_exchange_topic";
            var routingRule = "0728.*.routingkey";
            MyRabbitMQ myMQ = new MyRabbitMQ(exchangeName, queueName, routingRule);
            var consumer = myMQ.ReceiveMessage(queueName);
            while (true)
            {
                //BasicConsume 方法是可阻塞的，比较好
                var msgResponse = consumer.Queue.Dequeue();
                //这种方法不好，没有阻塞等待
                //var msgResponse = channel.BasicGet("zzs_queue", true);
                var msgBody = Encoding.UTF8.GetString(msgResponse.Body);
                Console.WriteLine($"Received: {msgBody}  (only for {routingRule})");
            }
        }
    }
}
