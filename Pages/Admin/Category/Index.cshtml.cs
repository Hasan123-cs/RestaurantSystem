using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.Category
{
    public class IndexModel : PageModel
    {
        public CRUD_CATEGORY c;
        public List<RestaurantSystem.Models.Category> Categories { get; set; }
        public IndexModel(CRUD_CATEGORY c)
        {
            this.c = c;
        }
        public async Task OnGet()
        {
            Categories = await c.LoadAllCategory();
        }
    }
}
