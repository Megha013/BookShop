using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> op) : base(op)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category>Categories { get; set; }
        public DbSet<Orderdetails> OrderDetails { get; set; }
        public DbSet<ShoppingCartItem> Carts { get; set; }
       
    }
}
