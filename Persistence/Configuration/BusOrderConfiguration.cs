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
        .HasIndex(bo => bo.OrderEntryDate);

      builder
        .HasIndex(bo => bo.DepartmentId);

      builder
        .HasIndex(bo => bo.BusOrderVerificationId);

      builder
        .HasIndex(bo => bo.UserId);

      builder
        .HasMany<BusOrderDetail>(bo => bo.BusOrderDetails)
        .WithOne(bod => bod.BusOrder)
        .HasForeignKey(bod => bod.BusOrderId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}