using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class Product_Type_Converters
    {
        public Product_TypeDTOs EntityToDTOs(Product_type product_)
        {
            return new Product_TypeDTOs()
            {
                product_type_id = product_.Id,
                created_at = product_.created_at,
                updated_at = product_.updated_at,
                image_type_product = product_.image_type_product,
                name_product_type = product_.name_product_type,
            };
        }
    }
}
