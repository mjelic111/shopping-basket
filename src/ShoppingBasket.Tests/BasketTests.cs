using System;
using System.Linq;
using BasketLibrary.Models;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class BasketTests
    {
        [Fact]
        public void CreateNewOrder()
        {

            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddConsole();
            });

            ILogger logger = loggerFactory.CreateLogger<BasketTests>();

            // arrange
            var basket = new SimpleBasket(logger);
            var order = new Order();

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
