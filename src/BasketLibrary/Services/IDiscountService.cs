using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public interface IDiscountService
    {
        string Id { get; }
        Response<ArticleDto> CalculateDiscount();
    }
}