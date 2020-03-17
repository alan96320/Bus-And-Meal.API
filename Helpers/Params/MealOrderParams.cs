using System;

namespace BusMeal.API.Helpers.Params
{
  public class MealOrderParams : BaseParams
  {
    public DateTime OrderEntryDate { get; set; } = new DateTime(01, 1, 1);
    public int DepartmentId { get; set; } = 0;

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

  }
}