namespace BusMeal.API.Controllers.Resources
{
  public class SaveModuleRightsResource
  {
    public string Code { get; set; }
    public string Description { get; set; }
  }

  public class ViewModuleRightsResource
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
  }
}