using System;
using System.Collections.Generic;
using System.Text;

namespace TradingEngineServer.Orders
{
    public class Limit
    {
        private long Price;

        public Limit(long price)
        {
            Price = price;
        }

        public long PriceLevel { get; set; }
        public OrderbookEntry Head { get; set; }
        public OrderbookEntry Tail { get; set; }

        public uint GetLevelOrderCount()
        {
            uint OrderCount = 0;
            OrderbookEntry HeadPointer = Head;
            while(HeadPointer != null)
            {
                if (HeadPointer.CurrentOrder.CurrentQuantity != 0)
                    OrderCount++;
                HeadPointer = HeadPointer.Next;
            }
            return OrderCount;
        }

        public uint GetLevelOrderQuantity()
        {
            uint Quantity = 0;
            OrderbookEntry HeadPointer = Head;
            while (HeadPointer != null)
            {
                Quantity += HeadPointer.CurrentOrder.CurrentQuantity;
                HeadPointer = HeadPointer.Next;
            }
            return Quantity;
        }
        public List<OrderRecord> GetLevelOrderRecords()
        {
            List<OrderRecord> OrderRecords = new List<OrderRecord>();
            uint TheoriticalQueuePosition = 0;
            OrderbookEntry HeadPointer = Head;
            while(HeadPointer != null)
            {
                if (HeadPointer.CurrentOrder.CurrentQuantity != 0)
                {
                    var CurrentOrder = HeadPointer.CurrentOrder;
                    OrderRecords.Add(new OrderRecord(CurrentOrder.OrderId, CurrentOrder.CurrentQuantity, this.PriceLevel,
                        CurrentOrder.IsBuyerSide, CurrentOrder.Username, CurrentOrder.SecurityId,
                        TheoriticalQueuePosition));
                }
                    TheoriticalQueuePosition++;
                    HeadPointer = HeadPointer.Next;
            }
            return OrderRecords;
        }
        public bool isEmpty
        {
            get
            {
                return Head == null && Tail == null;
            }
        }

        public Side Side
        {
            get
            {
                if (isEmpty)
                    return Side.Unknown;
                else
                {
                    return Head.CurrentOrder.IsBuyerSide ? Side.Bid : Side.Ask;
                }
            }
        }
    }
}
