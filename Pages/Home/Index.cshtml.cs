using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models;
using RestaurantSystem.Models.ViewModel;
using RestaurantSystem.Services;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Home
{
    public class IndexModel : PageModel
    {
        public CRUD_CATEGORY x;
        public MenuItemService menuItemService;
        public IndexModel(CRUD_CATEGORY x, MenuItemService menuItemService)
        {
            this.x = x;
            this.menuItemService = menuItemService;
        }
        public List<PopularDishViewModel> popular { get; set; }
        public int? Success { get; set; }
        public List<RestaurantSystem.Models.Category> Categories { get; set; } = new();

        public async Task OnGet(int? SuccessPay)
        {
            Categories = await x.LoadAllCategory();
            Success = SuccessPay;
            popular = await menuItemService.MostPopularLove();
        }
    }
}
