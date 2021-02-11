using System;
using System.Linq;
using BasketLibrary.Models;
using FluentAssertions;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class OrderTests : BaseTest
    {
        [Fact]
        public void CreateNewOrder_ShouldCreate()
        {
            // arrange

            // act
            var orderId = orderService.CreateNewOrder();
            // assert
            orderId.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void AddArticleToOrder_QuantityOutOfScopeError()
        {
            // arrange
            var quantity = 9239;
            // act
            var response = orderService.AddArticleToOrder(GenerateGuid(), GenerateGuid(), quantity);
            // assert
            response.IsError.Should().Be(true);
            response.ErrorMessage.Should().Be("Quantity out of range!");
        }

        [Fact]
        public void AddArticleToOrder_ArticleNotFoundError()
        {
            // arrange
            var articleId = GenerateGuid();
            // act
            var response = orderService.AddArticleToOrder(GenerateGuid(), articleId);
            // assert
            response.IsError.Should().Be(true);
            response.ErrorMessage.Should().Be($"Article with id: {articleId} not found!");
        }

        [Fact]
        public void AddArticleToOrder_OrderNotFoundError()
        {
            // arrange
            var articleId = InitArticlesCatalog();
            var orderId = GenerateGuid();
            // act
            var response = orderService.AddArticleToOrder(orderId, articleId);
            // assert
            response.IsError.Should().Be(true);
            response.ErrorMessage.Should().Be($"Order with id: {orderId} not found!");
        }

        [Fact]
        public void AddArticleToOrder_SuccessAddedNewArticle()
        {
            // arrange
            var articleId = InitArticlesCatalog();
            var name = articleCatalogService.FindArticeById(articleId).Data.Name;
            var orderId = orderService.CreateNewOrder();
            var quantity = 10;
            // act
            var response = orderService.AddArticleToOrder(orderId, articleId, quantity);
            // assert
            response.IsError.Should().Be(false);
            response.Data.Should().Be($"Article {name} added to order, new quantity: {quantity}!");
        }

        [Fact]
        public void AddArticleToOrder_SuccessUpdatedArticleQuantity()
        {
            // arrange
            var articleId = InitArticlesCatalog();
            var name = articleCatalogService.FindArticeById(articleId).Data.Name;
            var orderId = orderService.CreateNewOrder();
            var quantity = 10;

            orderService.AddArticleToOrder(orderId, articleId, quantity);
            // act
            var response = orderService.AddArticleToOrder(orderId, articleId, quantity);

            // assert
            response.IsError.Should().Be(false);
            response.Data.Should().Be($"Article {name} added to order, new quantity: {quantity * 2}!");
        }

        [Fact]
        public void UpdateOrderItemQuantity_SuccessRemovedItem()
        {
            // arrange
            var articleId = InitArticlesCatalog();
            var name = articleCatalogService.FindArticeById(articleId).Data.Name;
            var orderId = orderService.CreateNewOrder();
            var quantity = 0;
            orderService.AddArticleToOrder(orderId, articleId, 10);

            // act
            var response = orderService.UpdateOrderItemQuantity(orderId, articleId, quantity);

            // assert
            response.IsError.Should().Be(false);
            response.Data.Should().Be($"Article {name} removed to order!");
        }

        [Fact]
        public void UpdateOrderItemQuantity_SuccessUpdateQuantity()
        {
            // arrange
            var articleId = InitArticlesCatalog();
            var name = articleCatalogService.FindArticeById(articleId).Data.Name;
            var orderId = orderService.CreateNewOrder();
            var quantity = 15;
            orderService.AddArticleToOrder(orderId, articleId, 10);

            // act
            var response = orderService.UpdateOrderItemQuantity(orderId, articleId, quantity);

            // assert
            response.IsError.Should().Be(false);
            response.Data.Should().Be($"Article {name} added to order, new quantity: {quantity}!");
        }

        [Fact]
        public void GetAllOrderItems_Success()
        {
            // arrange
            var article1 = new ArticleDto { Id = GenerateGuid(), Name = "Butter", Price = 0.8 };
            var article2 = new ArticleDto { Id = GenerateGuid(), Name = "Milk", Price = 1.15 };
            var article3 = new ArticleDto { Id = GenerateGuid(), Name = "Bread", Price = 1.0 };
            articleCatalogService.RegisterArticle(article1);
            articleCatalogService.RegisterArticle(article2);
            articleCatalogService.RegisterArticle(article3);
            var orderId = orderService.CreateNewOrder();
            orderService.AddArticleToOrder(orderId, article1.Id, 10);
            orderService.AddArticleToOrder(orderId, article2.Id, 10);
            orderService.AddArticleToOrder(orderId, article3.Id, 10);

            // act
            var response = orderService.GetAllOrderItems(orderId);

            // assert
            response.IsError.Should().Be(false);
            response.Data.Count().Should().Be(3);

        }

        [Fact]
        public void GetAllOrderItems_SuccessRemoveItem()
        {
            // arrange
            var article1 = new ArticleDto { Id = GenerateGuid(), Name = "Butter", Price = 0.8 };
            var article2 = new ArticleDto { Id = GenerateGuid(), Name = "Milk", Price = 1.15 };
            var article3 = new ArticleDto { Id = GenerateGuid(), Name = "Bread", Price = 1.0 };
            articleCatalogService.RegisterArticle(article1);
            articleCatalogService.RegisterArticle(article2);
            articleCatalogService.RegisterArticle(article3);
            var orderId = orderService.CreateNewOrder();
            orderService.AddArticleToOrder(orderId, article1.Id, 10);
            orderService.AddArticleToOrder(orderId, article2.Id, 10);
            orderService.AddArticleToOrder(orderId, article3.Id, 10);

            // act
            orderService.UpdateOrderItemQuantity(orderId, article1.Id, 0);
            orderService.UpdateOrderItemQuantity(orderId, article2.Id, 0);
            var response = orderService.GetAllOrderItems(orderId);

            // assert
            response.IsError.Should().Be(false);
            response.Data.Count().Should().Be(1);
            response.Data.First().Article.Name.Should().Be("Bread");
            response.Data.First().Article.Price.Should().Be(1.0);

        }

        [Fact]
        public void GetOrderTotalPrice_SumWithoutDiscount()
        {
            // arrange
            articleCatalogService.RegisterArticle(butterArticle);
            articleCatalogService.RegisterArticle(milkArticle);
            articleCatalogService.RegisterArticle(breadArticle);

            var orderId = orderService.CreateNewOrder();
            orderService.AddArticleToOrder(orderId, butterArticle.Id, 3);
            orderService.AddArticleToOrder(orderId, milkArticle.Id, 2);
            orderService.AddArticleToOrder(orderId, breadArticle.Id, 5);
            // act
            var price = orderService.GetOrderTotalPrice(orderId);
            // assert
            var expectedPrice = butterArticle.Price * 3 + milkArticle.Price * 2 + breadArticle.Price * 5;
            price.Should().Be(expectedPrice);
        }
    }
}