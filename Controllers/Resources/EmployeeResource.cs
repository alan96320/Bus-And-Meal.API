namespace BusMeal.API.Controllers.Resources
{
  public class SaveEmployeeResource
  {
    public string HrCoreNo { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public string Fullname { get; set; }

    public string HIDNo { get; set; }

    public int DepartmentId { get; set; }
    public bool isUpdate { get; set; } = false;
  }

  public class ViewEmployeeResource
  {
    public int Id { get; set; }
    public string HrCoreNo { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Fullname { get; set; }
    public string HIDNo { get; set; }
    public int DepartmentId { get; set; }
    public ViewDepartmentResource Department { get; set; }
  }
}