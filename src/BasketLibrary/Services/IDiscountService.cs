using System.Collections.Generic;
using BasketLibrary.Models;

namespace BasketLibrary.Services
{
    public interface IDiscountService
    {
        string Id { get; }
        Response<OrderItemDto> CalculateDiscount(IEnumerable<OrderItemDto> orderItems, bool removeItemsOnDiscount = true);
    }
}