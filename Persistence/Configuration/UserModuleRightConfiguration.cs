using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class UserModuleRightConfiguration : IEntityTypeConfiguration<UserModuleRight>
  {
    public void Configure(EntityTypeBuilder<UserModuleRight> builder)
    {
    }
  }
}
