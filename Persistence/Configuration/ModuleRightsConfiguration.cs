using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class ModuleRightsConfiguration : IEntityTypeConfiguration<ModuleRights>
  {
    public void Configure(EntityTypeBuilder<ModuleRights> builder)
    {
      builder.Property(m => m.Code)
      .HasColumnType("varchar(10)");

      builder.Property(m => m.Description)
      .HasColumnType("varchar(100)");

    }
  }
}