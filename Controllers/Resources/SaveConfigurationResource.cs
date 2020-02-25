using System;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveConfigurationResource
  {
    public int RowGrid { get; set; }
    public string LockedBusOrder { get; set; }
    public string LockedMealOrder { get; set; }
  }
}