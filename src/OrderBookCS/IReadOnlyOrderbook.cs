using System;

namespace TradingEngineServer.Orderbook
{
    public interface IReadOnlyOrderBook
    {
        bool containsOrder(long OrderId);
        OrderbookSpread GetOrderBookSpread();
    }
}
