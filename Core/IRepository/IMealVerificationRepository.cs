using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IMealVerificationRepository
  {
    Task<MealOrderVerificationHeader> GetOne(int id);
    void Add(MealOrderVerificationHeader mealOrderVerificationHeader);
    void Remove(MealOrderVerificationHeader mealOrderVerificationHeader);

    Task<IEnumerable<MealOrderVerificationHeader>> GetAll();
    Task<PagedList<MealOrderVerificationHeader>> GetPagedMealOrderEntryHeader(MealVerificationParams mealVerificationParams);
  }
}