using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        public int Quantity { get; set; }
        public string ModelUrl { get; set; } = null!; // url model
        public string PosterUrl { get; set; } = null!; // ảnh hiển thị của product;  
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public double Price { get; set; }
        public double Sale { get; set; } // % giảm giá
        public string Color { get; set; } = string.Empty;
        public string ColorTexture { get; set; } = string.Empty;
        public string TextureRotation { get; set; } = string.Empty;
        public string TextureScale { get; set; } = string.Empty;
        public string Size { get; set; } = null!;



        public int OrderID { get; set; }
        public Order Order { get; set; } = null!;
    }
}
