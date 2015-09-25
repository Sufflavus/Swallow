using Swallow.QueueManager.Interfaces;

namespace Swallow.UnitTests.Mocks
{
    public class QueueFactoryMock : IQueueFactory
    {
        public IQueueWrapper CreateReceiver(string queueName)
        {
            return new QueueWrapperMock();
        }

        public IQueueWrapper CreateSender(string queueName)
        {
            return new QueueWrapperMock();
        }
    }
}