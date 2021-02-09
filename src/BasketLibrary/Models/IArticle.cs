namespace BasketLibrary.Models
{
    public interface IArticle
    {
        string Id { get; set; }
        string Name { get; set; }
        int Quantity { get; set; }
        double Price { get; set; }
    }
}