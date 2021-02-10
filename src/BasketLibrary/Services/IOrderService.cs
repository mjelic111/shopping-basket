using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public interface IOrderService
    {
        string CreateNewOrder();
        Response<string> AddArticleToOrder(string orderId, string articleId, int quantity = 1);
        Response<string> SetArticleQuantity(string orderId, string articleId, int quantity);
        Response<string> RemoveArticleFromOrder(string orderId, string articleId);
        Response<string> RegisterDiscount(IDiscountService discountService);
        double GetOrderTotalPrice(string orderId);
        void PrintOrder();
    }
}