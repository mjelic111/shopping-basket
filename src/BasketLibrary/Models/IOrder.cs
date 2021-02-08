using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public interface IOrder
    {
        string Id { get; internal set; }

        void AddArticle(IArticle article);

        void RemoveArticle(string articleId, int quantity = 1);

        double GetTotalPrice();

        void RegisterDiscount(IDiscount discount);

        IEnumerable<IArticle> ListArticles();

        void PrintOrder();

    }
}