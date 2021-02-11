using System.Collections.Generic;
using BasketLibrary.Services;

namespace BasketLibrary.Models
{
    public class OrderDto
    {
        public string Id { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

        public List<IDiscountService> DiscountServices { get; set; }
    }
}