using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StyleX.Models
{
    //chứa các product được thêm vào giỏ hoặc lưu khi design

    [Table("Cart")]
    public class Cart
    {
        [Key]
        public int CartID { get; set; }
        public string Color { get; set; } = string.Empty;
        public string ImageTexture { get; set; } = string.Empty; //image
        public string TextureRotation { get; set; } = string.Empty;
        public string TextureScale { get; set; } = string.Empty;
        public int? MaterialID { get; set; } = null;

        public int Quantity { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.Now;


        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;
        public int UserID { get; set; }
        public User User { get; set; } = null!;

    }
}