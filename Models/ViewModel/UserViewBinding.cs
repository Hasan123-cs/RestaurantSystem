using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models.UserViewBinding
{
    public class UserViewBinding
    {
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Password { get; set; }

        public string? Role { get; set; }
    }
}
