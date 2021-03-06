namespace BusMeal.API.Controllers.Resources
{
  public class SaveBusTimeResource
  {
    public string Code { get; set; }
    public string Time { get; set; }
    public int DirectionEnum { get; set; }
    public bool isUpdate { get; set; } = false;
  }

  public class ViewBusTimeResource
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Time { get; set; }
    public int DirectionEnum { get; set; }
  }
}