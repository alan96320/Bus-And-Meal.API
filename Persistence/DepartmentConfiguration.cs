using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence
{
  public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
  {
    public void Configure(EntityTypeBuilder<Department> builder)
    {
      builder.Property(d => d.Name)
     .IsRequired()
     .HasMaxLength(255)
     .IsUnicode();

      builder.Property(d => d.Code)
      .IsRequired()
      .HasMaxLength(50)
      .IsUnicode();
    }
  }
}