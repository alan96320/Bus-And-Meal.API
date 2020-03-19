using System;

namespace BusMeal.API.Helpers.Params
{
  public class AuditParams : BaseParams
  {
    public DateTime Date { get; set; } = new DateTime(01, 1, 1);
    public int UserId { get; set; } = 0;
  }
}