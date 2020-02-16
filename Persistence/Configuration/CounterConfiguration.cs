using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence
{
  public class CounterConfiguration : IEntityTypeConfiguration<Counter>
  {
    public void Configure(EntityTypeBuilder<Counter> builder)
    {
      builder.Property(c => c.Code)
      .HasColumnType("varchar")
      .HasMaxLength(2);

      builder.Property(c => c.Name)
      .HasColumnType("varchar")
      .HasMaxLength(100);

      builder.Property(c => c.Location)
      .HasColumnType("varchar")
      .HasMaxLength(100);
    }

  }
}