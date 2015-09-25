using System.Collections.Generic;
using Swallow.QueueManager;
using Swallow.QueueManager.Interfaces;

namespace Swallow.UnitTests.Mocks
{
    public class QueueWrapperMock : IQueueWrapper
    {
        private List<QueueItemEntity> _items = new List<QueueItemEntity>();

        public void Dispose()
        {
            _items = new List<QueueItemEntity>();
        }

        public T Dequeue<T>() where T : QueueItemEntity
        {
            return (T) _items[_items.Count - 1];
        }

        public void Enqueue<T>(string queueName, T entity) where T : QueueItemEntity
        {
            _items.Add(entity);
        }
    }
}