using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int Status { get; set; }
        public int UserID { get; set; }
        public User User { get; set; } = null!;
        public int? PromotionID { get; set; }
        public Promotion? Promotion { get; set; }
    }
}
