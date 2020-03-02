namespace BusMeal.API.Controllers.Resources
{
  public class SaveConfigurationResource
  {
    public int RowGrid { get; set; }
    public string LockedBusOrder { get; set; }
    public string LockedMealOrder { get; set; }
  }

  public class ViewConfigurationResource
  {
    public int Id { get; set; }
    public int RowGrid { get; set; }
    public string LockedBusOrder { get; set; }
    public string LockedMealOrder { get; set; }
  }
}