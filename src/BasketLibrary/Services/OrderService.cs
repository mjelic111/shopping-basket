using System;
using System.Collections.Generic;
using System.Linq;
using BasketLibrary.Models;
using Microsoft.Extensions.Logging;

namespace BasketLibrary.Services
{
    public class OrderService : IOrderService
    {
        private List<OrderDto> orders = new List<OrderDto>();
        private readonly ILogger<OrderService> logger;

        public OrderService(ILogger<OrderService> logger)
        {
            this.logger = logger;
        }
        public string CreateNewOrder()
        {
            var id = Guid.NewGuid().ToString();
            orders.Add(new OrderDto { Id = id, OrderItems = new List<OrderItemDto>() });
            return id;
        }
        public void AddArticleToOrder(string orderId, string articleId, int quantity = 1)
        {
            // var order = orders.Where(o => orderId.Equals(o.Id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

            throw new System.NotImplementedException();
        }

        public void RemoveArticleFromOrder(string orderId, string articleId)
        {
            throw new System.NotImplementedException();
        }

        public void SetArticleQuantity(string orderId, string articleId, int quantity)
        {
            throw new System.NotImplementedException();
        }

        public double GetOrderTotalPrice(string orderId)
        {
            throw new System.NotImplementedException();
        }

        public void PrintOrder()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterDiscount(IDiscountService discountService)
        {
            throw new System.NotImplementedException();
        }

    }
}