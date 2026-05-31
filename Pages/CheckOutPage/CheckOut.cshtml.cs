using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models;
using RestaurantSystem.Models.ViewModel;
using RestaurantSystem.Services;

namespace RestaurantSystem.Pages.CheckOutPage
{
    public class CheckOutModel : PageModel
    {
        public OrderOperation o { get; set; }
        public List<CartItem> CartItems { get; set; }
        [BindProperty]
        public UserCardInfo info { get; set; }
        public decimal Total { get; set; }

        public CheckOutModel(OrderOperation x)
        {
            this.o = x;
        }
        public async Task OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            CartItems = await  o.GetAllItemFromCard(userId);
            Total = await o.GetTotalOrderPayByUserID(userId);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            // virtual check for the visa !! 
            int? userId =
   HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToPage("/Login");
            }
            CartItems = await o.GetAllItemFromCard(userId);
            Total = await o.GetTotalOrderPayByUserID(userId);


            if (!ModelState.IsValid)
            {
                return Page();
            }
           

            await o.MakeTheAcceptForOrder(userId.Value);
            // this will be update to success page want 
            TempData["Success"] = "Payment completed successfully ✔";


            return RedirectToPage("/Home/Index");
        }
    }
}
