namespace Swallow.BusinessLogic.Interfaces
{
    public interface IQueueManager
    {
        void Enqueue(Mail mail);
        void InitializeQueueListener();
    }
}