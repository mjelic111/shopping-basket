using System;
using System.Collections.Generic;
using System.Linq;
using BasketLibrary.Models;

namespace BasketLibrary.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly List<OrderDto> orders;

        public OrderRepository()
        {
            orders = new List<OrderDto>();
        }
        public string Add(OrderDto order)
        {
            orders.Add(order);
            return order.Id;
        }

        public OrderDto GetOrderById(string id)
        {
            return orders.Where(o => o.Id.Equals(id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
        }
    }
}