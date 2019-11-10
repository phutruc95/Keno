using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Keno.Model;


namespace Keno.Repository
{
    public class KenoContext : DbContext
    {
        public KenoContext() : base("KenoContext")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Statistics> Statistics { get; set; }
        public DbSet<SaleCode> SaleCodes { get; set; }
        public DbSet<UserProperty> UserProperties { get; set; }
    }
}
