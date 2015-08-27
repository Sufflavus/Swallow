using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Swallow.BusinessLogic
{
    public sealed class MailProcessor
    {
        public void Dequeue()
        {
            var factory = new ConnectionFactory {HostName = "localhost"};
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                        {
                            byte[] body = ea.Body;
                            string message = Encoding.UTF8.GetString(body);
                        };
                    channel.BasicConsume(queue: "hello",
                                         noAck: true,
                                         consumer: consumer);
                }
            }
        }
    }
}