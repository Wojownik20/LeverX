using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Core.Data;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.ProductId).IsRequired();
        builder.Property(c => c.CustomerId).IsRequired();
        builder.Property(c => c.EmployeeId).IsRequired();
        builder.Property(c => c.TotalPrice).IsRequired();
        builder.Property(c => c.PurchaseDate).IsRequired();
    }
}