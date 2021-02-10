using System;
using System.Collections.Generic;
using System.Linq;
using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public class ArticleCatalogService : IArticleCatalogService
    {
        private List<ArticleDto> articles = new List<ArticleDto>();

        public Response<string> ClearCatalog()
        {
            articles = new List<ArticleDto>();
            return new Response<string> { Data = $"Celared catalog, current items in catalog: {articles.Count}" };
        }

        public Response<string> RegisterArticle(ArticleDto article)
        {
            if (FindArticeById(article.Id).IsError && FindArticeByName(article.Name).IsError)
            {
                articles.Add(article);
                return Response<string>.Success("Article registered!");
            }
            return Response<string>.Error("Article allready registered!");
        }

        public Response<ArticleDto> FindArticeById(string id)
        {
            var article = articles.Where(a => a.Id.Equals(id, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (article != null)
            {
                return Response<ArticleDto>.Success(article);
            }
            return Response<ArticleDto>.Error($"Article with id: {id} not found!");
        }

        public Response<ArticleDto> FindArticeByName(string name)
        {
            var article = articles.Where(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();
            if (article != null)
            {
                return Response<ArticleDto>.Success(article);
            }
            return Response<ArticleDto>.Error($"Article with name: {name} not found!");
        }

    }
}