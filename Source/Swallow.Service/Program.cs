using System;
using System.ServiceModel.Web;
using Nelibur.ServiceModel.Services;
using Nelibur.ServiceModel.Services.Default;
using Swallow.Contracts;

namespace Swallow.Service
{
    internal class Program
    {
        private static WebServiceHost _service;

        private static void ConfigureService()
        {
            NeliburRestService.Configure(x => x.Bind<PutMailCommand, MailHandler>());
        }

        private static void Main(string[] args)
        {
            ConfigureService();

            _service = new WebServiceHost(typeof (JsonServicePerCall));
            _service.Open();

            Console.WriteLine("MailService is running");
            Console.WriteLine("Press any key to exit\n");

            Console.ReadKey();
            _service.Close();
        }
    }
}