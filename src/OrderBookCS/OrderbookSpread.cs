using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orderbook
{
    public class OrderbookSpread
    {
        public long? Ask { get; private set; }
        public long? Bid { get; private set; }

        public OrderbookSpread(long? ask, long? bid)
        {
            Ask = ask;
            Bid = bid;
        }

        public long? Spread
        {
            get
            {
                if (Ask.HasValue && Bid.HasValue)
                    return Ask.Value - Bid.Value;
                else return null;
            }
           
        }
    }
}
