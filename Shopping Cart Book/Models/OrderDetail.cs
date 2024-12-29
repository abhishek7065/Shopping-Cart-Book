﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopping_Cart_Book.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]

        public int BookId { get; set; }
        [Required]

        public double UnitPrice { get; set; }
        [Required]
        public double Quantity { get; set; }
        public Order Order { get; set; }
        public Book Book { get; set; }


    }
}
