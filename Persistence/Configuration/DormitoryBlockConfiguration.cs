using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class DormitoryBlockConfiguration : IEntityTypeConfiguration<DormitoryBlock>
  {
    public void Configure(EntityTypeBuilder<DormitoryBlock> builder)
    {
      builder
         .HasMany<BusOrder>(d => d.BusOrders)
         .WithOne(bo => bo.DormitoryBlock)
         .HasForeignKey(bo => bo.DormitoryBlockId)
         .OnDelete(DeleteBehavior.Restrict);

      builder
         .HasMany<BusOrderVerificationDetail>(d => d.BusOrderVerificationDetails)
         .WithOne(bovd => bovd.DormitoryBlock)
         .HasForeignKey(bovd => bovd.DormitoryBlockId)
         .OnDelete(DeleteBehavior.Restrict);
    }
  }
}