using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StyleX.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [StringLength(50)]
        public string? FullName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        [MinLength(5)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        [MinLength(4)]
        public string Password { get; set; } = string.Empty;
        [StringLength(20)]
        public string? PhoneNumber { get; set; } = string.Empty;
        [StringLength(300)]
        public string? Address { get; set; } = string.Empty;
        public bool isActive { get; set; } = false;
        public string keyActive { get; set; } = string.Empty;

    }
}