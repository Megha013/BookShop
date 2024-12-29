using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]

        [Display(Name = "Phone Number")]
        public string? Contact { get; set; }

        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public int RoleId { get; set; }

        [Display(Name = "Confirm Password")]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Password can't be match !")]
        public string? ConfirmPassword { get; set; }

        //[NotMapped]
        //[Display(Name = "Role")]
        //public string? RoleName { get; set; }

    }
}


