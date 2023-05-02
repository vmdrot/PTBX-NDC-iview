﻿using System;
using System.Collections.Generic;

namespace Albelli.OrderManagement.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public IEnumerable<OrderLine> Items { get; set; }
        public double MinPackageWidth { get; set; }
    }
}
