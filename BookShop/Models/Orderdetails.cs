using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    [Table("Orders")]
    public class Orderdetails
    {
        [Key]
        public int OrderId { get; set; }
        public int BookId { get; set; }
        //public string BookName { get; set; }   
        public int CartId { get; set; }
        public int UserId { get; set; }
        //public string UserName { get; set; }
        public Book? Book { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalPrice { get; set; }
        [NotMapped]
        public string? BookName { get; set; }
        [NotMapped]
        public string? UserName { get; set; }
    }
}
