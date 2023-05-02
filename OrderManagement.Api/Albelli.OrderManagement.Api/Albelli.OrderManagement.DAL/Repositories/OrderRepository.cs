using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Albelli.OrderManagement.DAL.Contracts.Repositories;
using Albelli.OrderManagement.Models;

namespace Albelli.OrderManagement.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static readonly IList<Order> _orders = new List<Order>
        {
            new Order { OrderId = 1, MinPackageWidth = 19, Items = new List<OrderLine>
            {
                new OrderLine { ProductType = ProductType.PhotoBook, Quantity = 1 }
            }}
        };

#pragma warning disable 1998 //just for the sake of cleaner build output
        public async Task Add(Order order)
#pragma warning restore 1998
        {
            order.OrderId = _orders.Max(o => o.OrderId) + 1;
            _orders.Add(order);
        }

#pragma warning disable 1998
        public async Task<Order> Get(int orderId)
#pragma warning restore 1998
        {
            return _orders.FirstOrDefault(x => x.OrderId == orderId);
        }
    }
}
