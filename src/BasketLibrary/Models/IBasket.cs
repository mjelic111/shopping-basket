using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public interface IBasket
    {
        void AddOrder(IOrder order);

        IOrder GetOrderById(string orderId);

        string RemoveOrder(string orderId);

        IEnumerable<IOrder> GetAllOrders();
    }
}