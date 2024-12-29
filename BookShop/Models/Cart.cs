using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Humanizer;

namespace BookShop.Models
{
    [Table("Cart")]
    public class Cart
    {

        [Key]
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        public int Price { get; set; }

        [NotMapped]
        [Display(Name = "Book Name")]
        public string? BookName { get; set; }

        [NotMapped]
        [Display(Name = "Book")]
        public string? ImgUrl { get; set; }

        [NotMapped]
        public int Total { get; set; }

    }
}
