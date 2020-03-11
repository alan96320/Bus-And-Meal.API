using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using BusMeal.API.Persistence.Configuration;

namespace BusMeal.API.Persistence
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Department> Department { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<AppConfiguration> AppConfiguration { get; set; }
    public DbSet<Counter> Counter { get; set; }
    public DbSet<Audit> Audit { get; set; }
    public DbSet<ModuleRight> ModuleRight { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserDepartment> UserDepartment { get; set; }
    public DbSet<UserModuleRight> UserModuleRight { get; set; }
    public DbSet<DormitoryBlock> DormitoryBlock { get; set; }
    public DbSet<BusTime> BusTime { get; set; }
    public DbSet<MealOrder> MealOrder { get; set; }
    public DbSet<MealOrderVerification> MealOrderVerification { get; set; }
    public DbSet<MealOrderDetail> MealOrderDetail { get; set; }
    public DbSet<MealOrderVerificationDetail> MealOrderVerificationDetail { get; set; }
    public DbSet<BusOrderVerification> BusOrderVerification { get; set; }
    public DbSet<BusOrder> BusOrder { get; set; }
    public DbSet<BusOrderDetail> BusOrderDetail { get; set; }
    public DbSet<BusOrderVerificationDetail> BusOrderVerificationDetail { get; set; }
    public DbSet<MealType> MealType { get; set; }
    public DbSet<MealVendor> MealVendor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new ModuleRightsConfiguration());
      modelBuilder.ApplyConfiguration(new MealTypeConfiguration());
      modelBuilder.ApplyConfiguration(new DormitoryBlockConfiguration());
      modelBuilder.ApplyConfiguration(new BusTimeConfiguration());
      modelBuilder.ApplyConfiguration(new MealOrderConfiguration());
      modelBuilder.ApplyConfiguration(new MealOrderVerificationConfiguration());
      modelBuilder.ApplyConfiguration(new BusOrderConfiguration());
      modelBuilder.ApplyConfiguration(new BusOrderVerificationConfiguration());
    }
  }
}