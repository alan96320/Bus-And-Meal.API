using System;
using System.Collections.ObjectModel;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveMealVerificationResource
  {
    public string OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
    public bool OrderedStatus { get; set; }
    public Collection<SaveMealVerificationTotalResource> MealVerificationTotal { get; set; }
  }
}