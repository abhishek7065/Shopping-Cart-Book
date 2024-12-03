using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shopping_Cart_Book.Models
{
    [Table("Genre")]

    public class Genre
    {
            public int Id { get; set; }
            [Required]
            [MaxLength(50)]
            public string GenreName { get; set; }

        // we're using List<Book> in Genre because genre can have many books 1 to many relationship
        public List<Book> Books { get; set; }

        
    }
}
