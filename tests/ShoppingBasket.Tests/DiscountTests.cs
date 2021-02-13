using System;
using BasketLibrary.Services;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class DiscountTests : BaseTest
    {
        [Fact]
        public void RegisterDiscount_ShouldBeSuccessfull()
        {
            // arrange
            var orderId = orderService.CreateNewOrder();
            var simpleDiscount = new SimpleDiscountService();
            // act
            var response = orderService.RegisterDiscount(orderId, simpleDiscount);
            // assert
            response.IsError.Should().Be(false);
            response.Data.Should().Be($"Discount with id: {simpleDiscount.Id} added to order.");
        }

        [Fact]
        public void RegisterDiscount_DuplicateRegistrationError()
        {
            // arrange
            var orderId = orderService.CreateNewOrder();
            var simpleDiscount = new SimpleDiscountService();
            // act
            var response = orderService.RegisterDiscount(orderId, simpleDiscount);
            var response2 = orderService.RegisterDiscount(orderId, simpleDiscount);
            // assert
            response.IsError.Should().Be(false);
            response.Data.Should().Be($"Discount with id: {simpleDiscount.Id} added to order.");
            response2.IsError.Should().Be(true);
            response2.ErrorMessage.Should().Be($"Discount with id: {simpleDiscount.Id} allready registered!");
        }

        [Fact]
        public void RegisterDiscount_NotFoundOrderError()
        {
            // arrange
            var orderId = GenerateGuid();
            var simpleDiscount = new SimpleDiscountService();
            // act
            var response = orderService.RegisterDiscount(orderId, simpleDiscount);
            // assert
            response.IsError.Should().Be(true);
            response.ErrorMessage.Should().Be($"Order with id: {orderId} not found!");
        }

        [Fact]
        public void GetOrderTotalPrice_SimpleDiscount()
        {
            // arrange
            articleCatalogService.RegisterArticle(butterArticle);
            articleCatalogService.RegisterArticle(milkArticle);
            articleCatalogService.RegisterArticle(breadArticle);

            var orderId = orderService.CreateNewOrder();
            var discountValue = 1;
            orderService.RegisterDiscount(orderId, new SimpleDiscountService(discountValue));
            orderService.AddArticleToOrder(orderId, butterArticle.Id, 3);
            orderService.AddArticleToOrder(orderId, milkArticle.Id, 2);
            orderService.AddArticleToOrder(orderId, breadArticle.Id, 5);
            // act
            var price = orderService.GetOrderTotalPrice(orderId);
            // assert
            var expectedPrice = butterArticle.Price * 3 + milkArticle.Price * 2 + breadArticle.Price * 5;
            expectedPrice -= discountValue;
            price.Should().Be(expectedPrice);
        }

        [Fact]
        public void GetOrderTotalPrice_BigSimpleDiscount()
        {
            // arrange
            articleCatalogService.RegisterArticle(butterArticle);
            articleCatalogService.RegisterArticle(milkArticle);
            articleCatalogService.RegisterArticle(breadArticle);

            var orderId = orderService.CreateNewOrder();
            var discountValue = 100;
            orderService.RegisterDiscount(orderId, new SimpleDiscountService(discountValue));
            orderService.AddArticleToOrder(orderId, butterArticle.Id, 3);
            orderService.AddArticleToOrder(orderId, milkArticle.Id, 2);
            orderService.AddArticleToOrder(orderId, breadArticle.Id, 5);
            // act
            var price = orderService.GetOrderTotalPrice(orderId);
            // assert
            price.Should().Be(0);
        }

        [Fact]
        public void GetOrderTotalPrice_NoDiscount()
        {
            // arrange
            articleCatalogService.RegisterArticle(butterArticle);
            articleCatalogService.RegisterArticle(milkArticle);
            articleCatalogService.RegisterArticle(breadArticle);

            var orderId = orderService.CreateNewOrder();
            orderService.RegisterDiscount(orderId, new MilkDiscountService(articleCatalogService));
            orderService.RegisterDiscount(orderId, new BreadDiscountService(articleCatalogService));
            orderService.AddArticleToOrder(orderId, breadArticle.Id, 1);
            orderService.AddArticleToOrder(orderId, butterArticle.Id, 1);
            orderService.AddArticleToOrder(orderId, milkArticle.Id, 1);
            // act
            var price = orderService.GetOrderTotalPrice(orderId);
            // assert
            price.Should().Be(2.95M);
        }

        [Fact]
        public void GetOrderTotalPrice_BreadDiscount()
        {
            // arrange
            articleCatalogService.RegisterArticle(butterArticle);
            articleCatalogService.RegisterArticle(milkArticle);
            articleCatalogService.RegisterArticle(breadArticle);

            var orderId = orderService.CreateNewOrder();
            orderService.RegisterDiscount(orderId, new BreadDiscountService(articleCatalogService));
            orderService.AddArticleToOrder(orderId, butterArticle.Id, 2);
            orderService.AddArticleToOrder(orderId, breadArticle.Id, 2);
            // act
            var price = orderService.GetOrderTotalPrice(orderId);
            // assert
            price.Should().Be(3.1M);
        }

        [Fact]
        public void GetOrderTotalPrice_MilkDiscount()
        {
            // arrange
            articleCatalogService.RegisterArticle(butterArticle);
            articleCatalogService.RegisterArticle(milkArticle);
            articleCatalogService.RegisterArticle(breadArticle);

            var orderId = orderService.CreateNewOrder();
            orderService.RegisterDiscount(orderId, new MilkDiscountService(articleCatalogService));
            orderService.AddArticleToOrder(orderId, milkArticle.Id, 4);
            // act
            var price = orderService.GetOrderTotalPrice(orderId);
            // assert
            price.Should().Be(3.45M);
        }

        [Fact]
        public void GetOrderTotalPrice_BreadAndMilkDiscount()
        {
            // arrange
            articleCatalogService.RegisterArticle(butterArticle);
            articleCatalogService.RegisterArticle(milkArticle);
            articleCatalogService.RegisterArticle(breadArticle);

            var orderId = orderService.CreateNewOrder();
            orderService.RegisterDiscount(orderId, new MilkDiscountService(articleCatalogService));
            orderService.RegisterDiscount(orderId, new BreadDiscountService(articleCatalogService));
            orderService.AddArticleToOrder(orderId, milkArticle.Id, 8);
            orderService.AddArticleToOrder(orderId, butterArticle.Id, 2);
            orderService.AddArticleToOrder(orderId, breadArticle.Id, 1);
            // act
            var price = orderService.GetOrderTotalPrice(orderId);
            // assert
            price.Should().Be(9);
        }

        [Fact]
        public void GetOrderTotalPrice_LoggerShouldBeCalled()
        {

            // arrange
            articleCatalogService.RegisterArticle(butterArticle);
            articleCatalogService.RegisterArticle(milkArticle);
            articleCatalogService.RegisterArticle(breadArticle);
            // orderService
            var loggerMock = new Mock<ILogger<OrderService>>();
            var moqOrderService = new OrderService(loggerMock.Object, articleCatalogService, orderRepository);
            var orderId = orderService.CreateNewOrder();
            moqOrderService.RegisterDiscount(orderId, new MilkDiscountService(articleCatalogService));
            moqOrderService.RegisterDiscount(orderId, new BreadDiscountService(articleCatalogService));
            moqOrderService.AddArticleToOrder(orderId, milkArticle.Id, 8);
            moqOrderService.AddArticleToOrder(orderId, butterArticle.Id, 2);
            moqOrderService.AddArticleToOrder(orderId, breadArticle.Id, 1);
            // act
            var price = moqOrderService.GetOrderTotalPrice(orderId);
            // assert
            loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
            Times.Once);

        }
    }

}