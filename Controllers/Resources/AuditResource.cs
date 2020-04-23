using System;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveAuditResource
  {
    public string TableName { get; set; }
    public DateTime DateTime { get; set; }
    public int UserId { get; set; }
    public string KeyValues { get; set; }
    public string OldValues { get; set; }
    public string NewValues { get; set; }
    public bool isUpdate { get; set; } = false;
  }

  public class ViewAuditResource
  {
    public int Id { get; set; }
    public string TableName { get; set; }
    public DateTime DateTime { get; set; }
    public int UserId { get; set; }
    public string KeyValues { get; set; }
    public string OldValues { get; set; }
    public string NewValues { get; set; }
  }
}