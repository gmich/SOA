using Autofac;
using System;
using Processing.Consumer;
using Billing.Consumer;
using UserManagement.Consumer;
using MassTransit;
using System.Collections.Generic;

namespace ServiceBus
{
    public class Service : IDisposable
    {
        private readonly IContainer container;
        private readonly BusHandle busHandle;

        public Service()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<ProcessingConsumerModule>();
            builder.RegisterModule<BillingConsumerModule>();
            builder.RegisterModule<UserManagementConsumerModule>();

            container = builder.Build();

            busHandle = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                //TODO: get info from app.config
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint(host, "every_consumer_queue", e =>
                {
                    var consumers = container.Resolve<IEnumerable<IConsumer>>();
                    throw new Exception("Consumers dont resolve using IConsumer. Find another way");
                    foreach (var consumer in consumers)
                    {
                        e.Consumer(() => consumer);
                    }
                });
            }).Start();

        }

        #region Finalization

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Service()
        {

            Dispose(false);
        }
   
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                container.Dispose();
                busHandle.Stop();
                busHandle.Dispose();
            }
        }

        #endregion

    }
}
