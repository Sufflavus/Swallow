using System.Collections.Generic;

namespace Swallow.Service
{
    public sealed class Mail
    {
        public string Body { get; set; }
        public List<string> Receivers { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }
    }
}