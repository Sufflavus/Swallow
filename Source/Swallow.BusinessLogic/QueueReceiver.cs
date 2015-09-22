using Swallow.BusinessLogic.Interfaces;
using Swallow.QueueManager.Interfaces;

namespace Swallow.BusinessLogic
{
    public sealed class QueueReceiver : IQueueReceiver
    {
        private readonly IQueueFactory _factory;
        private readonly IMailProcessor _mailProcessor;

        public QueueReceiver(IQueueFactory queueFactory, IMailProcessor mailProcessor)
        {
            _factory = queueFactory;
            _mailProcessor = mailProcessor;
        }

        public void Dequeue()
        {
            using (IQueueWrapper queue = _factory.CreateReceiver(QueueSettings.QueueName))
            {
                var mail = queue.Dequeue<Mail>();
                _mailProcessor.Process(mail);
            }
        }
    }
}