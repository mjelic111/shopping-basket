using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace BasketLibrary.Models
{
    public class Order : IOrder
    {
        public string Id { get; }
        private Dictionary<string, int> orderItems = new Dictionary<string, int>(); // key is Article.Id, value is quantity
        private double discountValue = 0;
        private List<IDiscount> discounts = new List<IDiscount>();
        private readonly ILogger<Order> logger;
        private readonly IArticleCatalog articleCatalog;

        public Order(ILogger<Order> logger, IArticleCatalog articleCatalog)
        {
            this.logger = logger;
            this.articleCatalog = articleCatalog;
            Id = Guid.NewGuid().ToString();
        }

        public void AddArticle(Article article, int quantity = 1)
        {
            // TODO check is article in catalog
            if (orderItems.ContainsKey(article.Id))
            {
                orderItems[article.Id] += quantity;
            }
            else
            {
                orderItems[article.Id] = quantity;
            }
        }

        public double GetTotalPrice()
        {
            var totalValue = orderItems.Select(i => articleCatalog.FindArticeById(i.Key).Price * i.Value).Sum();
            discountValue = CalaculateDiscountValue();
            return totalValue - discountValue;
        }

        private double CalaculateDiscountValue()
        {
            // TODO
            return 0;
        }

        public void PrintOrder()
        {
            // TODO
            try
            {
                logger.LogInformation($"Order deatails for orderId: {Id}");
                foreach (var item in orderItems)
                {
                    var article = articleCatalog.FindArticeById(item.Key);
                    logger.LogInformation($"/t- {article.Name}/t/t | {item.Value}/t/t | {article.Price}");
                }
                if (discountValue > 0)
                {
                    logger.LogInformation($"/t- Discount/t/t | {discountValue}");
                }
                logger.LogInformation($"Total:/t {GetTotalPrice()}");
            }
            catch (Exception ex)
            {
                // TODO
            }
        }

        public void RegisterDiscount(IDiscount discount)
        {
            if (discounts.Any(d => d.Id == discount.Id))
            {
                throw new Exception("Discount allready registered!");
            }
            discounts.Add(discount);
        }

        public void RemoveArticle(string articleId, int quantity = 1)
        {
            if (orderItems.ContainsKey(articleId))
            {
                var newQuantity = orderItems[articleId] - quantity;
                orderItems[articleId] = newQuantity >= 0 ? newQuantity : 0;
            }
            throw new Exception($"Cannot remove article with id: {articleId}, not in current order!");
        }

    }
}