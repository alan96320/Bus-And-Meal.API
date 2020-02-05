using BusMeal.API.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace BusMeal.API.Persistance
{
    public class DataContext : DbContext
    {


       public DataContext(DbContextOptions<DataContext> options) : base (options)
       {
           
       }

       public DbSet<Department> Departments {get; set;}

    }
}