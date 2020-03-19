namespace BusMeal.API.Helpers.Params
{
  public class UserParams : BaseParams
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool isActive { get; set; } = true;
  }
}