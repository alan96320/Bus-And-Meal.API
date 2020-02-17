using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class UserConfiguration : IEntityTypeConfiguration<User>
  {
    public void Configure(EntityTypeBuilder<User> builder)
    {
      builder.Property(u => u.FirstName)
      .HasColumnType("varchar(100)");

      builder.Property(u => u.LastName)
      .HasColumnType("varchar(100)");

      builder.Property(u => u.FullName)
      .HasColumnType("varchar(100)");

      builder.Property(u => u.GddbId)
      .HasColumnType("varchar(100)");
    }
  }
}