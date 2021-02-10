using BasketLibrary.Models;

namespace BasketLibrary.Models
{
    public interface IArticleCatalog
    {
        void RegisterArticle(Article article);
        void DeregisterArticle(Article article);
        void DeregisterArticleByName(string name);
        Article FindArticeByName(string name);
        Article FindArticeById(string id);
        void ClearCatalog();
    }
}