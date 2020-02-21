using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Persistence.Repository
{
  public class MealOrderRepository : IMealOrderRepository
  {
    private readonly DataContext context;

    public MealOrderRepository(DataContext context)
    {
      this.context = context;
    }
    public void Add(MealOrderEntryHeader mealOrderEntryHeader)
    {
      context.MealOrderEntryHeader.Add(mealOrderEntryHeader);
    }

    // public Task<IEnumerable<MealOrderEntryHeader>> GetAll()
    // {


    // }

    public async Task<MealOrderEntryHeader> GetOne(int id)
    {
      return await context.MealOrderEntryHeader.FindAsync(id);
    }

    // public Task<PagedList<MealOrderEntryHeader>> GetPagedMealOrderEntryHeader(MealOrderParams mealOrderParams)
    // {


    // }

    // public void Remove(MealOrderEntryHeader mealOrderEntryHeader)
    // {


    // }
  }
}