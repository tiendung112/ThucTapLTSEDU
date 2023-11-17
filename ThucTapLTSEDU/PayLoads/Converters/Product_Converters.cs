using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class Product_Converters
    {
        public ProductDTOs EntityToDTOs(Product product)
        {
            return new ProductDTOs()
            {
                Product_ID = product.Id,
                avartar_image_product = product.avartar_image_product,
                created_at=product.created_at,
                updated_at=product.updated_at,
                discount=product.discount,
                name_product=product.name_product,
                number_of_views=product.number_of_views,
                price=product.price,
                product_typeID=product.product_typeID,
                status=product.status,
                title=product.title
            };
        }
    }
}
