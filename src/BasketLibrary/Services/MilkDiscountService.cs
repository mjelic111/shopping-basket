using System;
using System.Collections.Generic;
using System.Linq;
using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public class MilkDiscountService : IDiscountService
    {
        private readonly IArticleCatalogService articleCatalog;

        public string Id { get; }
        public MilkDiscountService(IArticleCatalogService articleCatalog)
        {
            this.articleCatalog = articleCatalog;
        }
        public Response<ArticleDto> CalculateDiscount(IEnumerable<OrderItemDto> orderItems, bool removeItemsOnDiscount = true)
        {
            // buy 3 milks and get 4th for free

            // find articles
            var response = articleCatalog.FindArticeByName("milk");
            if (response.IsError)
            {
                // cannot find butter
                return Response<ArticleDto>.Error("Milk not found!");
            }
            var milkArticle = response.Data;

            // count
            var milkCount = orderItems.Where(i => i.Article.Id.Equals(milkArticle.Id, StringComparison.OrdinalIgnoreCase)).Select(e => e.Quantity).SingleOrDefault();
            var discount = milkCount / 4 * (milkArticle.Price);
            return Response<ArticleDto>.Success(new ArticleDto { Id = Id, Name = "Forth milk gratis", Price = discount });
        }
    }
}