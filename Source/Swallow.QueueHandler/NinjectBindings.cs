using Ninject.Modules;
using Swallow.BusinessLogic;
using Swallow.BusinessLogic.Interfaces;
using Swallow.QueueManager;
using Swallow.QueueManager.Interfaces;

namespace Swallow.QueueHandler
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMailProcessor>().To<MailProcessor>();
            Bind<IQueueReceiver>().To<QueueReceiver>();
            Bind<IQueueFactory>().To<QueueFactory>();
        }
    }
}