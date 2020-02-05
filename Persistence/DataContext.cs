using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace BusMeal.API.Persistance
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Configuration> Configuration { get; set; }
    public DbSet<Counter> Counter { get; set; }
    public DbSet<Audit> Audit { get; set; }

  }
}