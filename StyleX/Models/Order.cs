using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public double Total { get; set; } //tổng tiền
        public string Address { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Message { get; set; } = null!;
        public int Status { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public int UserID { get; set; }
        public User User { get; set; } = null!;
    }
}
