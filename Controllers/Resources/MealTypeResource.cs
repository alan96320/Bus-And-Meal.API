using System.Collections.Generic;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveMealTypeResource
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public int MealVendorId { get; set; }
    public bool isUpdate { get; set; } = false;
  }

  public class ViewMealTypeResource
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public int MealVendorId { get; set; }
    public ViewMealVendorResource mealVendor { get; set; }
  }
}