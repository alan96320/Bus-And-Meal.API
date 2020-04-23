using System;

namespace BusMeal.API.Helpers.Params
{
  public class MealOrderVerificationParams : BaseParams
  {
    public string OrderNo { get; set; } = "";
    public DateTime OrderDate { get; set; } = new DateTime(01, 1, 1);
    public DateTime StartDate { get; set; } = new DateTime(01, 1, 1);
    public DateTime EndDate { get; set; } = new DateTime(01, 1, 1);
    public bool isOrdered { get; set; }
    public bool isLocked { get; set; }
  }
}