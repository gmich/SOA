using Autofac;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public class Startup
    {
        private readonly IContainer container;

        public Startup()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DalEFModule>();

            container = builder.Build();
        }


    }
}
