using System.Collections.Generic;
using System.Reflection;
using Nelibur.ObjectMapper;
using Nelibur.ServiceModel.Services.Operations;
using Ninject;
using Swallow.BusinessLogic;
using Swallow.BusinessLogic.Interfaces;
using Swallow.Contracts;

namespace Swallow.Service
{
    public sealed class MailHandler : IPutOneWay<PutMailCommand>
    {
        private readonly IQueueSender _queueSender;

        public MailHandler()
        {
            TinyMapper.Bind<PutMailCommand, Mail>();

            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            //var _mailProcessor = kernel.Get<IMailProcessor>();
            _queueSender = kernel.Get<IQueueSender>();
        }

        public void PutOneWay(PutMailCommand request)
        {
            var mail = TinyMapper.Map<Mail>(request);
            _queueSender.Enqueue(mail);
        }
    }
}