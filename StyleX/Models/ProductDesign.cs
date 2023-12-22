using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{

    [Table("ProductDesign")]
    public class ProductDesign
    {
        [Key]
        public int ProductDesignID { get; set; }
        public int Amount { get; set; }
        public string PosterUrl { get; set; } = null!; // ảnh hiển thị của product khi đã design;  
        public string? Size { get; set; } = string.Empty;
        public int Status { get; set; } //0 là sản phẩm trong cart và đang design, 1 là đã vào đơn order

        public int? OrderID { get; set; }
        public Order? Order { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;
        public int AccountID { get; set; }
        public Account Account { get; set; } = null!;
    }
}
