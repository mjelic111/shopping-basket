using System;
using BasketLibrary.Models;
using BasketLibrary.Repositories;
using BasketLibrary.Services;
using Microsoft.Extensions.Logging;

namespace ShoppingBasket.Tests
{
    public abstract class BaseTest
    {
        protected IArticleCatalogService articleCatalogService;
        protected OrderRepository orderRepository;
        protected OrderService orderService;
        protected readonly ArticleDto butterArticle;
        protected readonly ArticleDto milkArticle;
        protected readonly ArticleDto breadArticle;

        public BaseTest()
        {
            // this should be provided with DI
            // loggers
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddConsole();
            });

            var orderServiceLogger = loggerFactory.CreateLogger<OrderService>();

            // article catalog service
            articleCatalogService = new ArticleCatalogService();
            // order repository
            orderRepository = new OrderRepository();
            // order service
            orderService = new OrderService(orderServiceLogger, articleCatalogService, orderRepository);
            // articles
            butterArticle = new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Butter", Price = 0.8M };
            milkArticle = new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Milk", Price = 1.15M };
            breadArticle = new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Bread", Price = 1.0M };
        }

        protected string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
        protected string InitArticlesCatalog()
        {
            articleCatalogService.RegisterArticle(butterArticle);
            articleCatalogService.RegisterArticle(milkArticle);
            articleCatalogService.RegisterArticle(breadArticle);
            return milkArticle.Id;
        }
    }
}