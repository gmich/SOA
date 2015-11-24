using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Processing.Core.API;
using DAL.Repositories.API;

namespace Processing.Core.Impl
{
    internal class ProcessingService : IProcessingService
    {
        private readonly IRepository<Item> itemRepository;

        public ProcessingService(IRepository<Item> itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public IEnumerable<Item> GetItemsByName(string name)
        {
            return itemRepository.Query(item => item.Name == name);
        }
    }
}
