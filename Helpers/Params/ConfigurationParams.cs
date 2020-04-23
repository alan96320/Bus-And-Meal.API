using System;

namespace BusMeal.API.Helpers.Params
{
  public class ConfigurationParams : BaseParams
  {
    public int RowGrid { get; set; } = 0;
    public DateTime LockedBusOrder { get; set; }
    public DateTime LockedMealOrder { get; set; }
  }
}