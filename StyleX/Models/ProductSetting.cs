using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StyleX.Models
{
    //thiết lặp khi upload product
    [Table("ProductSetting")]
    public class ProductSetting
    {
        [Key]
        public int ProductSettingID { get; set; }
        //tên của bộ phận trên product
        public string ProductPartNameDefault { get; set; } = string.Empty;
        //tên hiển thị của bộ phận trên product
        public string ProductPartNameCustom { get; set; } = string.Empty;
        public string Uuid { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = false; //mặc định sẽ đặt về màu vải trắng , false sẽ giữ màu từ model
        public int ProductID { get; set; }
        public Product Product { get; set; } = null!;

    }
}
