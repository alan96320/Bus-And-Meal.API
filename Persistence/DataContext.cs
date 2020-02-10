using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BusMeal.API.Persistence
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Configuration> Configuration { get; set; }
    public DbSet<Counter> Counter { get; set; }
    public DbSet<Audit> Audit { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
      modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
      modelBuilder.ApplyConfiguration(new CounterConfiguration());
      modelBuilder.ApplyConfiguration(new AuditConfiguration());
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new ModuleRightsConfiguration());

    }


  }
}