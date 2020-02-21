using System;
using System.Collections.ObjectModel;
using BusMeal.API.Core.Models;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveMealOrderResource
  {
    public DateTime OrderEntryDate { get; set; }
    public int? DepartmentId { get; set; }
    public int? MealOrderVerificationHeaderId { get; set; }
    public Collection<MealOrderDetail> MealOrderDetail { get; set; }
  }
}