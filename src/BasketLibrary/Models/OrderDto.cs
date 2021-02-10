using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public class OrderDto
    {
        public string Id { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}