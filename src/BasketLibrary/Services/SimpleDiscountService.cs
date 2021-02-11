using System;
using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public class SimpleDiscountService : IDiscountService
    {
        public string Id { get; }
        private readonly double discountValue;

        public SimpleDiscountService(double discountValue = 2.0)
        {
            this.discountValue = discountValue;
            Id = Guid.NewGuid().ToString();
        }
        public Response<ArticleDto> CalculateDiscount()
        {
            return Response<ArticleDto>.Success(new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Simple discount", Price = discountValue });
        }
    }
}