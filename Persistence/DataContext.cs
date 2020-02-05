using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BusMeal.API.Persistence
{
    public class DataContext : DbContext
    {


       public DataContext(DbContextOptions<DataContext> options) : base (options)
       {
           
       }

       public DbSet<Department> Departments {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());


        }      

    }
}