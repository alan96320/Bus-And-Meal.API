using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class BusOrderEntryHeaderConfiguration : IEntityTypeConfiguration<BusOrderVerificationHeader>
  {
    public void Configure(EntityTypeBuilder<BusOrderVerificationHeader> builder)
    {
      builder
      .HasMany(b => b.BusOrderVerificationDetail)
      .WithOne(b => b.BusOrderVerificationHeader)
      .HasForeignKey(b => b.BusOrderVerificationHeaderId)
      .OnDelete(DeleteBehavior.Cascade);
    }
  }
}