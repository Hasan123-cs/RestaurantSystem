namespace RestaurantSystem.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MenuItemId { get; set; }

        public int Quantity { get; set; }
        public string? Note { get; set; }

        public MenuItem? MenuItem { get; set; }
    }
}
