using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Swallow.BusinessLogic.Queue
{
    public sealed class Receiver
    {
        public void Dequeue()
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

                    consumer.Received += (model, ea) =>
                        {
                            byte[] body = ea.Body;
                            string message = Encoding.UTF8.GetString(body);
                            var mail = JsonConvert.DeserializeObject<Mail>(message);
                            MailProcessor.Process(mail);
                            //TODO: undelivered messages
                        };

                    channel.BasicConsume(queue: QueueSettings.QueueName,
                                         noAck: true,
                                         consumer: consumer);
                }
            }
        }
    }
}