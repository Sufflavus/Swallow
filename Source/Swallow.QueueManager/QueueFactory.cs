using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Swallow.QueueManager
{
    public class QueueFactory
    {
        public QueueWrapper CreateSender(string name)
        {
            var factory = new ConnectionFactory { HostName = QueueSettings.HostName };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: name,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    return new QueueWrapper(channel);
                }
            }
        }

        public QueueWrapper CreateReceiver(string name)
        {
            var factory = new ConnectionFactory { HostName = QueueSettings.HostName };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: name,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new QueueingBasicConsumer(channel);

                    channel.BasicConsume(queue: name,
                                         noAck: true,
                                         consumer: consumer);

                    return new QueueWrapper(channel);
                }
            }
        }

        private IModel CreateChannel(string queueName)
        {
            throw new NotImplementedException();
        }
    }
}