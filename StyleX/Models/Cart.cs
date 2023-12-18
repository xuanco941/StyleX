using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StyleX.Models
{
    //chứa các product được thêm vào giỏ hoặc đã design
    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public string Color { get; set; } = string.Empty;
        public string ColorTextures { get; set; } = string.Empty;

        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;
        public int UserID { get; set; }
        public User User { get; set; } = null!;
    }
}