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
        private static readonly List<Mail> _mails = new List<Mail>();
        private readonly IQueueManager _queueManager;

        public MailHandler()
        {
            TinyMapper.Bind<PutMailCommand, Mail>();

            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            //var _mailProcessor = kernel.Get<IMailProcessor>();
            _queueManager = kernel.Get<IQueueManager>();
            _queueManager.InitializeQueueListener();
        }

        public void PutOneWay(PutMailCommand request)
        {
            var mail = TinyMapper.Map<Mail>(request);
            _mails.Add(mail);
            _queueManager.Enqueue(mail);
        }
    }
}