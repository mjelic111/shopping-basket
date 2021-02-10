using System;
using System.Linq;
using BasketLibrary.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class BasketTests : BaseTest
    {
        [Fact]
        public void CreateNewOrder()
        {

            // arrange
            var basket = new SimpleBasket(basketLogger);
            IArticleCatalog articleCatalog = null;
            var order = new Order(orderLogger, articleCatalog);

            // act
            basket.AddOrder(order);
            var createdOrder = basket.GetAllOrders().First();

            // assert
            createdOrder.Should().NotBeNull();
            createdOrder.Id.Should().NotBeEmpty();
            createdOrder.Id.Should().Be(order.Id);

        }
    }
}
