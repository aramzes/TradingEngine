using System;
using System.Collections.Generic;
using System.Text;

namespace OrdersCS
{
    class OrderCore : IOrderCore
    {
        public long OrderId { get; private set; }
        public string Username { get; private set; }

        public int SecurityId { get; private set; }

        public OrderCore(long orderId, string username, int securityId)
        {
            OrderId = orderId;
            Username = username;
            SecurityId = securityId;
        }
    }
}
