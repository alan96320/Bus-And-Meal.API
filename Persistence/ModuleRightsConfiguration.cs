using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence
{
  public class ModuleRightsConfiguration : IEntityTypeConfiguration<ModuleRights>
  {
    public void Configure(EntityTypeBuilder<ModuleRights> builder)
    {
      builder.Property(m => m.Code)
      .HasColumnType("varchar")
      .HasMaxLength(10);

      builder.Property(m => m.Description)
      .HasColumnType("varchar")
      .HasMaxLength(100);

    }
  }
}