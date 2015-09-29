using System;
using System.Reflection;
using Ninject;
using Swallow.BusinessLogic.Interfaces;

namespace Swallow.QueueHandler
{
    internal class Program
    {
        private static IQueueReceiver _queueReceiver;

        private static void Main(string[] args)
        {
            Console.WriteLine("QueueHandler is running");
            Console.WriteLine("Press any key to exit\n");

            InitQueueReceiver();

            while (!(Console.KeyAvailable))
            {
                _queueReceiver.Dequeue();
            }

            Console.ReadKey();
        }

        private static void InitQueueReceiver()
        {
            IKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            _queueReceiver = kernel.Get<IQueueReceiver>();
        }
    }
}