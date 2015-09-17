using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Swallow.QueueManager
{
    public class QueueWrapper
    {
        private readonly IModel _channel;

        public QueueWrapper(IModel channel)
        {
            _channel = channel;
        }

        public void Enqueue(string queueName, QueueItemEntity entity)
        {
            var factory = new ConnectionFactory { HostName = QueueSettings.HostName };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string jsonEntity = JsonConvert.SerializeObject(entity);
                    byte[] body = Encoding.UTF8.GetBytes(jsonEntity);

                    channel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: body);
                }
            }
        }

        public void InitializeQueueListener(string queueName, Action onDeque)
        {
            var factory = new ConnectionFactory { HostName = QueueSettings.HostName };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new QueueingBasicConsumer(channel);

                    channel.BasicConsume(queue: queueName,
                                         noAck: true,
                                         consumer: consumer);

                    while (true)
                    {
                        var ea = consumer.Queue.Dequeue();
                        byte[] body = ea.Body;
                        string jsonEntity = Encoding.UTF8.GetString(body);
                        //var mail = JsonConvert.DeserializeObject<Mail>(jsonEntity);
                    }
                }
            }
        }

        /*private void OnReceived(object model, BasicDeliverEventArgs ea)
        {
            byte[] body = ea.Body;
            string message = Encoding.UTF8.GetString(body);
            var mail = JsonConvert.DeserializeObject<Mail>(message);
            _mailProcessor.Process(mail);
            //TODO: undelivered messages
        }*/
    }
}