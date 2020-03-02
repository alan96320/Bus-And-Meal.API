using System.Collections.Generic;
using BusMeal.API.Core.Models;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveUserModuleRightsResource
  {
    public int ModuleRightsId { get; set; }
    public bool Read { get; set; }
    public bool Write { get; set; }
  }

  public class ViewUserModuleRightsResource
  {
    public int Id { get; set; }
    public int ModuleRightsId { get; set; }
    public ViewModuleRightsResource ModuleRights { get; set; }
    public bool Read { get; set; }
    public bool Write { get; set; }
  }
}