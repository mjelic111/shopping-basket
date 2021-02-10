using System;
using System.Collections.Generic;
using System.Linq;
using BasketLibrary.Models;
using Microsoft.Extensions.Logging;

namespace BasketLibrary.Services
{
    public class OrderService : IOrderService
    {
        // TODO replace with proper storage
        private List<OrderDto> orders = new List<OrderDto>();
        private readonly ILogger<OrderService> logger;
        private readonly IArticleCatalogService articleCatalogService;

        public OrderService(ILogger<OrderService> logger, IArticleCatalogService articleCatalogService)
        {
            this.articleCatalogService = articleCatalogService;
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
            if (quantity < 1 || quantity > 1000)
            {
                return Response<string>.Error("Quantity out of range!");
            }
            var articleResponse = articleCatalogService.FindArticeById(articleId);
            if (articleResponse.IsError)
            {
                return Response<string>.Error($"Article with id: {articleId} not found!");
            }
            var order = FindOrderById(orderId);
            if (order == null)
            {
                return Response<string>.Error($"Order with id: {orderId} not found!");
            }
            var article = articleResponse.Data;

            var orderItem = order.OrderItems.Where(oi => oi.Id.Equals(article.Id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (orderItem == null)
            {
                // add order item
                order.OrderItems.Add(new OrderItemDto { Id = article.Id, Article = article, Quantity = quantity });
                return Response<string>.Success($"Article {article.Name} added to order, new quantity: {quantity}!");
            }
            else
            {
                orderItem.Quantity += quantity;
            }
            return Response<string>.Success($"Article {article.Name} added to order, new quantity: {orderItem.Quantity}!");
        }

        public Response<string> UpdateOrderItemQuantity(string orderId, string articleId, int quantity)
        {
            // set to 0 to remove order item
            if (quantity < 0 || quantity > 1000)
            {
                return Response<string>.Error("Quantity out of range!");
            }
            var articleResponse = articleCatalogService.FindArticeById(articleId);
            if (articleResponse.IsError)
            {
                return Response<string>.Error($"Article with id: {articleId} not found!");
            }
            var order = FindOrderById(orderId);
            if (order == null)
            {
                return Response<string>.Error($"Order with id: {orderId} not found!");
            }
            var article = articleResponse.Data;

            var orderItem = order.OrderItems.Where(oi => oi.Id.Equals(article.Id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (orderItem == null)
            {
                // order item not found
                return Response<string>.Error($"Order item with id: {article.Id} not found!");
            }
            else
            {
                orderItem.Quantity = quantity;
                if (orderItem.Quantity == 0)
                {
                    // remove order item
                    order.OrderItems.Remove(orderItem);
                    return Response<string>.Success($"Article {article.Name} removed to order!");
                }
            }
            return Response<string>.Success($"Article {article.Name} added to order, new quantity: {orderItem.Quantity}!");
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

        public Response<IEnumerable<OrderItemDto>> GetAllOrderItems(string orderId)
        {
            var order = FindOrderById(orderId);
            if (order == null)
            {
                return Response<IEnumerable<OrderItemDto>>.Error($"Order with id: {orderId} not found!");
            }
            return Response<IEnumerable<OrderItemDto>>.Success(order.OrderItems);
        }
        private OrderDto FindOrderById(string orderId)
        {
            return orders.Where(o => o.Id.Equals(orderId, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
        }

    }
}