using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
  {
    public void Configure(EntityTypeBuilder<Department> builder)
    {
      builder.Property(d => d.Name)
      .HasColumnType("varchar(255)")
     .IsRequired()
     .IsUnicode();

      builder.Property(d => d.Code)
      .HasColumnType("varchar(50)")
      .IsRequired()
      .IsUnicode();
    }
  }
}