namespace ThucTapLTSEDU.PayLoads.Requests.Products
{
    public class Request_SuaProduct
    {
        public int product_typeID { get; set; }
        public string? name_product { get; set; }
        public double? price { get; set; }
        public IFormFile? avartar_image_product { get; set; }
        public string? title { get; set; }
        public int discount { get; set; }
        public int status { get; set; }
    }
}
