namespace BusMeal.API.Helpers.Params
{
  public class MealVendorParams : BaseParams
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string ContactName { get; set; }
    public string ContactPhone { get; set; }
    public string ContactEmail { get; set; }
  }
}