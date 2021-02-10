using System;
using System.Collections.Generic;
using System.Linq;

namespace BasketLibrary.Models
{
    public class ArticleCatalog : IArticleCatalog
    {
        private List<Article> articles = new List<Article>();

        public void RegisterArticle(Article article)
        {
            if (FindArticeById(article.Id) == null || FindArticeByName(article.Name) == null)
            {
                throw new Exception("Article allerady exists!");
            }
            articles.Add(article);
        }
        public void DeregisterArticle(Article article)
        {
            throw new System.NotImplementedException();
        }

        public void DeregisterArticleByName(string name)
        {
            throw new System.NotImplementedException();
        }

        public Article FindArticeByName(string name)
        {
            var article = articles.Where(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (article != null)
            {
                return article;
            }
            throw new Exception($"Article with name: {name}, not found!");
        }

        public Article FindArticeById(string id)
        {
            var article = articles.Where(a => a.Id.Equals(id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (article != null)
            {
                return article;
            }
            throw new Exception($"Article with id: {id}, not found!");
        }

        public void ClearCatalog()
        {
            articles = new List<Article>();
        }
    }
}