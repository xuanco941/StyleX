using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    [Table("DecalInfo")]
    public class DecalInfo
    {
        [Key]
        public int DecalInfoID { get; set; }
        public string ImageUrl { get; set; } = null!;
        public int CartItemID { get; set; }
        public CartItem CartItem { get; set; } = null!;
    }
}
