using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models;
using RestaurantSystem.Services;

namespace RestaurantSystem.Pages.Category
{
    public class IndexModel : PageModel
    {
        public CategoryOperation menu { get; set; }
        public List<MenuItem> Menus { get; set; } = new List<MenuItem>();
        public RestaurantSystem.Models.Category cat { get; set; } = new RestaurantSystem.Models.Category();
        public IndexModel(CategoryOperation m)
        {
            menu = m;
        }
        public async Task OnGet(int CategoryID)
        {
            cat = await menu.GetCategoryWithItems(CategoryID);
            if(cat is null)
            {
                return ;
            }
            Menus = cat.MenuItems;

        }
        public async Task<IActionResult> OnPostMakeOrder(int id)
        {
            Console.WriteLine($"{id}");
            return RedirectToPage("../Order/Index", new { nid = id });
        }
    }
}
