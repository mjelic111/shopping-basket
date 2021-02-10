using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public interface IDiscount
    {
        string Id { get; }
        void ApplyDiscount(IEnumerable<Article> articles);
    }
}