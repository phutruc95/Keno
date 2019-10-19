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
    }
}
