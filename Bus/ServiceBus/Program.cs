using Autofac;
using Processing.Consumer;
using Billing.Consumer;
using UserManagement.Consumer;
using DAL;
using Topshelf;
using System;

namespace ServiceBus
{
    public class Program
    {
        public static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DalEFModule>();
            builder.RegisterModule<ProcessingConsumerModule>();
            builder.RegisterModule<BillingConsumerModule>();
            builder.RegisterModule<UserManagementConsumerModule>();

            Console.WriteLine("Initializing...");

            HostFactory.Run(x =>
            {
                x.Service<Service>(s =>
                {
                    s.ConstructUsing(name => Service.ForRabbitMQ(builder));
                    s.WhenStarted(bus => bus.Start());
                    s.WhenStopped(bus => bus.Stop());
                    s.WhenShutdown(bus => bus.Dispose());
                });
                x.RunAsLocalSystem();

                x.SetDescription("SOA MassTranssit RabbitMQ Host");
                x.SetDisplayName("SOA_BUS");
                x.SetServiceName("SOA_BUS");
            });

            Console.WriteLine("Service is initialized");
        }
    }
}
