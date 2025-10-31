using Domain.Entities.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(o => o.Price).HasColumnType("decimal(18,4)");
            builder.OwnsOne(o => o.Product, p => p.WithOwner());
        }
    }
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, sh => sh.WithOwner());
            builder.HasMany(o => o.OrderItems).WithOne();
            builder.Property(p => p.PaymentStatus)
                .HasConversion(ps => ps.ToString(), ps => Enum.Parse<OrderPaymentStatus>(ps));

            builder.HasOne(o => o.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(o => o.SubTotal).HasColumnType("decimal(18,4)");
        }
    }
}
