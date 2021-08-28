using OrdersCS;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders
{
    public class CancelOrder : IOrderCore
    {
        private readonly IOrderCore _IOrderCore;
        public long OrderId => _IOrderCore.OrderId;
        public string Username => _IOrderCore.Username;
        public int SecurityId => _IOrderCore.SecurityId;

        public CancelOrder(IOrderCore orderCore)
        {
            _IOrderCore = orderCore;
        }
    }

}
