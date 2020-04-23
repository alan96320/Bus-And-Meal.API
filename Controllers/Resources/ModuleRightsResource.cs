using System.Collections.Generic;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveModuleRightsResource
  {
    public string Code { get; set; }
    public string Description { get; set; }
    public bool isUpdate { get; set; } = false;
  }

  public class ViewModuleRightsResource
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
  }
}