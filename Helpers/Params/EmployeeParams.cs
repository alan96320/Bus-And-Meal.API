namespace BusMeal.API.Helpers.Params
{
  public class EmployeeParams : BaseParams
  {
    public string HrCoreNo { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string DepartmentCode {get;set;}
    public string DepartmentName {get;set;}    
  }
}