using Autofac;
using System;
using Processing.Consumer;
using Billing.Consumer;
using UserManagement.Consumer;
using MassTransit;
using System.Reflection;
using ServiceBus.Config;
using MassTransit.Log4NetIntegration.Logging;
[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "log4net", Watch = true)]

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

            Log4NetLogger.Use();
            busHandle =
                  BusConfig
                  .ForRabbitMq(new[]
                  {
                    new EndpointConfiguration(
                        "dont_put_all_consumers_in_the_same_queue",
                        ecfg => ScanContractAssembly(container, ecfg, "Contracts"))
                  })
                  .Start();
        }

        public void ScanContractAssembly(
            IComponentContext container,
            IRabbitMqReceiveEndpointConfigurator config,
            string assemblyName)
        {
            MethodInfo method = typeof(Service).GetMethod("ResolveConsumer");
            var bindings = new object[] { container, config };
            foreach (Type type in Assembly.Load(assemblyName).GetTypes())
            {
                method.MakeGenericMethod(type).Invoke(null, bindings);
            }
        }

        public static void ResolveConsumer<TContract>(
            IComponentContext container,
            IRabbitMqReceiveEndpointConfigurator config)
            where TContract : class
        {
            config.Consumer(() => container.Resolve<IConsumer<TContract>>());
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
