using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Cart_Book.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]  
        public string UserId {  get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
