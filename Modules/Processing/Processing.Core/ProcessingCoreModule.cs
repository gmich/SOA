using Autofac;
using DAL.Repositories.API;
using Model;
using Processing.Core.API;
using Processing.Core.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Core
{
    public class ProcessingCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new ProcessingService(
                    c.Resolve<IRepository<Item>>()))
                    .As<IProcessingService>();
        }        

    }

}
