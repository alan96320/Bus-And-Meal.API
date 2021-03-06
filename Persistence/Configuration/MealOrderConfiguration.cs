using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class MealOrderConfiguration : IEntityTypeConfiguration<MealOrder>
  {
    public void Configure(EntityTypeBuilder<MealOrder> builder)
    {
      builder
        .HasIndex(mo => mo.DepartmentId);

      builder
        .HasIndex(e => e.OrderEntryDate);

      builder
        .HasIndex(e => e.MealOrderVerificationId);

      builder
        .HasIndex(e => e.IsReadyToCollect);

      builder
        .HasMany(mo => mo.MealOrderDetails)
        .WithOne(mod => mod.MealOrder)
        .HasForeignKey(mod => mod.MealOrderId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}