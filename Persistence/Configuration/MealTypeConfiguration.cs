using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class MealTypeConfiguration : IEntityTypeConfiguration<MealType>
  {
    public void Configure(EntityTypeBuilder<MealType> builder)
    {
      builder
        .HasIndex(mt => mt.Name);

      builder
        .HasIndex(mt => mt.Code);

      builder
        .HasMany<MealOrderDetail>(mt => mt.MealOrderDetails)   // mt = meal type
        .WithOne(mod => mod.MealType)                         // mod = meal order detail
        .HasForeignKey(mod => mod.MealTypeId)
        .OnDelete(DeleteBehavior.Restrict);

      builder
        .HasMany<MealOrderVerificationDetail>(mt => mt.MealOrderVerificationDetails)  // mt = meal type
        .WithOne(movd => movd.MealType)           // movd = meal order verification detail
        .HasForeignKey(movd => movd.MealTypeId)
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}