using MassTransit;
using System;

namespace ServiceBus.Config
{
    public class EndpointConfiguration
    {
        public EndpointConfiguration(string queueName, Action<IReceiveEndpointConfigurator> configuration)
        {
            QueueName = queueName;
            Configuration = configuration;
        }

        public string QueueName { get; }
        public Action<IReceiveEndpointConfigurator> Configuration { get; }

    }
}
