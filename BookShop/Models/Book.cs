using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string? BookName { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public int Price { get; set; }

        public string ImgUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [NotMapped]
        public string? CategoryName
        {
            get; set;

        }
    }
}
