using System;
using System.Collections.Generic;
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
            client.Put(new PutMailCommand
                {
                    Sender = "sender@mail.com",
                    Receivers = new List<string>
                        {
                            "receiver1@mail.com",
                            "receiver2@mail.com",
                        }
                });
            Console.WriteLine("Mail has been added");
            Console.ReadKey();
        }
    }
}