using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveMealOrderResource
  {
    public DateTime OrderEntryDate { get; set; }
    public int? DepartmentId { get; set; }
    public int? MealOrderVerificationId { get; set; }
    public bool isReadyToCollect { get; set; }
    public int UserId { get; set; }
    public bool isUpdate { get; set; } = false;
    public ICollection<SaveMealOrderDetailResource> MealOrderDetails { get; set; }
      = new Collection<SaveMealOrderDetailResource>();
  }

  public class ViewMealOrderResource
  {
    public int Id { get; set; }
    public DateTime OrderEntryDate { get; set; }
    public int? DepartmentId { get; set; }
    public int? MealOrderVerificationId { get; set; }
    public bool isReadyToCollect { get; set; }
    public int UserId { get; set; }
    public ICollection<ViewMealOrderDetailResource> MealOrderDetails { get; set; }
        = new Collection<ViewMealOrderDetailResource>();
  }
}