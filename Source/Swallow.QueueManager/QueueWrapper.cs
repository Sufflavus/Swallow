using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Swallow.QueueManager
{
    public class QueueWrapper : IDisposable
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly QueueingBasicConsumer _consumer;

        public QueueWrapper(IConnection connection, IModel channel)
        {
            _connection = connection;
            _channel = channel;
        }

        public QueueWrapper(IConnection connection, IModel channel, QueueingBasicConsumer consumer)
            : this(connection, channel)
        {
            _consumer = consumer;
        }

        public void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }

        public void Enqueue<T>(string queueName, T entity) where T : QueueItemEntity
        {
            string jsonMail = JsonConvert.SerializeObject(entity);
            byte[] body = Encoding.UTF8.GetBytes(jsonMail);

            _channel.BasicPublish(exchange: "",
                                  routingKey: queueName,
                                  basicProperties: null,
                                  body: body);
        }

        public T Dequeue<T>() where T : QueueItemEntity
        {
            BasicDeliverEventArgs eventArgs = _consumer.Queue.Dequeue();
            byte[] body = eventArgs.Body;
            string jsonEntity = Encoding.UTF8.GetString(body);
            return JsonConvert.DeserializeObject<T>(jsonEntity);
        }
    }
}