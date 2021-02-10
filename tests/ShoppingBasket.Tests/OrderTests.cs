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
    }
}