using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IMealOrderVerificationRepository
  {
    Task<MealOrderVerification> GetOne(int id);
    void Add(MealOrderVerification mealOrderVerification);
    void Remove(MealOrderVerification mealOrderVerification);

    Task<IEnumerable<MealOrderVerification>> GetAll();
    Task<PagedList<MealOrderVerification>> GetPagedMealOrderVerification(MealOrderVerificationParams mealOrderVerificationParams);
  }
}