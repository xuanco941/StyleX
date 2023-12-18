using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    [Table("Size")]
    public class Size
    {
        [Key]
        public int SizeID { get; set; }
    }
}
