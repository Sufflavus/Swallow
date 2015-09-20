using RabbitMQ.Client;
using Swallow.QueueManager.Interfaces;

namespace Swallow.QueueManager
{
    public class QueueFactory : IQueueFactory
    {
        public IQueueWrapper CreateSender(string queueName)
        {
            var factory = new ConnectionFactory {HostName = QueueSettings.HostName};
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            return new QueueWrapper(connection, channel);
        }

        public IQueueWrapper CreateReceiver(string queueName)
        {
            var factory = new ConnectionFactory {HostName = QueueSettings.HostName};
            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new QueueingBasicConsumer(channel);

            channel.BasicConsume(queue: queueName,
                                 noAck: true,
                                 consumer: consumer);

            return new QueueWrapper(connection, channel, consumer);
        }
    }
}