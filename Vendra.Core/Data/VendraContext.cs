using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Core.Data
{
    public class VendraContext : DbContext
    {

        public VendraContext(DbContextOptions<VendraContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
                     t => t.Namespace != null && t.Namespace.StartsWith("Vendra.Core.Data.Config"));
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<ProductBrand> productBrands { get; set; }

        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set;}
        
        



    }
}

