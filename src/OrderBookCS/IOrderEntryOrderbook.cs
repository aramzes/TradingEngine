using System;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.Orders;

namespace TradingEngineServer.Orderbook
{
    public interface IOrderEntryOrderbook : IReadOnlyOrderBook
    {
        void AddOrder(Order order);
        void ChangeOrder(ModifyOrder order);
        void RemoveOrder(CancelOrder order);
    }
}
