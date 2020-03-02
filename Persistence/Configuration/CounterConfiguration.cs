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
        .Property(c => c.Code)
        .HasColumnType("varchar(2)");


      builder
        .Property(c => c.Name)
        .HasColumnType("varchar(100)");

      builder
        .Property(c => c.Location)
        .HasColumnType("varchar(100)");
    }

  }

}