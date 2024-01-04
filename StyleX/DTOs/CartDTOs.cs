namespace StyleX.DTOs
{
    public class AddCModel
    {
        public string name { get; set; } = null!;
        public IFormFile fileModel { get; set; } = null!;
        public IFormFile file { get; set; } = null!;
        public string description { get; set; } = string.Empty;
        public double price { get; set; }
        public double sale { get; set; }
        public DateTime saleEndAt { get; set; }
        public bool status { get; set; }
        public int categoryID { get; set; }
        public List<string> productParts { get; set; } = null!;
    }
}
