using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class MealTypeConfiguration : IEntityTypeConfiguration<MealType>
  {
    public void Configure(EntityTypeBuilder<MealType> builder)
    {
      builder.Property(m => m.Code)
      .HasColumnType("varchar(50)");

      builder.Property(m => m.Name)
      .HasColumnType("varchar(100)");

      builder.Property(m => m.MealVendorId)
      .IsRequired(false);

      // builder.HasOne(m => m.mealVendor)
      // .WithOne(v => v.mealType)
      // .HasForeignKey<MealType>(m => m.MealVendorId);
    }
  }
}