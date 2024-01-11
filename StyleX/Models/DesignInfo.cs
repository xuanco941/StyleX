using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    //chỉ số design của từng bộ phận trên product cart item
    [Table("DesignInfo")]
    public class DesignInfo
    {
        [Key]
        public int DesignInfoID { get; set; }
        public string DesignName { get; set; } = string.Empty; // tên của bộ phận trên quần áo
        public string? Color { get; set; }
        public string? ImageTexture { get; set; } //color image url
        public double? TextureRotation { get; set; } 
        public double? TextureScale { get; set; }
        public string? ImageMaterial { get; set; } // image url
        public int CartItemID { get; set; }
        public CartItem CartItem { get; set; } = null!;
    }
}
