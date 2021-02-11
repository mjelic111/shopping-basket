using BasketLibrary.Models;
using BasketLibrary.Services;

namespace BasketLibrary.Repositories
{
    public interface IOrderRepository
    {
        string Add(OrderDto order);

        OrderDto GetOrderById(string id);

        string AddDiscountToOrder(string orderId, IDiscountService discountService);
    }
}