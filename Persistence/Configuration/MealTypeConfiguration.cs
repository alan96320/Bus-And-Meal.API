using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence
{
  public class MealTypeConfiguration : IEntityTypeConfiguration<MealType>
  {
    public void Configure(EntityTypeBuilder<MealType> builder)
    {
      builder.Property(m => m.Code)
      .HasColumnType("varchar")
      .HasMaxLength(50);

      builder.Property(m => m.Name)
      .HasColumnType("varchar")
      .HasMaxLength(100);
    }
  }
}