namespace StyleX.DTOs
{
    public class AddMaterialModel
    {
        public string name { get; set; } = null!;
        public IFormFile file { get; set; } = null!;
        public bool status { get; set; }
    }
    public class UpdateMaterialModel
    {
        public int materialID { get; set; }
        public string name { get; set; } = null!;
        public IFormFile file { get; set; } = null!;
        public bool status { get; set; }
    }
}
