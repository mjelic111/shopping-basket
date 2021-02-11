using System.Collections.Generic;
using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public interface IOrderService
    {
        string CreateNewOrder();
        Response<string> AddArticleToOrder(string orderId, string articleId, int quantity = 1);
        Response<IEnumerable<OrderItemDto>> GetAllOrderItems(string orderId);
        Response<string> UpdateOrderItemQuantity(string orderId, string articleId, int quantity);
        Response<string> RegisterDiscount(string orderId, IDiscountService discountService);
        decimal GetOrderTotalPrice(string orderId);
    }
}