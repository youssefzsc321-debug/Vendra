using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Core.Data.Config
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(o => o.ProductItemOrder, ProductItemOrder => ProductItemOrder.WithOwner());

            builder.Property(o => o.Price).HasColumnType("decimal(18,2)");
        }
    }
}
