using ThucTapLTSEDU.Context;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class User_Converters
    {
        private readonly AppDBContext context;
        public User_Converters()
        {
            context =new AppDBContext() ;
        }
        public UserDTOs EntityToDTOs (User user)
        {
            return new UserDTOs() {
                user_name = user.user_name,
                address = user.address,
                email = user.email,
                phone = user.phone,
                accountID = user.accountID,
            };
        }
    }
}
