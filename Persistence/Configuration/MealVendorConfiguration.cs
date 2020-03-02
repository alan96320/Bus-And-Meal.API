using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class MealVendorConfiguration : IEntityTypeConfiguration<MealVendor>
  {
    public void Configure(EntityTypeBuilder<MealVendor> builder)
    {
      builder
        .Property(m => m.Code)
        .HasColumnType("varchar(50)");

      builder
        .Property(m => m.Name)
        .HasColumnType("varchar(100)");

      builder
        .Property(m => m.ContactName)
        .HasColumnType("varchar(100)");

      builder
        .Property(m => m.ContactPhone)
        .HasColumnType("varchar(15)");

      builder
        .Property(m => m.ContactEmail)
        .HasColumnType("varchar(100)");
    }
  }
}