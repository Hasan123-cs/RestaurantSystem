using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models;
using RestaurantSystem.Services;


namespace RestaurantSystem.Pages.Admin
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;
        public UserOperation Operation { get; set; }
        public LoginModel(UserOperation operation)
        {
            Operation = operation;
        }
        [BindProperty]
        public int id { get; set; }
        public void OnGet(int idItem)
        {
            id = idItem;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var access = await Operation.CheckUserLogin(Email,Password);
            if(access==-1 || access==0 || !ModelState.IsValid)
            {
                if(access==-1)
                {
                    ModelState.AddModelError("Email", " the email Invalid");
                }
                if(access==0)
                {
                    ModelState.AddModelError("Password", "Incorrect Password");
                }
                return Page();
            }
            if(access==1)
            {
                // for an admin redirect to admin page
            return RedirectToPage("../Admin/AdminDashboard");
            }
            HttpContext.Session.SetInt32("UserId", access);
            if (id == 0) { return RedirectToPage("/Home/Index"); }
            return RedirectToPage("../Order/Index", new { nid = id }); // this the client home page 
        }
    }
}
