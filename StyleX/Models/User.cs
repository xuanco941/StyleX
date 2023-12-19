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
        public string Email { get; set; } = string.Empty;
        [StringLength(50)]
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string? PhoneNumber { get; set; } = string.Empty;
        [StringLength(300)]
        public string? Address { get; set; } = string.Empty;
        public bool isAuthen = false;
        public string keyActive = new Guid().ToString();

    }
}