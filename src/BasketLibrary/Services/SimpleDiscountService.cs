using System;
using System.Collections.Generic;
using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public class SimpleDiscountService : IDiscountService
    {
        public string Id { get; }
        private readonly decimal discountValue;

        public SimpleDiscountService(decimal discountValue = 2)
        {
            this.discountValue = discountValue;
            Id = Guid.NewGuid().ToString();
        }
        public Response<ArticleDto> CalculateDiscount(IEnumerable<OrderItemDto> orderItems, bool removeItemsOnDiscount = true)
        {
            return Response<ArticleDto>.Success(new ArticleDto
            {
                Id = Id,
                Name = "Simple discount",
                Price = discountValue
            });
        }
    }
}