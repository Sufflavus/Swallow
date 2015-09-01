using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Swallow.BusinessLogic.Queue
{
    public sealed class Sender
    {
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
    }
}