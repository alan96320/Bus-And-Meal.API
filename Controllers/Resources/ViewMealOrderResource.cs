using System;
using System.Collections.ObjectModel;

namespace BusMeal.API.Controllers.Resources
{
  public class ViewMealOrderResource
  {
    public int Id { get; set; }
    public DateTime OrderEntryDate { get; set; }
    public int? DepartmentId { get; set; }
    public int? MealOrderVerificationHeaderId { get; set; }
    public Collection<ViewMealOrderDetailResource> MealOrderDetail { get; set; }
  }
}