using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class UserModuleRightConfiguration : IEntityTypeConfiguration<UserModuleRights>
  {
    public void Configure(EntityTypeBuilder<UserModuleRights> builder)
    {
      builder.HasOne<User>(u => u.User)
      .WithMany(u => u.UserModuleRights)
      .HasForeignKey(u => u.UserId)
      .OnDelete(DeleteBehavior.Cascade);

      builder.HasOne<ModuleRights>(u => u.ModuleRights)
      .WithMany(u => u.UserModuleRights)
      .HasForeignKey(u => u.ModuleRightsId)
      .OnDelete(DeleteBehavior.Cascade);
    }
  }
}
