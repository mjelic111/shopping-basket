using System;
using System.Collections.Generic;
using System.Linq;
using BasketLibrary.Models;
using BasketLibrary.Services;

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
            order.DiscountServices = new List<IDiscountService>();
            orders.Add(order);
            return order.Id;
        }

        public OrderDto GetOrderById(string id)
        {
            return orders.Where(o => o.Id.Equals(id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
        }

        public string AddDiscountToOrder(string orderId, IDiscountService discountService)
        {
            var order = GetOrderById(orderId);
            if (order == null)
            {
                throw new Exception($"Order with id: {orderId}, not found!");
            }
            if (order.DiscountServices.Any(d => d.Id.Equals(discountService.Id, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception($"Discount with id: {discountService.Id} allready registered!");
            }
            order.DiscountServices.Add(discountService);
            return discountService.Id;

        }

    }
}