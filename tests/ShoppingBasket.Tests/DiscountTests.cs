using BasketLibrary.Services;
using FluentAssertions;
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
    }

}