using Swallow.BusinessLogic.Interfaces;
using Swallow.QueueManager.Interfaces;

namespace Swallow.BusinessLogic
{
    public sealed class QueueSender : IQueueSender
    {
        private readonly IQueueFactory _factory;

        public QueueSender(IQueueFactory queueFactory)
        {
            _factory = queueFactory;
        }

        public void Enqueue(Mail mail)
        {
            using (IQueueWrapper queue = _factory.CreateSender(QueueSettings.QueueName))
            {
                queue.Enqueue(QueueSettings.QueueName, mail);
            }
        }

        /*public void InitializeQueueListener()
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
        }*/
    }
}