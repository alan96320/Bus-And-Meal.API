using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class MealOrderDetailConfiguration : IEntityTypeConfiguration<MealOrderDetail>
  {
    public void Configure(EntityTypeBuilder<MealOrderDetail> builder)
    {
      //   builder
      //   .WithMany(m => m.MealOrderDetail)
      //   .HasForeignKey(m => m.MealOrderEntryHeaderId)
      //   .WillCascadeOnDelete();
    }
  }
}