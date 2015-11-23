using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processing.Core.API
{
    public interface IProcessingService
    {
        IEnumerable<Item> GetProcessedItems(int amount);
    }
}
