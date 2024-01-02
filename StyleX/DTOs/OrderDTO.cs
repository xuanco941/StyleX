namespace StyleX.DTOs
{
    public class SearhOrderModel
    {
        public string orderID { get; set; } = string.Empty;
        public string accountName { get; set;} = string.Empty;

    }
    public class UpdateOrderModel
    {
        public int orderID { get; set; }
        public double transportFee { get; set; }
        public double netPrice { get; set; } //số tiền phải trả

        public string name { get; set; } = null!;
        public string address { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string message { get; set; } = null!;
        public int status { get; set; }  //0.đang xử lý, 1.đang giao hàng, 2.giao hàng thành công, 3.hủy. nếu nhận được yêu cầu sửa thì quay lại 0
    }
}
