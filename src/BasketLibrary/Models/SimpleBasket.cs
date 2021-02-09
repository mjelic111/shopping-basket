using System;
using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public class SimpleBasket : IBasket
    {
        List<IOrder> orders = new List<IOrder>();

        public string CompleteOrder(string orderId)
        {
            throw new System.NotImplementedException();
        }

        public IOrder CreateOrder()
        {
            var orderId = Guid.NewGuid().ToString();
            var order = new Order(orderId);
            orders.Add(order);
            return order;
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