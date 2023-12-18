using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    [Table("Material")]
    public class Material
    {
        [Key]
        public int MaterialID { get; set; }
        public string Name { get; set; } = null!;
        public string Url { get; set; } = null!; //link ảnh tạo vật liệu
    }
}
