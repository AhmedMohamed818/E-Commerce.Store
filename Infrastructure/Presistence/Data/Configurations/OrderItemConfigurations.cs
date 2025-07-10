using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Data.Configurations
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(O => O.Product, p => p.WithOwner());
            builder.Property(o => o.Price)
                   .HasColumnType("decimal(18,2)"); // Ensures that the Price is stored as a decimal with two decimal places
            
        }
    }
}
