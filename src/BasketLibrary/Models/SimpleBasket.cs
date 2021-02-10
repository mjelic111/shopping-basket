using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace BasketLibrary.Models
{
    public class SimpleBasket : IBasket
    {
        List<IOrder> orders = new List<IOrder>();
        public ILogger<SimpleBasket> Logger { get; }

        public SimpleBasket(ILogger<SimpleBasket> logger)
        {
            Logger = logger;
        }

        public void AddOrder(IOrder order)
        {
            Logger.LogInformation("Creating new order...");
            orders.Add(order);
            Logger.LogInformation($"Created order with id: {order.Id}");
        }

        public IEnumerable<IOrder> GetAllOrders()
        {
            return orders.ToList();
        }

        public IOrder GetOrderById(string orderId)
        {
            var order = orders.Where(o => o.Id.Equals(orderId, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (order != null)
            {
                return order;
            }

            throw new Exception($"Order with id: {orderId}, not found");
        }

        public string RemoveOrder(string orderId)
        {
            var order = GetOrderById(orderId);
            if (order != null)
            {
                orders = orders.Where(o => !o.Id.Equals(orderId, StringComparison.OrdinalIgnoreCase)).ToList();
                return orderId;
            }

            throw new Exception($"Order with id: {orderId}, not found");
        }
    }
}