using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{

    // phiếu khuyến mãi cho đơn hàng
    [Table("Promotion")]
    public class Promotion
    {
        [Key]
        public int PromotionID { get; set; }
        public string? Name { get; set; }
        public double Number { get; set; } //% phần trăm giảm giá
        public bool Status { get; set; }
        public int? OrderID { get; set; }

        public int UserID { get; set; }
        public User User { get; set; } = null!;


    }
}
