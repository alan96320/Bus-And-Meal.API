using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class BusTimeConfiguration : IEntityTypeConfiguration<BusTime>
  {
    public void Configure(EntityTypeBuilder<BusTime> builder)
    {
      builder.Property(b => b.Code)
      .HasColumnType("varchar")
      .HasMaxLength(5);
    }

  }
}