using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class BusTimeConfiguration : IEntityTypeConfiguration<BusTime>
  {
    public void Configure(EntityTypeBuilder<BusTime> builder)
    {
      builder
        .HasMany<BusOrderDetail>(bt => bt.BusOrderDetails)  // bt = bus time
        .WithOne(bod => bod.BusTime)                        // bod = bus order detail
        .HasForeignKey(bod => bod.BusTimeId)
        .OnDelete(DeleteBehavior.Restrict);

      builder
        .HasMany<BusOrderVerificationDetail>(bt => bt.BusOrderVerificationDetails)  // bt = bus time
        .WithOne(bovd => bovd.BusTime)        // bovd = bus order verification detail
        .HasForeignKey(bovd => bovd.BusTimeId)
        .OnDelete(DeleteBehavior.Restrict);
    }

  }
}