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

    }
}