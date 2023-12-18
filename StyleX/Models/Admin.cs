using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    [Table("Admin")]
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Password { get; set; } = null!;
    }
}
