using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wood.Domain.Entities;

namespace Wood.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(300).IsRequired();
            builder.Property(p => p.Species).HasMaxLength(100);
            builder.Property(p => p.Grade).HasMaxLength(50);
            builder.Property(p => p.Dimensions).HasMaxLength(100);
            builder.Property(p => p.Unit).HasMaxLength(20);
            builder.Property(p => p.PricePerCubicMeter).HasPrecision(18, 2);
            builder.Property(p => p.PricePerPiece).HasPrecision(18, 2);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(200).IsRequired();
            builder.Property(c => c.Slug).HasMaxLength(100);
            builder.Property(c => c.Icon).HasMaxLength(10);
        }
    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.CustomerName).HasMaxLength(200).IsRequired();
            builder.Property(o => o.Phone).HasMaxLength(20).IsRequired();
            builder.Property(o => o.Email).HasMaxLength(200);
            builder.Property(o => o.Address).HasMaxLength(500);
            builder.Property(o => o.TotalAmount).HasPrecision(18, 2);

            builder.HasMany(o => o.Items)
                   .WithOne(i => i.Order)
                   .HasForeignKey(i => i.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.ProductName).HasMaxLength(300);
            builder.Property(i => i.Unit).HasMaxLength(20);
            builder.Property(i => i.Quantity).HasPrecision(18, 3);
            builder.Property(i => i.UnitPrice).HasPrecision(18, 2);
            builder.Ignore(i => i.TotalPrice); // computed property
        }
    }
}
