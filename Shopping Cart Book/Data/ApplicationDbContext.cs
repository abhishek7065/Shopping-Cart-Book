using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shopping_Cart_Book.Models;

namespace Shopping_Cart_Book.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }    
        public DbSet<ShoppingCart> ShoppingCarts { get; set;}
        public DbSet<CartDetail> CartDetail { get; set; }   
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> ordersDetail { get; set; }    
        public DbSet<OrderStatus> orderStatus { get; set; } 
    }
}
