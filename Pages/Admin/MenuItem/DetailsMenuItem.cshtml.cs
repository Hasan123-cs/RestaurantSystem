using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.MenuItem
{
    public class DetailsMenuItemModel : PageModel
    {
        public CRUD_MENU c;
        public CRUD_CATEGORY x;
        public RestaurantSystem.Models.MenuItem MenuItem { get; set; }
        public string CategoryName { get; set; }
        public DetailsMenuItemModel(CRUD_MENU c,CRUD_CATEGORY x) { this.c = c;this.x = x; }
        public async Task OnGet(int id)
        {
            MenuItem = await c.LoadItemById(id);
            CategoryName = await x.GetCategoryName(MenuItem.CategoryId);
        }
    }
}
