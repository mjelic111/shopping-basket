using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketLibrary.Models
{
    public class Order : IOrder
    {
        public string Id { get; }
        private List<IDiscount> discounts = new List<IDiscount>();

        public Order()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void AddArticle(IArticle article, int quantity = 1)
        {
            throw new System.NotImplementedException();
        }

        public double GetTotalPrice()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IArticle> GetAllArticles()
        {
            throw new System.NotImplementedException();
        }

        public void PrintOrder()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterDiscount(IDiscount discount)
        {
            discounts.Add(discount);
        }

        public void RemoveArticle(string articleId, int quantity = 1)
        {
            throw new System.NotImplementedException();
        }

    }
}