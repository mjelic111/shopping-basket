using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public class SimpleBasket : IBasket
    {
        public string CompleteOrder(string orderId)
        {
            throw new System.NotImplementedException();
        }

        public IOrder CreateOrder()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IOrder> GetAllOrders()
        {
            throw new System.NotImplementedException();
        }

        public IOrder GetOrderById(string orderId)
        {
            throw new System.NotImplementedException();
        }

        public string RemoveOrder(string orderId)
        {
            throw new System.NotImplementedException();
        }
    }
}