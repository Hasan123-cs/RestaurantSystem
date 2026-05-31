using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models.ViewModel
{
    public class CategoryAddViewModel
    {
        [Required]
        [Display(Name="Please Enter Category Name")]
        public string Name { get; set; }
        // i dont use List of Menu Item
        public List<MenuItem>? MenuItems { get; set; }
        [Display(Name = "Please Enter Category Photo")]

        public string? BackgroundImage { get; set; }
    }
}
