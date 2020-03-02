using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class AppConfigConfiguration : IEntityTypeConfiguration<AppConfiguration>
  {
    public void Configure(EntityTypeBuilder<AppConfiguration> builder)
    {
      builder.Property(a => a.LockedBusOrder)
      .HasColumnType("varchar(10)");

      builder.Property(a => a.LockedMealOrder)
      .HasColumnType("varchar(10)");
    }
  }
}
