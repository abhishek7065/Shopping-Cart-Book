using System.ComponentModel.DataAnnotations;

namespace Shopping_Cart_Book.Models.DTOs
{
    public class CheckoutModel
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Email { get; set; }
        [Required]
        public string? MobileNumber { get; set; }
        [Required]
        [MaxLength(200)]
        public string? Address { get; set; }
        [Required]
        [MaxLength(30)]
        public string? PaymentMethod { get; set; }
    }
}
