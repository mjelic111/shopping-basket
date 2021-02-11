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
        }

        protected string GenerateGuid()
        {
            return Guid.NewGuid().ToString();
        }
        protected string InitArticlesCatalog()
        {
            var id = Guid.NewGuid().ToString();
            articleCatalogService.RegisterArticle(new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Butter", Price = 0.8 });
            articleCatalogService.RegisterArticle(new ArticleDto { Id = id, Name = "Milk", Price = 1.15 });
            articleCatalogService.RegisterArticle(new ArticleDto { Id = Guid.NewGuid().ToString(), Name = "Bread", Price = 1.0 });
            return id;
        }
    }
}