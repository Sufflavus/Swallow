namespace Swallow.QueueManager
{
    public interface IQueueFactory
    {
        QueueWrapper CreateSender(string queueName);
        QueueWrapper CreateReceiver(string queueName);
    }
}