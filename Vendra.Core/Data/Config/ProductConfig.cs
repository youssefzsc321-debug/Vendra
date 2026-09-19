using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities;

namespace Vendra.Core.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(300);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
            //builder.HasOne(x => x.ProductType).WithMany().HasForeignKey(x => x.ProductTypeId);
            //builder.HasOne(x => x.ProductBrand).WithMany().HasForeignKey(x => x.ProductBrandId);

        }
    }
}
