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
        .HasIndex(d => d.Code);

      builder
        .HasIndex(d => d.Name);

      builder
         .HasMany<BusOrder>(d => d.BusOrders)
         .WithOne(bo => bo.DormitoryBlock)
         .HasForeignKey(bo => bo.DormitoryBlockId)
         .OnDelete(DeleteBehavior.Restrict);
    }
  }
}