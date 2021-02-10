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

        public Response<string> AddArticleToOrder(string orderId, string articleId, int quantity = 1)
        {
            throw new NotImplementedException();
        }

        public Response<string> SetArticleQuantity(string orderId, string articleId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Response<string> RemoveArticleFromOrder(string orderId, string articleId)
        {
            throw new NotImplementedException();
        }

        public Response<string> RegisterDiscount(IDiscountService discountService)
        {
            throw new NotImplementedException();
        }

        public double GetOrderTotalPrice(string orderId)
        {
            throw new NotImplementedException();
        }

        public void PrintOrder()
        {
            throw new NotImplementedException();
        }
    }
}