using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace Shopping_Cart_Book.Models
{
    [Table("Book")]
    public class Book
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]  
        public string? BookName { get; set; }
        [Required]
        public int GenreId { get; set; }
        [Required]
        public double Price { get; set; }
        public string ? AuthorName { get;set; }
        public string? Image {  get; set; } 
        //many to one 
        public Genre Genre { get; set; }
        public List<OrderDetail> OrderDetail { get; set; } = new List<OrderDetail>();
        public List<CartDetail> CartDetail { get; set; } = new List<CartDetail>();
        [NotMapped]
        public string GenreName { get; set; }   



    }
}
