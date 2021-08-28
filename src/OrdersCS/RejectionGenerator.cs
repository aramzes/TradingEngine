﻿using OrdersCS;
using System;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.Orders;

namespace TradingEngineServer.Rejects
{
    public sealed class RejectionGenerator
    {
        public static Rejection GenerateOrderCoreReject(IOrderCore oc, RejectionReason rr)
        {
            return new Rejection(oc, rr);
        }
    }
}