using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders
{
    public class BidLimitComparer : IComparer<Limit>
    {
        public static BidLimitComparer Comparer { get; } = new BidLimitComparer();
        public int Compare(Limit x, Limit y)
        {
            if (x.PriceLevel == y.PriceLevel)
                return 0;
            else if (x.PriceLevel < y.PriceLevel)
                return 1;
            else return -1;
        }
    }

    public class AskLimitComparer : IComparer<Limit>
    {
        public static AskLimitComparer Comparer { get; } = new AskLimitComparer();
        public int Compare(Limit x, Limit y)
        {
            if (x.PriceLevel == y.PriceLevel)
                return 0;
            else if (x.PriceLevel > y.PriceLevel)
                return 1;
            else return -1;
        }
    }
}


