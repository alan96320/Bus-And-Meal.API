using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IMealOrderRepository
  {
    Task<MealOrder> GetOne(int id, int? userId = null);
    void Add(MealOrder mealOrder);
    void Remove(MealOrder mealOrder);

    Task<IEnumerable<MealOrder>> GetAll(int? userId = null);
    Task<PagedList<MealOrder>> GetPagedMealOrder(MealOrderParams mealOrderParams, int? userId = null);
  }
}