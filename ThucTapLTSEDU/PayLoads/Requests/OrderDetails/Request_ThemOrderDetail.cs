using ThucTapLTSEDU.Entities;

namespace ThucTapLTSEDU.PayLoads.Requests.OrderDetails
{
    public class Request_ThemOrderDetail
    { 
        public int? productID { get; set; }
        public int? quantity { get; set; }

    }
}
