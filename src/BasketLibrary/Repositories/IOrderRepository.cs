using BasketLibrary.Models;

namespace BasketLibrary.Repositories
{
    public interface IOrderRepository
    {
        string Add(OrderDto order);

        OrderDto GetOrderById(string id);
    }
}