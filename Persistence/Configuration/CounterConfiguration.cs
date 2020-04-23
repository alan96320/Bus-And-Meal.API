using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class CounterConfiguration : IEntityTypeConfiguration<Counter>
  {
    public void Configure(EntityTypeBuilder<Counter> builder)
    {
      builder
          .HasIndex(c => c.Code);

      builder
          .HasIndex(c => c.Name);

      builder
          .HasIndex(c => c.Location);

      builder
          .HasIndex(c => c.Status);
    }
  }
}