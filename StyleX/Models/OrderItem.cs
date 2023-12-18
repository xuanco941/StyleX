using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }
        public int Amount { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; } = null!;
    }
}
