using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasketLibrary.Models;
using BasketLibrary.Repositories;
using Microsoft.Extensions.Logging;

namespace BasketLibrary.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> logger;
        private readonly IArticleCatalogService articleCatalogService;
        private readonly IOrderRepository orderRepository;

        public OrderService(ILogger<OrderService> logger, IArticleCatalogService articleCatalogService, IOrderRepository orderRepository)
        {
            this.logger = logger;
            this.articleCatalogService = articleCatalogService;
            this.orderRepository = orderRepository;
        }
        public string CreateNewOrder()
        {
            var id = Guid.NewGuid().ToString();
            orderRepository.Add(new OrderDto { Id = id, OrderItems = new List<OrderItemDto>() });
            return id;
        }

        public Response<string> AddArticleToOrder(string orderId, string articleId, int quantity = 1)
        {
            OrderDto order;
            if (quantity < 1 || quantity > 1000)
            {
                return Response<string>.Error("Quantity out of range!");
            }
            var articleResponse = articleCatalogService.FindArticeById(articleId);
            if (articleResponse.IsError)
            {
                return Response<string>.Error($"Article with id: {articleId} not found!");
            }
            try
            {
                order = FindOrderById(orderId);

            }
            catch (Exception)
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

        public Response<string> RegisterDiscount(string orderId, IDiscountService discountService)
        {
            try
            {
                var order = FindOrderById(orderId);
            }
            catch (Exception)
            {
                return Response<string>.Error($"Order with id: {orderId} not found!");
            }

            try
            {
                var discountServiceId = orderRepository.AddDiscountToOrder(orderId, discountService);
                return Response<string>.Success($"Discount with id: {discountServiceId} added to order.");
            }
            catch (Exception ex)
            {
                return Response<string>.Error(ex.Message);
            }
        }

        public decimal GetOrderTotalPrice(string orderId)
        {
            // TODO if needed introduce caching layer
            var textBuilder = new StringBuilder();
            PrintOrderItemHeader(textBuilder, orderId);

            var items = orderRepository.GetAllOrderItems(orderId).ToList();
            // print items
            foreach (var item in items)
            {
                PrintOrderItem(textBuilder, item);
            }
            var sum = items.Sum(i => i.Article.Price * i.Quantity);
            decimal discountPrice = 0;
            foreach (var discountService in orderRepository.GetAllOrderDiscounts(orderId))
            {
                var response = discountService.CalculateDiscount(items);
                if (response.IsError == false)
                {
                    discountPrice += response.Data.Article.Price * response.Data.Quantity;
                    // print discount
                    PrintOrderItem(textBuilder, response.Data);

                }
            }
            var totalSum = sum - discountPrice;
            if (totalSum < 0)
            {
                totalSum = 0;
            }
            // print total sum
            PrintOrderItemFooter(textBuilder, totalSum);
            logger.LogInformation(textBuilder.ToString());
            return totalSum;
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
            return orderRepository.GetOrderById(orderId);
        }

        private void PrintOrderItemHeader(StringBuilder builder, string orderId)
        {
            builder
                .AppendLine($"Order: {orderId}")
                .AppendLine("--------------------------------------------------")
                .Append("Name".PadRight(20))
                .Append("| Quantity".PadRight(10))
                .Append("| Price".PadRight(10))
                .AppendLine("| Total".PadRight(15))
                .AppendLine("--------------------------------------------------");
        }

        private void PrintOrderItem(StringBuilder builder, OrderItemDto item)
        {
            builder
                .Append(item.Article.Name.PadRight(20))
                .Append($"| {item.Quantity}".PadRight(10))
                .Append($"| {item.Article.Price}".PadRight(10))
                .AppendLine($"| {item.Quantity * item.Article.Price}".PadRight(15));
        }

        private void PrintOrderItemFooter(StringBuilder builder, decimal totalSum)
        {
            builder
                .AppendLine("--------------------------------------------------")
                .AppendLine("Total:".PadRight(42) + totalSum);
        }
    }
}