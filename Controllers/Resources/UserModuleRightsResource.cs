using System.Collections.Generic;
using BusMeal.API.Core.Models;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveUserModuleRightsResource
  {
    public int RightsId { get; set; }
    public int UserId { get; set; }
    public bool Read { get; set; }
    public bool Write { get; set; }
  }

  public class ViewUserModuleRightsResource
  {
    public int Id { get; set; }
    public ViewModuleRightsResource ModuleRights { get; set; }
    public bool Read { get; set; }
    public bool Write { get; set; }
  }
}