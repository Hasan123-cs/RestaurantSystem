using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Models;
using RestaurantSystem.Services;

namespace RestaurantSystem.Pages.Cart
{
    public class IndexModel : PageModel
    {
        public OrderOperation o { get; set; }
        public IndexModel(OrderOperation x)
        {
            o = x;
        }
        [BindProperty]
        public string addNote { get; set; }
        public List<CartItem> CartItems { get; set; } = new();
        public decimal TotalPrice { get; set; } = new();
        public async Task OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            CartItems = await o.LoadCartForCartPage(userId);
            TotalPrice = await o.getTotalPrice(userId);
        }
        public async Task<IActionResult> OnPostRemoveAsync(int cartItemId)
        {
            await o.RemoveFromCart(cartItemId);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostUpdateQuantityAsync(int cartItemId, int quantity)
        {

            await o.UpdateQuantityInCart(cartItemId, quantity);
            return RedirectToPage();


        }
        public async Task<IActionResult> OnPostAddMoreNote(int idCart, string note)
        {
            await o.updateAddNote(note, idCart);

            return RedirectToPage();
        }

    }
}
