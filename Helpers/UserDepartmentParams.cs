namespace BusMeal.API.Helpers
{
  public class UserDepartmentParams : BaseParams
  {
    public int DepartmentId { get; set; } = 0;
    public int UserId { get; set; } = 0;
  }
}