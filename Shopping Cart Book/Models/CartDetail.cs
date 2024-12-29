using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Cart_Book.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId {  get; set; } 
        public int BookId {  get; set; }
        [Required]
        public int Quantity {  get; set; }
        [Required]  
        public double UnitPrice { get; set; }
        public ShoppingCart ShoppingCart { get; set; } // Navigation to ShoppingCart
        public Book Book { get; set; } // Navigation to Book

    }

}
