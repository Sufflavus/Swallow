using Ninject.Modules;
using Swallow.BusinessLogic;
using Swallow.BusinessLogic.Interfaces;

namespace Swallow.Service
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMailProcessor>().To<MailProcessor>();
            Bind<IQueueManager>().To<QueueManager>();
        }
    }
}