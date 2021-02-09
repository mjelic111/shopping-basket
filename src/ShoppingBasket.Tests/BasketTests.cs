using System;
using BasketLibrary.Models;
using FluentAssertions;
using Xunit;

namespace ShoppingBasket.Tests
{
    public class BasketTests
    {
        [Fact]
        public void CreateNewOrder()
        {
            // arrange
            var basket = new SimpleBasket();

            // act
            var order = basket.CreateOrder();

            // assert
            order.Should().NotBeNull();
            order.Id.Should().NotBeEmpty();

        }
    }
}
