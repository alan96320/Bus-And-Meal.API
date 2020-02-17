using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
  {
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
      builder.Property(e => e.HrCoreNo)
      .HasColumnType("varchar(8)");

      builder.Property(e => e.Firstname)
      .HasColumnType("varchar(100)");

      builder.Property(e => e.Lastname)
            .HasColumnType("varchar(100)");

      builder.Property(e => e.Fullname)
      .HasColumnType("varchar(100)");
    }
  }
}