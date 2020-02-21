using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IMealOrderRepository
  {
    Task<MealOrderEntryHeader> GetOne(int id);
    void Add(MealOrderEntryHeader mealOrderEntryHeader);
    // void Remove(MealOrderEntryHeader mealOrderEntryHeader);

    // Task<IEnumerable<MealOrderEntryHeader>> GetAll();
    // Task<PagedList<MealOrderEntryHeader>> GetPagedMealOrderEntryHeader(MealOrderParams mealOrderParams);
  }
}