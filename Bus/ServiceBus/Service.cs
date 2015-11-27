using Autofac;
using System;
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
        public BusHandle BusHandle { get; private set; }

        public IBusControl Bus { get; }

        private Service(Func<EndpointConfiguration[], IBusControl> configurationMethod,
                        ContainerBuilder builder)
        {         
            container = builder.Build();

            Log4NetLogger.Use();
            Bus = configurationMethod(new[]
                  {
                    new EndpointConfiguration(
                        //WARN: dont put all consumers in the same queue
                        "service_queue",
                        //WARN: check for IReceiveEndpointRabbitMQ
                        ecfg => ScanContractAssembly(container, ecfg, "Contracts"))
                  });
           
            BusHandle = Bus.Start();
        }

        public Service Start()
        {
            BusHandle = Bus.Start();
            return this;
        }

        public void Stop()
        {
            BusHandle.Stop();
        }

        public IRequestClient<TRequest,TResponse> CreateRequestClient<TRequest,TResponse>()
            where TRequest : class
            where TResponse : class
        {
            return Bus.CreateRequestClient<TRequest, TResponse>(new Uri("rabbitmq://localhost/service_queue"), TimeSpan.FromSeconds(10));
        }

        public static Service ForRabbitMQ(ContainerBuilder builder) =>
            new Service(config => BusConfig.ForRabbitMq(config),builder);

        public static Service InMemory(ContainerBuilder builder) =>
            new Service(config => BusConfig.InMemory(config),builder);


        public void ScanContractAssembly(
            IComponentContext container,
            IReceiveEndpointConfigurator config,
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
            IReceiveEndpointConfigurator config)
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
                BusHandle.Stop();
                BusHandle.Dispose();
            }
        }

        #endregion

    }
}
