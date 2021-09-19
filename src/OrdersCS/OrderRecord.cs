using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders
{
    public record OrderRecord (long OrderId, uint Quantity, long Price, 
        bool IsBuyerSide, string Username, int SecurityId, uint TheoriticalQueuePosition);
}

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { };
}