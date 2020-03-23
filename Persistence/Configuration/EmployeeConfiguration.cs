using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
  {
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
      builder
        .HasIndex(e => e.HrCoreNo);

      builder
          .HasIndex(e => e.HIDNo);

      builder
          .HasIndex(e => e.Firstname);

      builder
          .HasIndex(e => e.Lastname);

      builder
          .HasIndex(e => e.DepartmentId);
    }
  }
}