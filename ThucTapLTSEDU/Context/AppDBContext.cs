using Microsoft.EntityFrameworkCore;
using ThucTapLTSEDU.Entities;

namespace ThucTapLTSEDU.Context
{
    public class AppDBContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Decentralization> Decentralizations { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_type> Products_type { get; set; }
        public virtual DbSet<Product_review> Products_review { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Order_detail> Order_Details { get; set; }
        public virtual DbSet<Order_status> Order_Statuses { get; set; }
        public virtual DbSet<Carts> Carts { get; set; }
        public  virtual DbSet<Cart_item> Cart_Items { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<EmailValidate> EmailValidates { get; set; }    
        
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(SourceData.MyConnect());
        }
    }
}
