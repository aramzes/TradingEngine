using InstrumentCS;
using System;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.Orders;

namespace TradingEngineServer.Orderbook
{
    public class Orderbook : IRetrievalOrderbook
    {
        private readonly Security _Instrument;
        private readonly Dictionary<long, OrderbookEntry> _Orders = new Dictionary<long, OrderbookEntry>();
        private readonly SortedSet<Limit> _AskLimits = new SortedSet<Limit>(AskLimitComparer.Comparer);
        private readonly SortedSet<Limit> _BidLimits = new SortedSet<Limit>(BidLimitComparer.Comparer);

        public int count => _Orders.Count;
        public Orderbook(Security Instrument)
        {
            _Instrument = Instrument;
        }
        public void AddOrder(Order order)
        {
            var baseLimit = new Limit(order.Price);
            AddOrder(order, baseLimit, order.IsBuyerSide ? _BidLimits : _AskLimits, _Orders);

        }

        public void ChangeOrder(ModifyOrder order)
        {
            if(_Orders.TryGetValue(order.OrderId, out OrderbookEntry orderbookEntry))
            {
                RemoveOrder(order.ToCancelOrder());
                AddOrder(order.ToNewOrder(), orderbookEntry.ParentLimit, order.IsBuyerSide ? _BidLimits : _AskLimits, _Orders);
            }
        }

        public bool containsOrder(long OrderId)
        {
            return _Orders.ContainsKey(OrderId); 
        }

        public List<OrderbookEntry> GetAskOrders()
        {
            List<OrderbookEntry> AskOrders = new List<OrderbookEntry>();
            foreach(var limit in _AskLimits)
            {
                if (limit.isEmpty)
                    continue;
                else
                {
                    OrderbookEntry HeadPointer = limit.Head;
                    while(HeadPointer != null)
                    {
                        AskOrders.Add(HeadPointer);
                        HeadPointer = HeadPointer.Next;
                    }
                }
            }
            return AskOrders;
        }

        public List<OrderbookEntry> GetBidOrders()
        {
            List<OrderbookEntry> BidOrders = new List<OrderbookEntry>();
            foreach (var limit in _BidLimits)
            {
                if (limit.isEmpty)
                    continue;
                else
                {
                    OrderbookEntry HeadPointer = limit.Head;
                    while (HeadPointer != null)
                    {
                        BidOrders.Add(HeadPointer);
                        HeadPointer = HeadPointer.Next;
                    }
                }
            }
            return BidOrders;
        }

        public OrderbookSpread GetOrderBookSpread()
        {
            long? BestBid = null;
            long? BestAsk = null;

            if (_AskLimits.Count != 0 && !_AskLimits.Min.isEmpty)
                BestAsk = _AskLimits.Min.PriceLevel;
            if (_BidLimits.Count != 0 && !_BidLimits.Min.isEmpty)
                BestBid= _BidLimits.Min.PriceLevel;

            return new OrderbookSpread(BestAsk, BestBid);
        }

        public void RemoveOrder(CancelOrder order)
        {
            if(_Orders.TryGetValue(order.OrderId, out OrderbookEntry orderbookEntry))
            {
                RemoveOrder(order.OrderId, orderbookEntry, _Orders);
            }
        }

        // STATIC FUNCTIONS
        private static void AddOrder(Order order, Limit baseLimit, SortedSet<Limit> limitLevels, Dictionary<long, OrderbookEntry> internalBook)
        {
            OrderbookEntry orderbookEntry = new OrderbookEntry(order, baseLimit);
            if (limitLevels.TryGetValue(baseLimit, out Limit limit))
            {
                if(limit.Head == null)
                {
                    limit.Head = orderbookEntry;
                    limit.Tail = orderbookEntry;
                }
                else
                {
                    OrderbookEntry tailPointer = limit.Tail;
                    tailPointer.Next = orderbookEntry;
                    orderbookEntry.Previous = tailPointer;
                    limit.Tail = orderbookEntry;
                }
            } 
            else
            {
                limitLevels.Add(baseLimit);
                baseLimit.Head = orderbookEntry;
                baseLimit.Tail = orderbookEntry;
            }
            internalBook.Add(order.OrderId, orderbookEntry);
        }

        private static void RemoveOrder(long orderId, OrderbookEntry orderbookEntry, Dictionary<long, OrderbookEntry> internalBook)
        {
            if (orderbookEntry.Next != null && orderbookEntry.Previous != null)
            {
                orderbookEntry.Next.Previous = orderbookEntry.Previous;
                orderbookEntry.Previous.Next = orderbookEntry.Next;
            } else if (orderbookEntry.Previous != null)
            {
                orderbookEntry.Previous.Next = null;

            } else if (orderbookEntry.Next != null)
            {
                orderbookEntry.Next.Previous = null;
            }

            if(orderbookEntry.ParentLimit.Head == orderbookEntry && orderbookEntry.ParentLimit.Tail == orderbookEntry)
            {
                orderbookEntry.ParentLimit.Head = null;
                orderbookEntry.ParentLimit.Tail = null;
            } 
            else if (orderbookEntry.ParentLimit.Head == orderbookEntry)
            {
                orderbookEntry.ParentLimit.Head = orderbookEntry.Next;
            }
            else if (orderbookEntry.ParentLimit.Tail == orderbookEntry)
            {
                orderbookEntry.ParentLimit.Tail = orderbookEntry.Previous;
            }

            internalBook.Remove(orderId);
        }

    }
}
