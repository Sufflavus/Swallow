using System.Collections.Generic;

namespace Swallow.Contracts
{
    public sealed class PutMailCommand
    {
        public PutMailCommand()
        {
            Receivers = new List<string>();
        }

        public string Body { get; set; }
        public List<string> Receivers { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
    }
}