using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public interface IArticleCatalogService
    {
        Response<string> RegisterArticle(ArticleDto article);
        Response<ArticleDto> FindArticeByName(string name);
        Response<ArticleDto> FindArticeById(string id);
        Response<string> ClearCatalog();
    }
}