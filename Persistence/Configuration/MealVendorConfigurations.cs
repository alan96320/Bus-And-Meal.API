using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class MealVendorConfigurations : IEntityTypeConfiguration<MealVendor>
  {

    public void Configure(EntityTypeBuilder<MealVendor> builder)
    {
      builder
        .HasIndex(mv => mv.Code);

      builder
        .HasIndex(mv => mv.Name);
    }
  }
}