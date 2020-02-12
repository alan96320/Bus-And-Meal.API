namespace BusMeal.API.Controllers.Resources
{
  public class ViewEmployeeResource
  {
    public int Id { get; set; }

    public string HrCoreNo { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public string Fullname { get; set; }
    public int DepartmentId { get; set; }
  }
}