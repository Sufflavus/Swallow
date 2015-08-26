using System.Collections.Generic;
using Nelibur.ObjectMapper;
using Nelibur.ServiceModel.Services.Operations;
using Swallow.Contracts;

namespace Swallow.Service
{
    public sealed class MailProcessor : IPutOneWay<PutMailCommand>
    {
        private static readonly List<Mail> _mails = new List<Mail>();

        public MailProcessor()
        {
            TinyMapper.Bind<PutMailCommand, Mail>();
        }

        public void PutOneWay(PutMailCommand request)
        {
            var mail = TinyMapper.Map<Mail>(request);
            _mails.Add(mail);
        }
    }
}