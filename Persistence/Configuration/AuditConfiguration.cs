using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class AuditConfiguration : IEntityTypeConfiguration<Audit>
  {
    public void Configure(EntityTypeBuilder<Audit> builder)
    {
      builder
        .Property(a => a.TableName)
        .HasColumnType("varchar(100)");


    }

  }
}