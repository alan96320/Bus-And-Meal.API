using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence.Configuration
{
  public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
  {
    public void Configure(EntityTypeBuilder<Department> builder)
    {
      builder
        .Property(d => d.Name)
        .IsUnicode();

      builder
        .Property(d => d.Code)
        .IsUnicode();

      builder
        .HasMany<Employee>(d => d.Employees)
        .WithOne(e => e.Department)
        .HasForeignKey(e => e.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict);

      builder
        .HasMany<MealOrder>(d => d.MealOrders)
        .WithOne(mo => mo.Department)
        .HasForeignKey(mo => mo.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict);

      builder
        .HasMany<UserDepartment>(d => d.UserDepartments)
        .WithOne(ud => ud.Department)
        .HasForeignKey(ud => ud.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict);

      builder
        .HasMany<BusOrder>(d => d.BusOrders)
        .WithOne(bo => bo.Department)
        .HasForeignKey(bo => bo.DepartmentId)
        .OnDelete(DeleteBehavior.Restrict);
    }
  }
}