using OrdersCS;
using System;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.Orders;

namespace TradingEngineServer.Rejects
{
    public class Rejection : IOrderCore
    {
        public RejectionReason RejectionReason { get; private set; }
        public long OrderId => _orderCore.OrderId;
        public string Username => _orderCore.Username;
        public int SecurityId => _orderCore.SecurityId;

        private readonly IOrderCore _orderCore;

        public Rejection(IOrderCore rejectedOrder, RejectionReason rejectioNReason)
        {
            RejectionReason = rejectioNReason;

            _orderCore = rejectedOrder;
        }
    }
}