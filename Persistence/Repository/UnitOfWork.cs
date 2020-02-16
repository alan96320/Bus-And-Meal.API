using System.Threading.Tasks;
using BusMeal.API.Core.IRepository;

namespace BusMeal.API.Persistence.Repository
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
      return await context.SaveChangesAsync() > 0;
    }
  }
}