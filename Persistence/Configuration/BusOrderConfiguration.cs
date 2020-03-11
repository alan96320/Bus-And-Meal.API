using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class BusOrderConfiguration : IEntityTypeConfiguration<BusOrder>
  {
    public void Configure(EntityTypeBuilder<BusOrder> builder)
    {
      builder
        .HasMany<BusOrderDetail>(bo => bo.BusOrderDetails)
        .WithOne(bod => bod.BusOrder)
        .HasForeignKey(bod => bod.BusOrderId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}