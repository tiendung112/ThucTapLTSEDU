namespace ThucTapLTSEDU.PayLoads.Requests.Auth
{
    public class Request_DangKyTaiKhoan
    {
        public string? username { get; set; }
        public string? name { get; set; }
        public IFormFile? avatar { get; set; }

        public string? password { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? address { get; set; }
    }
}
