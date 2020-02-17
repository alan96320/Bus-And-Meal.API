namespace BusMeal.API.Controllers.Resources
{
  public class SaveUserResource
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string GddbId { get; set; }
    public bool AdminStatus { get; set; }
    public int LockTransStatus { get; set; }
  }
}