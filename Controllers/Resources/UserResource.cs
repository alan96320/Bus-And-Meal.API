using System.Collections.Generic;
using BusMeal.API.Core.Models;

namespace BusMeal.API.Controllers.Resources
{
  public class AddUserResource
  {
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string GddbId { get; set; }
    public bool AdminStatus { get; set; } = false;
    public bool isActive { get; set; }
    public int LockTransStatus { get; set; }
  }
  public class SaveUserResource
  {
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string GddbId { get; set; }
    public bool AdminStatus { get; set; }
    public bool isActive { get; set; }
    public int LockTransStatus { get; set; }
    public ICollection<SaveUserModuleRightsResource> UserModuleRights { get; set; }
    public ICollection<SaveUserDepartmentResource> UserDepartments { get; set; }
  }

  public class ViewUserResource
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string FullName { get; set; }
    public string LastName { get; set; }
    public string GddbId { get; set; }
    public bool AdminStatus { get; set; }
    public bool isActive { get; set; }
    public int LockTransStatus { get; set; }
    public ICollection<ViewUserModuleRightsResource> UserModuleRights { get; set; }
    public ICollection<ViewUserDepartmentResource> UserDepartments { get; set; }
  }
}
