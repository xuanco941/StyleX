using StyleX.Models;

namespace StyleX.DTOs
{
    public class SearchProductModel
    {
        public int categoryID { get; set; } = 0;
        public int status { get; set; } = 0;
    }
    public class ProductSettingsWithMaterial
    {
        public int ProductSettingID { get; set; }
        public string ProductPartNameDefault { get; set; } = string.Empty;
        public string ProductPartNameCustom { get; set; } = string.Empty;
        public bool IsDefault { get; set; } = false;
        public List<Material> materials { get; set; } = null!;

    }
    public class AddProductModel
    {
        public string name { get; set; } = null!;
        public IFormFile fileModel { get; set; } = null!;
        public IFormFile file { get; set; } = null!;
		public IFormFile? img1 { get; set; }
		public IFormFile? img2 { get; set; }

		public string description { get; set; } = string.Empty;
        public double price { get; set; }
        public double sale { get; set; }
        public DateTime saleEndAt { get; set; }
        public bool status { get; set; }
        public int categoryID { get; set; }
        public List<string> productParts { get; set;} = null!;
    }
    public class UpdateProductModel
    {
        public int productID { get; set; }
        public string name { get; set; } = null!;
        public IFormFile? file { get; set; }
		public IFormFile? img1 { get; set; } 
		public IFormFile? img2 { get; set; } 
		public string description { get; set; } = string.Empty;
        public double price { get; set; }
        public double sale { get; set; }
        public DateTime saleEndAt { get; set; }
        public bool status { get; set; }
        public int categoryID { get; set; }
    }
    public class SettingProductModel
    {
        public int productSettingID { get; set; }

        public string productPartNameCustom { get; set; } = string.Empty;
        public bool isDefault { get; set; }

        public List<int> materials { get; set; } = null!;
    }
}
