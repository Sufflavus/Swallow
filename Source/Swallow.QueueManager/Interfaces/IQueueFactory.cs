namespace Swallow.QueueManager.Interfaces
{
    public interface IQueueFactory
    {
        IQueueWrapper CreateSender(string queueName);
        IQueueWrapper CreateReceiver(string queueName);
    }
}