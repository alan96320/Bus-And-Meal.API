using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class DormitoryBlockConfiguration : IEntityTypeConfiguration<DormitoryBlock>
  {
    public void Configure(EntityTypeBuilder<DormitoryBlock> builder)
    {
      builder.Property(d => d.Code)
      .HasColumnType("varchar(2)");

      builder.Property(d => d.Name)
      .HasColumnType("varchar(100)");
    }
  }
}