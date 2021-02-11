using System;
using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public class SimpleDiscountService : IDiscountService
    {
        public string Id { get; }

        public SimpleDiscountService()
        {
            Id = Guid.NewGuid().ToString();
        }
        public Response<ArticleDto> CalculateDiscount()
        {
            return Response<ArticleDto>.Success(new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Simple discount", Price = 2.0 });
        }
    }
}