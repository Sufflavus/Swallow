namespace Swallow.BusinessLogic.Interfaces
{
    public interface IQueueSender
    {
        void Enqueue(Mail mail);
    }
}