using Autofac;
using Contracts;
using MassTransit;
using Processing.Core;
using Processing.Core.API;

namespace Processing.Consumer
{
    public class ProcessingConsumerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<ProcessingCoreModule>();

            builder.Register(c => new ItemConsumer(c.Resolve<IProcessingService>()))
                    .As<IConsumer<RequestItem>>();
        }

    }
}
