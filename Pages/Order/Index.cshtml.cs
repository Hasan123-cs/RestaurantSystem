using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using RestaurantSystem.Models;
using RestaurantSystem.Services;

namespace RestaurantSystem.Pages.Order
{
    public class IndexModel : PageModel
    {
        public OrderOperation Operation { get; set; }
       
        [BindProperty]
        public int Quantity { get; set; }
        public MenuItem Item { get; set; } = new();
        public MenuItemService service { get; set; }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public int CategoryIDs { get; set; }
        public IndexModel( MenuItemService service, OrderOperation operation)
        {
            this.Operation = operation;
            this.service = service;
        }

        public async Task<IActionResult> OnGetAsync(int nid)
        {
            Item = await service.LoadItemToOrder(nid);
            

            if (Item == null)
            {
                return NotFound("error"); // أو NotFound()
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAdd()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToPage("../Account/Login", new {idItem = Id});
            bool t = await Operation.AddToCart(Quantity, Id, userId);
            if(!t)
            {
                return NotFound($"{Id}");
            }
            return RedirectToPage("/Category/Index", new { CategoryID = CategoryIDs });
        }
    }
}
