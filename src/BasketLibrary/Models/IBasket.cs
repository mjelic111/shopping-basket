using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public interface IBasket
    {
        IOrder CreateOrder();

        IOrder GetOrderById(string orderId);

        string CompleteOrder(string orderId);

        string RemoveOrder(string orderId);

        IEnumerable<IOrder> GetAllOrders();
    }
}