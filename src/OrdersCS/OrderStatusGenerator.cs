using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders
{
    public sealed class OrderStatusGenerator
    {
        public static CancelOrderStatus GenerateCancelOrderStatus(CancelOrder cancelOrder)
        {
            return new CancelOrderStatus();
        }

        public static NewOrderStatus GenerateNewOrderStatus(Order newOrder)
        {
            return new NewOrderStatus();
        }
        public static ModifyOrderStatus GenerateModifyOrderStatus(ModifyOrderStatus modifyOrder)
        {
            return new ModifyOrderStatus();
        }
    }
}
