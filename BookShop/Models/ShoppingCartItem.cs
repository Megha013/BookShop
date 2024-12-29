using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    [Table("Cart")]
    public class ShoppingCartItem
    {
        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        [NotMapped]
        public Book? Book { get; set; }

    }
}
