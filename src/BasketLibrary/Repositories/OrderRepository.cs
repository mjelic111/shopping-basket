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
            var order = orders.Where(o => o.Id.Equals(id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (order == null)
            {
                throw new Exception($"Order with id: {id}, not found!");
            }
            return order;
        }

        public string AddDiscountToOrder(string orderId, IDiscountService discountService)
        {
            var order = GetOrderById(orderId);
            if (order.DiscountServices.Any(d => d.Id.Equals(discountService.Id, StringComparison.OrdinalIgnoreCase)))
            {
                throw new Exception($"Discount with id: {discountService.Id} allready registered!");
            }
            order.DiscountServices.Add(discountService);
            return discountService.Id;

        }

        public IEnumerable<OrderItemDto> GetAllOrderItems(string orderId)
        {
            var order = GetOrderById(orderId);
            return order.OrderItems;
        }

        public IEnumerable<IDiscountService> GetAllOrderDiscounts(string orderId)
        {
            var order = GetOrderById(orderId);
            return order.DiscountServices;
        }
    }
}