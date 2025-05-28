using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

/// <summary>
/// Entity Framework configuration for the <see cref="Sale"/> entity.
/// </summary>
public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.SaleNumber).IsRequired().HasMaxLength(50);
        builder.Property(s => s.Customer).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Branch).IsRequired().HasMaxLength(100);
        builder.Property(s => s.SaleDate).IsRequired();
        builder.Property(s => s.IsCancelled).IsRequired();

        builder.HasMany(s => s.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}