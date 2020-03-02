namespace BusMeal.API.Controllers.Resources
{
  public class SaveDepartmentResource
  {

    public string Name { get; set; }

    public string Code { get; set; }


  }

  public class ViewDepartmentResource
  {
    public int Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }
  }
}