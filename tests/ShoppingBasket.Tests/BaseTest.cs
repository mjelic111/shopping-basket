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
            butterArticle = new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Butter", Price = new decimal(0.8) };
            milkArticle = new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Milk", Price = new decimal(1.15) };
            breadArticle = new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Bread", Price = new decimal(1.0) };
        }

        protected string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
        protected string InitArticlesCatalog()
        {
            var id = Guid.NewGuid().ToString();
            articleCatalogService.RegisterArticle(new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Butter", Price = new decimal(0.8) });
            articleCatalogService.RegisterArticle(new ArticleDto { Id = id, Name = "Milk", Price = new decimal(1.15) });
            articleCatalogService.RegisterArticle(new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Bread", Price = new decimal(1.0) });
            return id;
        }
    }
}