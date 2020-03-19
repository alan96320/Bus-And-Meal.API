using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IMealVendorRepository
  {
    Task<MealVendor> GetOne(int id);
    void Add(MealVendor mealVendor);
    void Remove(MealVendor mealVendor);
    void Update(MealVendor mealVendor);    
    Task<IEnumerable<MealVendor>> GetAll();
    Task<PagedList<MealVendor>> GetPagedMealVendor(MealVendorParams mealVendorParams);
  }
}