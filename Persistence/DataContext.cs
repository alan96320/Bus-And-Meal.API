using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
using BusMeal.API.Persistence.Configuration;

namespace BusMeal.API.Persistence
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> Employee { get; set; }
    public DbSet<AppConfiguration> AppConfiguration { get; set; }
    public DbSet<Counter> Counter { get; set; }
    public DbSet<Audit> Audit { get; set; }
    public DbSet<ModuleRights> ModuleRights { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<UserDepartment> UserDepartment { get; set; }
    public DbSet<UserModuleRights> UserModuleRights { get; set; }
    public DbSet<DormitoryBlock> DormitoryBlock { get; set; }
    public DbSet<BusTime> BusTime { get; set; }
    public DbSet<MealOrderEntryHeader> MealOrderEntryHeader { get; set; }
    public DbSet<MealOrderVerificationHeader> MealOrderVerificationHeader { get; set; }
    public DbSet<MealOrderDetail> MealOrderDetail { get; set; }
    public DbSet<MealOrderVerificationHeaderTotal> MealOrderVerificationHeaderTotal { get; set; }
    public DbSet<BusOrderVerificationHeader> BusOrderVerificationHeader { get; set; }
    public DbSet<BusOrderEntryHeader> BusOrderEntryHeader { get; set; }
    public DbSet<BusOrderEntryDetail> BusOrderEntryDetail { get; set; }
    public DbSet<BusOrderVerificationHeaderTotal> BusOrderVerificationHeaderTotal { get; set; }
    public DbSet<MealType> MealType {get; set;}
    public DbSet<MealVendor> MealVendor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
      modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
      modelBuilder.ApplyConfiguration(new CounterConfiguration());
      modelBuilder.ApplyConfiguration(new AuditConfiguration());
      modelBuilder.ApplyConfiguration(new UserConfiguration());
      modelBuilder.ApplyConfiguration(new ModuleRightsConfiguration());
      modelBuilder.ApplyConfiguration(new MealVendorConfiguration());
      modelBuilder.ApplyConfiguration(new MealTypeConfiguration());
      modelBuilder.ApplyConfiguration(new DormitoryBlockConfiguration());
      modelBuilder.ApplyConfiguration(new BusTimeConfiguration());
      modelBuilder.ApplyConfiguration(new MealOrderVerificationHeaderConfiguration());
      modelBuilder.ApplyConfiguration(new BusOrderVerificationHeaderConfiguration());
    }
  }
}