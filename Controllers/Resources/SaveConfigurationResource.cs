using System;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveConfigurationResource
  {
    public int RowGrid { get; set; }
    public DateTime LockedBusOrder { get; set; }
    public DateTime LockedMealOrder { get; set; }
  }
}