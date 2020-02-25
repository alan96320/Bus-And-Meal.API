using System;

namespace BusMeal.API.Controllers.Resources
{
  public class ViewConfigurationResource
  {
    public int Id { get; set; }
    public int RowGrid { get; set; }
    public string LockedBusOrder { get; set; }
    public string LockedMealOrder { get; set; }
  }
}