using System.Collections.Generic;

namespace BusMeal.API.Controllers.Resources
{
  public class ViewMealTypeResource
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public ICollection<ViewMealVendorResource> mealVendor { get; set; }
  }
}