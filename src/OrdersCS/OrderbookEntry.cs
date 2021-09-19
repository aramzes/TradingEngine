using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders
{
    public class OrderbookEntry
    {
        public Order CurrentOrder { get; private set; }
        public Limit ParentLimit { get; private set; }
        public DateTime CreationTime { get; private set; }

        public OrderbookEntry Next { get; set; }
        public OrderbookEntry Previous { get; set; }

        public OrderbookEntry(Order currentOrder, Limit parentLimit)
        {
            CurrentOrder = currentOrder;
            ParentLimit = parentLimit;
            CreationTime = DateTime.UtcNow;


        }


    }
}
