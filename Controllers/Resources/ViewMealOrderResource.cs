using System;
using System.Collections.Generic;

namespace BusMeal.API.Controllers.Resources
{
  public class ViewMealOrderResource
  {
    public int Id { get; set; }
    public DateTime OrderEntryDate { get; set; }
    public int? DepartmentId { get; set; }
    public int? MealOrderVerificationHeaderId { get; set; }
    public ICollection<ViewMealOrderDetailResource> MealOrderDetail { get; set; }
  }
}