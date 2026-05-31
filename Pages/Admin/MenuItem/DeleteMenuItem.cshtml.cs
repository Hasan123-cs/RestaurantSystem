using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models;
using RestaurantSystem.Services.Admin;

namespace RestaurantSystem.Pages.Admin.MenuItem
{
    public class DeleteMenuItemModel : PageModel
    {
        public CRUD_MENU m;
        [BindProperty]
        public RestaurantSystem.Models.MenuItem MenuItem { get; set; }
        public DeleteMenuItemModel(CRUD_MENU m)
        {
            this.m = m;
        }
        public async Task OnGet(int id)
        {
            MenuItem = await m.LoadItemById(id);
        }
        public async Task<IActionResult> OnPostAsync()
        {
           

            await m.RemoveMenuItem(MenuItem.Id);
            return RedirectToPage("../MenuItem/Index");
        }
    }
}
