using System;
using Nelibur.ServiceModel.Clients;
using Swallow.Client.Properties;
using Swallow.Contracts;

namespace Swallow.Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new JsonServiceClient(Settings.Default.ServiceAddress);
            client.Put(new PutMailCommand {Sender = "sender@mail.com"});
            Console.WriteLine("Mail has added");
            Console.ReadKey();
        }
    }
}