using System.Collections.Generic;

namespace BasketLibrary.Models
{
    public class BasketDto
    {
        public string Name { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}