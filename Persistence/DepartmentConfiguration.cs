using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusMeal.API.Persistence
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> modelBuilder)
        {
            modelBuilder.HasIndex(d => d.Code).IsUnique();
            modelBuilder.HasIndex(d => d.Name).IsUnique();
        }
    }
}