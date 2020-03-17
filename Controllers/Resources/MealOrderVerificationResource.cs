using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveMealOrderVerificationResource
  {
    public string OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsClosed { get; set; }
    public IList<int> OrderList { get; set; }
    public bool isUpdate { get; set; } = false;

    public ICollection<SaveMealOrderVerificationDetailResource> MealOrderVerificationDetails { get; set; }
      = new Collection<SaveMealOrderVerificationDetailResource>();
  }

  public class ViewMealOrderVerificationResource
  {
    public int Id { get; set; }
    public string OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsClosed { get; set; }
    public ICollection<ViewMealOrderVerificationDetailResource> mealVerificationDetails { get; set; }
      = new Collection<ViewMealOrderVerificationDetailResource>();
    public ICollection<ViewMealOrderResource> mealOrders { get; set; } = new Collection<ViewMealOrderResource>();
  }
}