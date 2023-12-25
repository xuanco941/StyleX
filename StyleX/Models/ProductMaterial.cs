using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    [Table("ProductMaterial")]
    public class ProductMaterial
    {
        [Key]
        public int ProductMaterialID { get; set; }
        [Required]
        public string ProductMaterialName { get; set; } = null!; //vị trí trên product
        public string Uuid { get; set; } = null!;
        public bool IsDefault { get; set; } = false; // = true thì dẽ đổi màu về màu vải chưa trắng (#fff, textures=null)
        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;
        public int MaterialID { get; set; }
        public Material Material { get; set; } = null!;
    }
}
