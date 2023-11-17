using ThucTapLTSEDU.Context;
using ThucTapLTSEDU.Entities;
using ThucTapLTSEDU.PayLoads.DTOs;

namespace ThucTapLTSEDU.PayLoads.Converters
{
    public class Account_Converters
    {
        private readonly AppDBContext context;
        private readonly User_Converters user_Converters;
        public Account_Converters()
        {
            context = new AppDBContext();
            user_Converters = new User_Converters();
        }
        public AccountDTOs EntityToDTOs (Account account)
        {
            return new AccountDTOs()
            {
                user_name = account.user_name,
                avatar = account.avatar,
                password = account.password,
                status = account.status,
                users =context.Users.Where(x=>x.accountID==account.Id).Select(y=>user_Converters.EntityToDTOs(y)),
            };
        }
    }
}
