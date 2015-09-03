using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Swallow.BusinessLogic
{
    public sealed class Mail
    {
        public string Body { get; set; }
        public List<string> Receivers { get; set; }
        public string Sender { get; set; }
        public string Subject { get; set; }

        public byte[] ToBytes()
        {
            string jsonMail = JsonConvert.SerializeObject(this);
            byte[] result = Encoding.UTF8.GetBytes(jsonMail);
            return result;
        }

        public static Mail FromBytes(byte[] data)
        {
            string message = Encoding.UTF8.GetString(data);
            var mail = JsonConvert.DeserializeObject<Mail>(message);
            return mail;
        }
    }
}