using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class MealOrderVerificationConfiguration : IEntityTypeConfiguration<MealOrderVerification>
  {
    public void Configure(EntityTypeBuilder<MealOrderVerification> builder)
    {
      builder
        .Property(m => m.OrderNo)
        .HasColumnType("varchar(10)");

      builder
        .HasMany<MealOrder>(mov => mov.MealOrders)   //mov = meal order verification
        .WithOne(mo => mo.MealOrderVerification)    // mo = meal order
        .HasForeignKey(mo => mo.MealOrderVerificationId)
        .OnDelete(DeleteBehavior.SetNull);

      builder
        .HasMany(mov => mov.MealOrderVerificationDetails)    //mov = meal order verification
        .WithOne(movd => movd.MealOrderVerification)        // movd = meal order verifcation detail
        .HasForeignKey(movd => movd.MealOrderVerificationId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}