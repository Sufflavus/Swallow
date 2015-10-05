using Ninject.Modules;
using Swallow.BusinessLogic;
using Swallow.BusinessLogic.Interfaces;
using Swallow.QueueManager;
using Swallow.QueueManager.Interfaces;

namespace Swallow.Service
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMailProcessor>().To<MailProcessor>();
            Bind<IQueueSender>().To<QueueSender>();
            Bind<IQueueFactory>().To<QueueFactory>();
        }
    }
}