using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; } //số dư trong kho
        public double Price { get; set; }
        public double Sale { get; set; } // % giảm giá
        public int CategoryID { get; set; }
        public Category Category { get; set; } = null!;
        public int SizeID { get; set; }
        public Size Size { get; set; } = null!;
        public int MaterialID { get; set; }
        public Material Material { get; set; } = null!;


    }
}
