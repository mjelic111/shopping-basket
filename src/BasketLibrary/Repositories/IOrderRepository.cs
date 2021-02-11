using System.Collections.Generic;
using BasketLibrary.Models;
using BasketLibrary.Services;

namespace BasketLibrary.Repositories
{
    public interface IOrderRepository
    {
        string Add(OrderDto order);
        OrderDto GetOrderById(string id);
        IEnumerable<OrderItemDto> GetAllOrderItems(string orderId);
        IEnumerable<IDiscountService> GetAllOrderDiscounts(string orderId);
        string AddDiscountToOrder(string orderId, IDiscountService discountService);
    }
}