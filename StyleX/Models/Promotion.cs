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
        public int Number { get; set; } //% phần trăm giảm giá 4 loại 5%,10%,15%,20%
        public bool Status { get; set; } = false; // false là chưa dùng
        public int? OrderID { get; set; }
        public string ResultSpin { get; set; } = null!;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public DateTime? UsedAt { get; set; }
        public DateTime? ExpiredAt { get; set; }


        public int AccountID { get; set; }
        public Account Account { get; set; } = null!;


    }
}
