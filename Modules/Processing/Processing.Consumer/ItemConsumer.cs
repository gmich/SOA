using Contracts;
using MassTransit;
using System.Linq;
using System.Threading.Tasks;
using Processing.Consumer.Messages;
using Processing.Core.API;

namespace Processing.Consumer
{
    internal class ItemConsumer :
        IConsumer<RequestItem>
    {
        private readonly IProcessingService service;

        public ItemConsumer(IProcessingService service)
        {
            this.service = service;
        }

        #pragma warning disable 1998
        public async Task Consume(ConsumeContext<RequestItem> context)
        {
            var items = service.GetItemsByName(context.Message.ItemName);
            context.Respond(new ItemResponseMessage(items.First().Info));
        }
        #pragma warning disable 1998
    }

}
