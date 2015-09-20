using System;

namespace Swallow.QueueManager.Interfaces
{
    public interface IQueueWrapper : IDisposable
    {
        void Enqueue<T>(string queueName, T entity) where T : QueueItemEntity;
        T Dequeue<T>() where T : QueueItemEntity;
    }
}