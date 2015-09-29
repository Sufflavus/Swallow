using Ninject.Modules;
using Swallow.BusinessLogic;
using Swallow.BusinessLogic.Interfaces;

namespace Swallow.QueueHandler
{
    public class NinjectBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IQueueReceiver>().To<QueueReceiver>();
        }
    }
}