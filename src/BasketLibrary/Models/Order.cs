using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public class Order : IOrder
    {
        public Order(string id)
        {
            Id = id;
        }
        public string Id { get; }

        public void AddArticle(IArticle article)
        {
            throw new System.NotImplementedException();
        }

        public double GetTotalPrice()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IArticle> ListArticles()
        {
            throw new System.NotImplementedException();
        }

        public void PrintOrder()
        {
            throw new System.NotImplementedException();
        }

        public void RegisterDiscount(IDiscount discount)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveArticle(string articleId, int quantity = 1)
        {
            throw new System.NotImplementedException();
        }

        void IOrder.AddArticle(IArticle article)
        {
            throw new System.NotImplementedException();
        }

        double IOrder.GetTotalPrice()
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<IArticle> IOrder.ListArticles()
        {
            throw new System.NotImplementedException();
        }

        void IOrder.PrintOrder()
        {
            throw new System.NotImplementedException();
        }

        void IOrder.RegisterDiscount(IDiscount discount)
        {
            throw new System.NotImplementedException();
        }

        void IOrder.RemoveArticle(string articleId, int quantity)
        {
            throw new System.NotImplementedException();
        }
    }
}