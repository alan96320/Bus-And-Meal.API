using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence
{
  public class MealVendorConfiguration : IEntityTypeConfiguration<MealVendor>
  {
    public void Configure(EntityTypeBuilder<MealVendor> builder)
    {
      builder.Property(m => m.Code)
      .HasColumnType("varchar")
      .HasMaxLength(50);

      builder.Property(m => m.Name)
      .HasColumnType("varchar")
      .HasMaxLength(100);

      builder.Property(m => m.ContactName)
            .HasColumnType("varchar")
            .HasMaxLength(100);

      builder.Property(m => m.ContactPhone)
      .HasColumnType("varchar")
      .HasMaxLength(15);

      builder.Property(m => m.ContactEmail)
            .HasColumnType("varchar")
            .HasMaxLength(100);
    }
  }
}