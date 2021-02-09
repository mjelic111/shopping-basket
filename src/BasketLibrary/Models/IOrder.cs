using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public interface IOrder
    {
        string Id { get; }

        void AddArticle(IArticle article, int quantity = 1);

        void RemoveArticle(string articleId, int quantity = 1);

        double GetTotalPrice();

        void RegisterDiscount(IDiscount discount);

        IEnumerable<IArticle> GetAllArticles();

        void PrintOrder();

    }
}