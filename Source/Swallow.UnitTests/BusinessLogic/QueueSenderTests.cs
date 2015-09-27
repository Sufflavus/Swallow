using Moq;
using Swallow.BusinessLogic;
using Swallow.QueueManager.Interfaces;
using Xunit;

namespace Swallow.UnitTests.BusinessLogic
{
    public class QueueSenderTests
    {
        [Fact]
        public void Enqueue_EnqueueCalledInQueueWrapper()
        {
            var factory = new Mock<IQueueFactory>();
            var queueWrapper = new Mock<IQueueWrapper>();
            factory.Setup(x => x.CreateSender(QueueSettings.QueueName)).Returns(queueWrapper.Object);
            var queueReceiver = new QueueSender(factory.Object);
            var mail = new Mail();

            queueReceiver.Enqueue(mail);

            factory.Verify(x => x.CreateSender(QueueSettings.QueueName));
            queueWrapper.Verify(x => x.Enqueue(QueueSettings.QueueName, mail));
            Assert.True(true);
        }
    }
}