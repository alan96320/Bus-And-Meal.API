namespace BusMeal.API.Controllers.Resources
{
  public class SaveConfigurationResource
  {
    public string LockedBusOrder { get; set; }
    public string LockedMealOrder { get; set; }
  }

  public class ViewConfigurationResource
  {
    public int Id { get; set; }
    public string LockedBusOrder { get; set; }
    public string LockedMealOrder { get; set; }
  }
}