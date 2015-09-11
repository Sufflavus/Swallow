using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Swallow.BusinessLogic.Interfaces;

namespace Swallow.BusinessLogic
{
    public sealed class QueueManager : IQueueManager
    {
        private readonly IMailProcessor _mailProcessor;

        public QueueManager(IMailProcessor mailProcessor)
        {
            _mailProcessor = mailProcessor;
        }

        public void Enqueue(Mail mail)
        {
            var factory = new ConnectionFactory {HostName = QueueSettings.HostName};
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueSettings.QueueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string jsonMail = JsonConvert.SerializeObject(mail);
                    byte[] body = Encoding.UTF8.GetBytes(jsonMail);

                    channel.BasicPublish(exchange: "",
                                         routingKey: QueueSettings.RoutingKey,
                                         basicProperties: null,
                                         body: body);
                }
            }
        }

        public void InitializeQueueListener()
        {
            var factory = new ConnectionFactory {HostName = QueueSettings.HostName};
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QueueSettings.QueueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += OnReceived;

                    channel.BasicConsume(queue: QueueSettings.QueueName,
                                         noAck: true,
                                         consumer: consumer);
                }
            }
        }

        private void OnReceived(object model, BasicDeliverEventArgs ea)
        {
            byte[] body = ea.Body;
            string message = Encoding.UTF8.GetString(body);
            var mail = JsonConvert.DeserializeObject<Mail>(message);
            _mailProcessor.Process(mail);
            //TODO: undelivered messages
        }
    }
}