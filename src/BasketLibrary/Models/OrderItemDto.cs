namespace BasketLibrary.Models
{
    public class OrderItemDto
    {
        public string Id { get; set; }
        public ArticleDto Article { get; set; }
        public int Quantity { get; set; }

    }
}