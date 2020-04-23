namespace BusMeal.API.Controllers.Resources
{
  public class SaveCounterResource
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int Status { get; set; }
    public bool isUpdate { get; set; } = false;
  }

  public class ViewCounterResource
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public int Status { get; set; }
  }
}