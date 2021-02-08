using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public interface IDiscount
    {
        void ApplyDiscount(IEnumerable<IArticle> articles);
    }
}