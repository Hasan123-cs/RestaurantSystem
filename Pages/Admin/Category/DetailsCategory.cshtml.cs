using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.Category
{
    public class DetailsCategoryModel : PageModel
    {
        public CRUD_CATEGORY c;
        public RestaurantSystem.Models.Category Category { get; set; }
        public DetailsCategoryModel(CRUD_CATEGORY x) { c = x; }
        public async Task OnGet(int id)
        {
            Category = await c.LoadCategoryById(id); 
        }
    }
}
