namespace StyleX.DTOs
{
    public class SaveDesignInfoModel
    {
        public int designInfoIDID { get; set; } // nếu = 0 thì tạo mới, khác 0 thì cập nhật
        public int cartItemID { get; set; } 
        public int productSettingID { get; set; }
        public int DesignInfoID { get; set; }
        public string DesignName { get; set; } = string.Empty; // tên của bộ phận trên quần áo
        public string? Color { get; set; }
        public string? ImageTexture { get; set; } //color image url
        public double? TextureRotation { get; set; }
        public double? TextureScale { get; set; }
        public string? ImageMaterial { get; set; } // image url
    }
}
