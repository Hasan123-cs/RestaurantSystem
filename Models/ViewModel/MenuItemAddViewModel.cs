using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models.ViewModel
{
    public class MenuItemAddViewModel
    {
        [Display(Name= "Item Name")]
        public string Name { get; set; }
        [Display(Name = "Description")]

        public string Description { get; set; }
        [Display(Name = "Price")]

        public decimal Price { get; set; }
        [Display(Name = "Upload Image\r\n")]
        public string? ImagePath { get; set; }
       


        public int CategoryId { get; set; }

    }
}
