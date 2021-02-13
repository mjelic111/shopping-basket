using System;
using System.Collections.Generic;
using System.Linq;
using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public class BreadDiscountService : IDiscountService
    {
        public string Id { get; }
        private readonly IArticleCatalogService articleCatalog;

        public BreadDiscountService(IArticleCatalogService articleCatalog)
        {
            Id = Guid.NewGuid().ToString();
            this.articleCatalog = articleCatalog;
        }
        public Response<OrderItemDto> CalculateDiscount(IEnumerable<OrderItemDto> orderItems, bool removeItemsOnDiscount = true)
        {
            // buy 2 butters and get one bread at 50% off

            // find articles
            var response = articleCatalog.FindArticeByName("butter");
            if (response.IsError)
            {
                // cannot find butter
                return Response<OrderItemDto>.Error("Butter not found!");
            }
            var butterArticle = response.Data;
            response = articleCatalog.FindArticeByName("bread");
            if (response.IsError)
            {
                // cannot find bread
                return Response<OrderItemDto>.Error("Bread not found!");
            }
            var breadArticle = response.Data;
            // count
            var butterCount = orderItems.Where(i => i.Article.Id.Equals(butterArticle.Id, StringComparison.OrdinalIgnoreCase)).Select(e => e.Quantity).SingleOrDefault();
            var breadCount = orderItems.Where(i => i.Article.Id.Equals(breadArticle.Id, StringComparison.OrdinalIgnoreCase)).Select(e => e.Quantity).SingleOrDefault();
            var discountQuantity = Math.Min(butterCount / 2, breadCount);
            // var discount = discountQuantity * (breadArticle.Price / 2);
            if (removeItemsOnDiscount)
            {
                orderItems.Where(i => i.Article.Id.Equals(butterArticle.Id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault().Quantity -= discountQuantity * 2;
                orderItems.Where(i => i.Article.Id.Equals(breadArticle.Id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault().Quantity -= discountQuantity;
            }
            return Response<OrderItemDto>.Success(new OrderItemDto
            {
                Id = Id,
                Article = new ArticleDto
                {
                    Id = Id,
                    Name = "Bread 50% off",
                    Price = breadArticle.Price / 2
                },
                Quantity = discountQuantity
            });
        }
    }
}