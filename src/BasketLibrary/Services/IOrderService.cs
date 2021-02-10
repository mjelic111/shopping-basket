using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public interface IOrderService
    {
        string CreateNewOrder();
        void AddArticleToOrder(string orderId, string articleId, int quantity = 1);
        void SetArticleQuantity(string orderId, string articleId, int quantity);
        void RemoveArticleFromOrder(string orderId, string articleId);
        void RegisterDiscount(IDiscountService discountService);
        double GetOrderTotalPrice(string orderId);
        void PrintOrder();
    }
}