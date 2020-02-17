using System;

namespace BusMeal.API.Controllers.Resources
{
  public class ViewConfigurationResource
  {
    public int Id { get; set; }
    public int RowGrid { get; set; }
    public DateTime LockedBusOrder { get; set; }
    public DateTime LockedMealOrder { get; set; }
  }
}