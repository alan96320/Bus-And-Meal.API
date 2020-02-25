using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IMealtypeRepository
  {
    Task<MealType> GetOne(int id);
    void Add(MealType mealType);
    void Remove(MealType mealType);
    Task<IEnumerable<MealType>> GetAll();
    Task<PagedList<MealType>> GetPagedmealType(MealTypeParams mealTypeParams);
  }
}