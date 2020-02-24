using System;

namespace BusMeal.API.Helpers.Params
{
  public class MealOrderParams : BaseParams
  {
    public DateTime OrderEntryDate { get; set; }
    public int DepartmentId { get; set; } = 0;
    public int MealOrderVerificationHeaderId { get; set; } = 0;
  }
}