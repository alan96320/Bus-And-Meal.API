using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class BusOrderVerificationConfiguration : IEntityTypeConfiguration<BusOrderVerification>
  {
    public void Configure(EntityTypeBuilder<BusOrderVerification> builder)

    {
      builder
        .HasMany<BusOrderVerificationDetail>(bov => bov.BusOrderVerificationDetails)
        .WithOne(bovd => bovd.BusOrderVerification)
        .HasForeignKey(bovd => bovd.BusOrderVerificationId)
        .OnDelete(DeleteBehavior.Cascade);

      builder
        .HasMany<BusOrder>(bov => bov.BusOrders)
        .WithOne(bo => bo.BusOrderVerification)
        .HasForeignKey(bo => bo.BusOrderVerificationId)
        .OnDelete(DeleteBehavior.SetNull);
    }
  }
}