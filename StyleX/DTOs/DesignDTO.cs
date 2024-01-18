namespace StyleX.DTOs
{
    public class SaveDesignInfoModel
    {
        public int designInfoID { get; set; } // nếu = 0 thì tạo mới, khác 0 thì cập nhật
        public int cartItemID { get; set; }
        public string designName { get; set; } = string.Empty; // tên của bộ phận trên quần áo
        public string? color { get; set; }
        public string? imageTexture { get; set; } //color image url
        public double? textureScale { get; set; }
        public string nameMaterial { get; set; } = string.Empty;
        public string aoMap { get; set; } = string.Empty;//mô phỏng cách ánh sáng tương tác với môi trường xung quanh
        public string normalMap { get; set; } = string.Empty; //sử dụng để tạo ra chi tiết đồ họa
        public string roughnessMap { get; set; } = string.Empty;// xác định độ nhám
        public string metalnessMap { get; set; } = string.Empty;// xác định vị trí của các vùng kim loại trong vật liệu
    }
}
