using ThucTapLTSEDU.PayLoads.DTOs;
using ThucTapLTSEDU.PayLoads.Requests.Cart_Item;

namespace ThucTapLTSEDU.PayLoads.Requests.Cart
{
    public class Request_ThemCart
    {
        public IQueryable<Request_ThemCart_Items>? cart_Items { get; set; }
    }
}
