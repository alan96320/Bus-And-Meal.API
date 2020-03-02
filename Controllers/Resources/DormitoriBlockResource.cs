namespace BusMeal.API.Controllers.Resources
{
  public class SaveDormitoryBlockResource
  {
    public string Code { get; set; }
    public string Name { get; set; }
  }

  public class ViewDormitoryBlockResource
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
  }
}