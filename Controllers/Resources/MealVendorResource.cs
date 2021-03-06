namespace BusMeal.API.Controllers.Resources
{
  public class SaveMealVendorResource
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string ContactName { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
    public bool isUpdate { get; set; } = false;
  }

  public class ViewMealVendorResource
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string ContactName { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
  }
}