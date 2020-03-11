namespace BusMeal.API.Controllers.Resources
{
  public class SaveUserDepartmentResource
  {
    public int DepartmentId { get; set; }
    public int UserId { get; set; }
  }

  public class ViewUserDepartmentResource
  {
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public int UserId { get; set; }
  }
}