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
    }
}