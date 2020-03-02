using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class ModuleRightsConfiguration : IEntityTypeConfiguration<ModuleRight>
  {
    public void Configure(EntityTypeBuilder<ModuleRight> builder)
    {
      builder
        .HasMany<UserModuleRight>(mr => mr.UserModuleRights)
        .WithOne(umr => umr.ModuleRights)
        .HasForeignKey(umr => umr.ModuleRightsId)
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}