using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder
        .HasMany<UserDepartment>(u => u.UserDepartments)
        .WithOne(ud => ud.User)
        .HasForeignKey(ud => ud.UserId)
        .OnDelete(DeleteBehavior.Restrict);

      builder
        .HasMany<UserModuleRight>(u => u.UserModuleRights)
        .WithOne(um => um.User)
        .HasForeignKey(um => um.UserId)
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}