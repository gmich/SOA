using Autofac;
using Processing.Consumer;
using Billing.Consumer;
using UserManagement.Consumer;
using DAL;

namespace ServiceBus
{

    public class Startup
    {
        public Service Service { get; }

        public Startup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DalEFModule>();
            builder.RegisterModule<ProcessingConsumerModule>();
            builder.RegisterModule<BillingConsumerModule>();
            builder.RegisterModule<UserManagementConsumerModule>();
            Service = Service.ForRabbitMQ(builder);
            //throw new Exception("Configure to run as a windows service with Topshelf");
        }
    }
    
}
