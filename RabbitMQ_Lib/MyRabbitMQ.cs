using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ_Lib
{
    public class MyRabbitMQ
    {
        //连接工厂
        private ConnectionFactory factory { get; set; } = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };

        private IConnection connection { get; set; }

        private IModel channel { get; set; }

        //生产方 构造函数
        public MyRabbitMQ(string exchangeName = "", string exchangeType = ExchangeType.Direct)
        {
            //创建一个连接
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            //创建一个转发器
            channel.ExchangeDeclare(exchangeName, exchangeType);
        }

        //消费方 构造函数
        public MyRabbitMQ(string exchangeName = "", string queueName = "", string routingKey = "")
        {
            //创建一个连接
            connection = factory.CreateConnection();
            channel = connection.CreateModel();

            //创建一个队列
            channel.QueueDeclare(queueName, true, false, false);

            //队列绑定
            channel.QueueBind(queueName, exchangeName, routingKey);
        }

        public void SendMessage(string message = "", string exchangeName = "", string routingKey = "")
        {
            channel.BasicPublish(exchangeName, routingKey, null, Encoding.UTF8.GetBytes(message));
        }

        public QueueingBasicConsumer ReceiveMessage(string queueName = "")
        {
            //EventingBasicConsumer
            var consumer = new QueueingBasicConsumer(channel);
            channel.BasicConsume(queueName, true, consumer);
            return consumer;
        }
    }
}
