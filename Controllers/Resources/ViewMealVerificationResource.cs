using System;
using System.Collections.ObjectModel;

namespace BusMeal.API.Controllers.Resources
{
  public class ViewMealVerificationResource
  {
    public int Id { get; set; }
    public string OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
    public bool OrderedStatus { get; set; }
    public Collection<ViewMealVerificationTotalResource> MealVerificationTotal { get; set; }
  }
}