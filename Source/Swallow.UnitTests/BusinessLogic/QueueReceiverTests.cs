using Moq;
using Swallow.BusinessLogic;
using Swallow.BusinessLogic.Interfaces;
using Swallow.QueueManager.Interfaces;
using Xunit;

namespace Swallow.UnitTests.BusinessLogic
{
    public class QueueReceiverTests
    {
        [Fact]
        public void Dequeue_ProcessCalledInMailProcessor()
        {
            var factory = new Mock<IQueueFactory>();
            var mailProcessor = new Mock<IMailProcessor>();
            var queueWrapper = new Mock<IQueueWrapper>();
            var mail = new Mail();
            queueWrapper.Setup(x => x.Dequeue<Mail>()).Returns(mail);
            factory.Setup(x => x.CreateReceiver(QueueSettings.QueueName)).Returns(queueWrapper.Object);
            var queueReceiver = new QueueReceiver(factory.Object, mailProcessor.Object);

            queueReceiver.Dequeue();

            factory.Verify(x => x.CreateReceiver(QueueSettings.QueueName));
            queueWrapper.Verify(x => x.Dequeue<Mail>());
            mailProcessor.Verify(x => x.Process(mail));
        }
    }
}