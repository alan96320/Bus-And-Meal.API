using System.Threading.Tasks;
using BusMeal.API.Core;
using BusMeal.API.Persistance;

namespace BusMeal.API.Persistence
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly DataContext context;

    public UnitOfWork(DataContext context)
    {
      this.context = context;
    }

    public async Task<bool> CompleteAsync()
    {
      return await context.SaveChangesAsync() > 0 ;
    }
  }
}