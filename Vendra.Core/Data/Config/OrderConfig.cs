using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendra.Core.Enities;
using Vendra.Core.Enities.Order_Aggragtte;

namespace Vendra.Core.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.Address, Address => Address.WithOwner());

            builder.Property(o => o.Status)
                .HasConversion(ConvertToString => ConvertToString.ToString(),
                ConvertToEnum=> (OrderStatus)Enum.Parse(typeof(OrderStatus), ConvertToEnum));

            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.Property(o => o.SubTotal)
          .HasPrecision(18, 2);
        }
    }
}
