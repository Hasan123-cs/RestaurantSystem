using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.MenuItem
{
    public class IndexModel : PageModel
    {
        public CRUD_MENU c;
        public CRUD_CATEGORY cat;
        public List<RestaurantSystem.Models.MenuItem> MenuItems { get; set; }
        public List<RestaurantSystem.Models.Category> cate { get; set; }
        public IndexModel(CRUD_MENU c,CRUD_CATEGORY cat)
        {   
            this.c = c;
            this.cat = cat;
        }
        public async Task OnGet()
        {
            MenuItems = await c.LoadAllMenuItem();
            cate = await cat.LoadAllCategory();
        }
    }
}
