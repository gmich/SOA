using MassTransit;
using MassTransit.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Consumer
{
    internal class ConsumerEndpoint :
        IEndpointSpecification
    {
        private readonly IConsumerFactory<ItemConsumer> _consumerFactory;

        public ConsumerEndpoint(IConsumerFactory<ItemConsumer> consumerFactory)
        {
            _consumerFactory = consumerFactory;
        }

        /// <summary>
        /// The default queue name for the endpoint, which can be overridden in the .config 
        /// file for the assembly
        /// </summary>
        public string QueueName => "book-meeting";

        /// <summary>
        /// The default concurrent consumer limit for the endpoint, which can be overridden in the .config 
        /// file for the assembly
        /// </summary>
        public int ConsumerLimit => Environment.ProcessorCount;

        /// <summary>
        /// Configures the endpoint, with consumers, handlers, sagas, etc.
        /// </summary>
        public void Configure(IReceiveEndpointConfigurator configurator)
        {
            configurator.Consumer(_consumerFactory);
        }
    }
}
