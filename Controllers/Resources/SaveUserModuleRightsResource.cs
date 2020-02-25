namespace BusMeal.API.Controllers.Resources
{
  public class SaveUserModuleRightsResource
  {
    public int RightsId { get; set; }
    public int UserId { get; set; }
    public bool Read { get; set; }
    public bool Write { get; set; }
  }
}