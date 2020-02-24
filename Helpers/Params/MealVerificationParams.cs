using System;

namespace BusMeal.API.Helpers.Params
{
  public class MealVerificationParams : BaseParams
  {
    public string OrderNo { get; set; }
    public DateTime OrderDate { get; set; } = new DateTime(01, 1, 1);
    public bool OrderedStatus { get; set; }
  }
}