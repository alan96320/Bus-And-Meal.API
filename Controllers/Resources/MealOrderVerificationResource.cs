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
    public ICollection<SaveMealOrderResource> MealOrders { get; set; }
      = new Collection<SaveMealOrderResource>();
    public ICollection<SaveMealOrderVerificationDetailResource> MealOrderVerificationDetails { get; set; }
      = new Collection<SaveMealOrderVerificationDetailResource>();
  }

  public class ViewMealOrderVerificationResource
  {
    public int Id { get; set; }
    public string OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsClosed { get; set; }

    public ICollection<ViewMealOrderResource> MealOrders { get; set; }
      = new Collection<ViewMealOrderResource>();
    public ICollection<ViewMealOrderVerificationDetailResource> MealVerificationDetails { get; set; }
      = new Collection<ViewMealOrderVerificationDetailResource>();
  }
}