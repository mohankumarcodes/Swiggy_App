namespace Swiggy_App.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? FoodName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    }
}
