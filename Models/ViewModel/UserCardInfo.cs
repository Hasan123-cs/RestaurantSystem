using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Models.ViewModel
{
    public class UserCardInfo
    {
// we use the view model and binding model as same because the project is small
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"^(\d{4}\s?){4}$", ErrorMessage = "Visa Number format is invalid")]
        public string VisaNumber { get; set; }
        [Required]
        public string Expiry { get; set; }
        [Required]
        public string CVV { get; set; }

    }
}
