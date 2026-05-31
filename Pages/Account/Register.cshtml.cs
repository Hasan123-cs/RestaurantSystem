using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantSystem.Models.UserViewBinding;
using RestaurantSystem.Services;
using Microsoft.AspNetCore.Identity;
namespace RestaurantSystem.Pages.Account
{
    public class RegisterModel : PageModel
    {
        public UserOperation operation { get; set; }
        public RegisterModel(UserOperation operation)
        {
            this.operation = operation;
        }
        [BindProperty]
        public UserViewBinding Input { get; set; } = new UserViewBinding();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid || (Input.Password != Input.ConfirmPassword))
            {
                if(Input.Password != Input.ConfirmPassword)
                {
                    ModelState.AddModelError("Input.ConfirmPassword", "Different Password !! Incorrect");
                    return Page();
                }
                else if(!ModelState.IsValid)
                {
                    return Page();

                }
                //
            }
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

            string hashedPassword =
                passwordHasher.HashPassword(null, Input.Password);

            Input.Password = hashedPassword;

            await operation.RegisterUserAccount(Input);
            return RedirectToPage("/Account/Login");
        }
    }
}
