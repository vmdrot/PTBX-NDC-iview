using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Albelli.OrderManagement.Models
{
    public class OrderLine
    {
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
    }
}
