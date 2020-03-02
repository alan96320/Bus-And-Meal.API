using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class UserModuleRightConfiguration : IEntityTypeConfiguration<UserModuleRight>
  {
    public void Configure(EntityTypeBuilder<UserModuleRight> builder)
    {
      builder.HasOne<User>(u => u.User)
      .WithMany(u => u.UserModuleRights)
      .HasForeignKey(u => u.UserId)
      .OnDelete(DeleteBehavior.Cascade);

      builder.HasOne<ModuleRight>(u => u.ModuleRights)
      .WithMany(u => u.UserModuleRights)
      .HasForeignKey(u => u.ModuleRightsId)
      .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
