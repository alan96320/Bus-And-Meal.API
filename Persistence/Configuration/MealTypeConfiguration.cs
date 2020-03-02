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

      builder.HasOne<MealVendor>(mt => mt.MealVendor)     // mt = meal type
      .WithOne(v => v.MealType)                           // v = vendor
      .HasForeignKey<MealType>(mt => mt.MealVendorId);

      builder
      .HasMany<MealOrderDetail>(mt => mt.MealOrderDetails)   // mt = meal type
      .WithOne(mod => mod.MealType)                         // mod = meal order detail
      .HasForeignKey(mod => mod.MealOrderId)
      .OnDelete(DeleteBehavior.Restrict);

      builder
      .HasMany<MealOrderVerificationDetail>(mt => mt.MealOrderVerificationDetails)  // mt = meal type
      .WithOne(movd => movd.MealType)           // movd = meal order verification detail
      .HasForeignKey(movd => movd.MealOrderVerificationId)
      .OnDelete(DeleteBehavior.Restrict);


    }
  }
}