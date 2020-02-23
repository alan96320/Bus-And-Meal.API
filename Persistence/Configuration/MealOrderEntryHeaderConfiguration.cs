using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class MealOrderEntryHeaderConfiguration : IEntityTypeConfiguration<MealOrderEntryHeader>
  {
    public void Configure(EntityTypeBuilder<MealOrderEntryHeader> builder)
    {
      builder
      .HasMany(m => m.MealOrderDetail)
      .WithOne(m => m.MealOrderEntryHeader)
      .HasForeignKey(m => m.MealOrderEntryHeaderId)
      .OnDelete(DeleteBehavior.Cascade);
    }
  }
}