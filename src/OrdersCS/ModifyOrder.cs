using OrdersCS;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders
{
    public class ModifyOrder : IOrderCore
    {
        private readonly IOrderCore _IOrderCore;
        public long Price { get; private set; }

        public uint Quantity { get; private set; }
        public bool IsBuyerSide { get; private set; }
        public long OrderId => _IOrderCore.OrderId;

        public string Username => _IOrderCore.Username;

        public int SecurityId => _IOrderCore.SecurityId;

        public ModifyOrder(IOrderCore orderCore, long modifyPrice, uint modifyQuantity, bool isBuyerSide)
        {
            _IOrderCore = orderCore;
            Price = modifyPrice;
            Quantity = modifyQuantity;
            IsBuyerSide = isBuyerSide;
        }

        public CancelOrder ToCancelOrder()
        {
            return new CancelOrder(this);
        }

        public Order ToNewOrder()
        {
            return new Order(this);
        }
    }
}
