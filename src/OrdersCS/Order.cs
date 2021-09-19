using OrdersCS;
using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders
{
    public class Order : IOrderCore
    {
        private readonly IOrderCore _IOrderCore;

        public long Price { get; private set; }
        public uint InitialQuantity { get; private set; }
        public uint CurrentQuantity { get; private set; }
        public bool IsBuyerSide { get; private set; }

        public long OrderId => _IOrderCore.OrderId;

        public string Username => _IOrderCore.Username;

        public int SecurityId => _IOrderCore.SecurityId;

        public Order(IOrderCore orderCore, long price, uint initialQuanity, bool isBuyerSide)
        {
            Price = price;
            InitialQuantity = initialQuanity;
            IsBuyerSide = isBuyerSide;

            _IOrderCore = orderCore;
        }

        public Order(ModifyOrder modifyOrder)
            : this(modifyOrder, modifyOrder.Price, modifyOrder.Quantity, modifyOrder.IsBuyerSide)
        {
        }

        public void increaseQuantity(uint quantityDelta)
        {
            CurrentQuantity += quantityDelta;
        }
        
        public void decrementQuantity(uint quantityDelta)
        {
            if (quantityDelta > CurrentQuantity)
                    throw new InvalidOperationException($"QuantityDelta > currentQuantity for orderId:{OrderId}");

            CurrentQuantity -= quantityDelta;
        }
    }
}
