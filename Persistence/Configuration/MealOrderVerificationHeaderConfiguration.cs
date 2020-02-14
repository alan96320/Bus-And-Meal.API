using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence
{
  public class MealOrderVerificationHeaderConfiguration : IEntityTypeConfiguration<MealOrderVerificationHeader>
  {
    public void Configure(EntityTypeBuilder<MealOrderVerificationHeader> builder)
    {
      builder.Property(m => m.OrderNo)
      .HasColumnType("varchar")
      .HasMaxLength(10);
    }
  }
}