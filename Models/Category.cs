namespace RestaurantSystem.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        // i dont use List of Menu Item
        public List<MenuItem?> MenuItems { get; set; }
        public string BackgroundImage { get; set; }
    }
}
