using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{

    [Table("Orders")]
    public class Odrders
    {
        [Key]
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        [NotMapped]
        public string? BookName { get; set; }
        [NotMapped]
        public string? UserName { get; set; }

    }
}
