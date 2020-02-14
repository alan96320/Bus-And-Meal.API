using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence
{
  public class BusOrderVerificationHeaderConfiguration : IEntityTypeConfiguration<BusOrderVerificationHeader>
  {
    public void Configure(EntityTypeBuilder<BusOrderVerificationHeader> builder)

    {
      builder.Property(b => b.OrderNo)
      .HasColumnType("varchar")
      .HasMaxLength(10);
    }
  }
}