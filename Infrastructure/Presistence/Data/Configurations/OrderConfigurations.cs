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
    public class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(O => O.ShippingAddress, a => a.WithOwner());
            builder.HasMany(builder => builder.OrderItems)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade); // Ensures that when an Order is deleted, its OrderItems are also deleted
            builder.HasOne(o => o.DeliveryMethod)
                   .WithMany()
                   .OnDelete(DeleteBehavior.SetNull); // Prevents deletion of DeliveryMethod if it is referenced by an Order
            builder.Property(o => o.PaymentStatus)
                   .HasConversion(v => v.ToString(), v => Enum.Parse<OrderPaymentStatus>(v));
                   


        }
    }
}
