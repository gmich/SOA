using Contracts;

namespace Processing.Consumer.Messages
{
    internal class ItemResponseMessage : ItemResponse
    {
        public ItemResponseMessage(string info)
        {
            ItemInfo = info;
        }

        public string ItemInfo { get; }
    }
}
